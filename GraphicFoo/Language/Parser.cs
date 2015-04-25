using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Parser
	{
		#region Parser Main

		public const int _EOF = 0;
		public const int _id = 1;
		public const int _number = 2;
		public const int _string = 3;
		public const int _true = 4;
		public const int _false = 5;
		public const int maxT = 36;

		const bool _T = true;
		const bool _x = false;
		const int minErrDist = 2;
	
		public Scanner scanner;
		public Errors errors;

		public Token t;
		// last recognized token
		public Token la;
		// lookahead token
		int errDist = minErrDist;

		// Memory
		public ProgramMemory programMemory;
		// Scope
		public Procedure scope = null;

		public Parser (Scanner scanner)
		{
			this.scanner = scanner;
			errors = new Errors ();

			// Memory
			this.programMemory = new ProgramMemory ();

			// Stacks
			Quadruple.Initialize ();
		}

		#endregion

		#region Match

		void SynErr (int n)
		{
			if (errDist >= minErrDist)
				errors.SynErr (la.line, la.col, n);
			errDist = 0;
		}

		public void SemErr (string msg)
		{
			if (errDist >= minErrDist)
				errors.SemErr (t.line, t.col, msg);
			errDist = 0;
		}

		void Get ()
		{
			for (;;) {
				t = la;
				la = scanner.Scan ();
				if (la.kind <= maxT) {
					++errDist;
					break;
				}

				la = t;
			}
		}

		void Expect (int n)
		{
			if (la.kind == n)
				Get ();
			else {
				SynErr (n);
			}
		}

		bool StartOf (int s)
		{
			return set [s, la.kind];
		}

		void ExpectWeak (int n, int follow)
		{
			if (la.kind == n)
				Get ();
			else {
				SynErr (n);
				while (!StartOf (follow))
					Get ();
			}
		}

		bool WeakSeparator (int n, int syFol, int repFol)
		{
			int kind = la.kind;
			if (kind == n) {
				Get ();
				return true;
			} else if (StartOf (repFol)) {
				return false;
			} else {
				SynErr (n);
				while (!(set [syFol, kind] ||
				       set [repFol, kind] ||
				       set [0, kind])) {
					Get ();
					kind = la.kind;
				}
				return StartOf (syFol);
			}
		}

		#endregion

		#region FOO Language

		void FOO ()
		{
			Program ();
			Expect ((int)TokenEnum.EOF);
		}

		void Program ()
		{
			while (StartOf ((int)TokenEnum.Id)) {
				Declaration ();
			}
			Function ();
			while (la.kind == (int)TokenEnum.Function) {
				Function ();
			}
		}

		void Declaration ()
		{
			Type ();
			string type = GetLastTokenValue ();
			string id = Assignation ();
			if (scope == null) {
				programMemory.AddGlobalVariable (id, type);
			} else {
				scope.AddVariable (id, type);
			}
		}

		void Function ()
		{
			scope = FunctionHeader ();
			Quadruple.scope = scope;
			while (StartOf ((int)TokenEnum.Id)) {
				Declaration ();
			}
			while (StartOf ((int)TokenEnum.Number)) {
				Statute ();
			}
			Return ();
			EndFunction ();
		}

		Procedure FunctionHeader ()
		{
			Expect ((int)TokenEnum.Function);
			Expect ((int)TokenEnum.Id);
			string id = GetLastTokenValue ();
			Expect ((int)TokenEnum.LeftParenthesis);

			VariableBlock variables = null;

			if (StartOf ((int)TokenEnum.Id)) {
				variables = new VariableBlock ();
				Type ();
				string varType = GetLastTokenValue ();
				Expect ((int)TokenEnum.Id);
				string varId = GetLastTokenValue ();
				Variable variable = new Variable (varId, varType);
				variables.AddVariable (variable);
				while (la.kind == (int)TokenEnum.Comma) {
					Get ();
					Type ();
					varType = GetLastTokenValue ();
					Expect ((int)TokenEnum.Id);
					varId = GetLastTokenValue ();
					variable = new Variable (varId, varType);
					variables.AddVariable (variable);
				}
			}
			Expect ((int)TokenEnum.RightParenthesis);
			Expect ((int)TokenEnum.Colon);
			Type ();
			string type = GetLastTokenValue ();
			return programMemory.AddProcedure (id, type, variables);
		}

		void Statute ()
		{
			if (la.kind == (int)TokenEnum.Id) {
				Assignation ();
			} else if (la.kind == (int)TokenEnum.If) {
				Condition ();
			} else if (la.kind == (int)TokenEnum.While) {
				Loop ();
			} else if (la.kind == (int)TokenEnum.Print) {
				Print ();
			} else if (la.kind == (int)TokenEnum.Function) {
				CallFunction ();
			} else
				SynErr (37);
		}

		void Return ()
		{
			Expect (6);
			Var ();
			Expect ((int)TokenEnum.Semicolon);
		}

		void EndFunction ()
		{
			Expect (13);
		}

		Variable Var ()
		{
			if (la.kind == (int)TokenEnum.Id) {
				Get ();
				string id = GetLastTokenValue ();
				return programMemory.FindVariable (scope, id);
			} else if (la.kind == (int)TokenEnum.Number ||
			           la.kind == (int)TokenEnum.String ||
			           la.kind == (int)TokenEnum.True ||
			           la.kind == (int)TokenEnum.False) {
				GraphicFooType type;
				if (la.kind == (int)TokenEnum.Number) {
					type = GraphicFooType.Number;
				} else if (la.kind == (int)TokenEnum.String) {
					type = GraphicFooType.String;
				} else {
					type = GraphicFooType.Boolean;
				}
				Get ();
				string constant = GetLastTokenValue ();
				return programMemory.AddConstant (constant, type);
			} else {
				SynErr (38);
				return null;
			}
		}

		string Assignation ()
		{
			Expect ((int)TokenEnum.Id);
			string id = GetLastTokenValue ();
			if (la.kind == 8) {
				Get ();
				Expression ();
			}
			Expect ((int)TokenEnum.Semicolon);
			return id;
		}

		void Condition ()
		{
			IfHeader ();
			while (StartOf ((int)TokenEnum.Number)) {
				Statute ();
			}
			if (la.kind == (int)TokenEnum.Else) {
				Get ();
				Quadruple.PopJump ();
				Quadruple.CreateGotoQuadruple ();
				Quadruple.PushJump ();
				while (StartOf ((int)TokenEnum.Number)) {
					Statute ();
				}
			}
			EndIf ();
		}

		void Loop ()
		{
			LoopHeader ();
			while (StartOf ((int)TokenEnum.Number)) {
				Statute ();
			}
			EndLoop ();
		}

		void Print ()
		{
			Expect ((int)TokenEnum.Print);
			Expect ((int)TokenEnum.LeftParenthesis);
			Expression ();
			Expect ((int)TokenEnum.RightParenthesis);
			Expect ((int)TokenEnum.Semicolon);
		}

		void CallFunction ()
		{
			Expect ((int)TokenEnum.Function);
			Expect ((int)TokenEnum.Id);
			Expect ((int)TokenEnum.LeftParenthesis);
			if (StartOf ((int)TokenEnum.String)) {
				Expression ();
				while (la.kind == (int)TokenEnum.Comma) {
					Get ();
					Expression ();
				}
			}
			Expect ((int)TokenEnum.RightParenthesis);
		}

		void Type ()
		{
			if (la.kind == 16) {
				Get ();
			} else if (la.kind == 17) {
				Get ();
			} else if (la.kind == 18) {
				Get ();
			} else if (la.kind == 19) {
				Get ();
			} else
				SynErr (39);
		}

		void Expression ()
		{
			Exp ();
			if (StartOf ((int)TokenEnum.True)) {
				Operators op;
				if (la.kind == (int)TokenEnum.Greater) {
					Get ();
					op = Operators.Greater;
				} else if (la.kind == (int)TokenEnum.Lesser) {
					Get ();
					op = Operators.Lesser;
				} else if (la.kind == (int)TokenEnum.Unequal) {
					Get ();
					op = Operators.Unequal;
				} else if (la.kind == (int)TokenEnum.Equals) {
					Get ();
					op = Operators.Equal;
				} else {
					Get ();
					op = Operators.Concatenation;
				}
				Quadruple.operatorStack.Push (op);
				Exp ();
				Quadruple.CreateRelationalQuadruple ();
			}
		}

		void EndIf ()
		{
			Expect ((int)TokenEnum.EndIf);
			Quadruple.PopJump ();
		}

		void EndLoop ()
		{
			Expect ((int)TokenEnum.EndWhile);
			Quadruple.CreateGotoQuadruple ();
			Quadruple.PopTwoJumps ();
		}

		void LoopHeader ()
		{
			Expect ((int)TokenEnum.While);
			Quadruple.PushJump ();
			Expect ((int)TokenEnum.LeftParenthesis);
			Expression ();
			Quadruple.PushJump ();
			GraphicFooType type = Quadruple.typeStack.Pop ();
			if (Semantics.ExpectType (type, GraphicFooType.Boolean)) {
				string id = Quadruple.operandStack.Pop ();
				Quadruple.CreateGotoFalseQuadruple (id);
			}
			Expect ((int)TokenEnum.RightParenthesis);
		}

		void IfHeader ()
		{
			Expect ((int)TokenEnum.If);
			Expect ((int)TokenEnum.LeftParenthesis);
			Expression ();
			GraphicFooType type = Quadruple.typeStack.Pop ();
			if (Semantics.ExpectType (type, GraphicFooType.Boolean)) {
				string id = Quadruple.operandStack.Pop ();
				Quadruple.CreateGotoFalseQuadruple (id);
				Quadruple.PushJump ();
			}
			Expect ((int)TokenEnum.RightParenthesis);
		}

		void Exp ()
		{
			Term ();
			Quadruple.CreateAdditiveQuadruple ();
			while (la.kind == (int)TokenEnum.Plus ||
			       la.kind == (int)TokenEnum.Minus ||
			       la.kind == (int)TokenEnum.Or) {
				Operators op;
				if (la.kind == (int)TokenEnum.Plus) {
					Get ();
					op = Operators.Plus;
				} else if (la.kind == (int)TokenEnum.Minus) {
					Get ();
					op = Operators.Minus;
				} else {
					Get ();
					op = Operators.Or;
				}
				Quadruple.operatorStack.Push (op);
				Term ();
				Quadruple.CreateAdditiveQuadruple ();

			}
		}

		void Term ()
		{
			Factor ();
			Quadruple.CreateMultiplicativeQuadruple ();
			while (la.kind == (int)TokenEnum.Multiplication ||
			       la.kind == (int)TokenEnum.Division ||
			       la.kind == (int)TokenEnum.And) {
				Operators op;
				if (la.kind == (int)TokenEnum.Multiplication) {
					Get ();
					op = Operators.Multiplication;
				} else if (la.kind == (int)TokenEnum.Division) {
					Get ();
					op = Operators.Division;
				} else {
					Get ();
					op = Operators.And;
				}
				Quadruple.operatorStack.Push (op);
				Factor ();
				Quadruple.CreateMultiplicativeQuadruple ();
			}
		}

		void Factor ()
		{
			if (la.kind == (int)TokenEnum.LeftParenthesis) {
				Quadruple.hierarchyStack.Push (Quadruple.operatorStack.Count);
				Get ();
				Expression ();
				Expect ((int)TokenEnum.RightParenthesis);
				Quadruple.hierarchyStack.Pop ();
			} else if (StartOf ((int)TokenEnum.False)) {
				if (la.kind == (int)TokenEnum.Plus ||
				    la.kind == (int)TokenEnum.Minus) {
					if (la.kind == (int)TokenEnum.Plus) {
						Get ();
					} else {
						Get ();
					}
				}
				Variable variable = Var ();
				Quadruple.operandStack.Push (variable.name);
				Quadruple.typeStack.Push (variable.type);

			} else
				SynErr (40);
		}

		#endregion

		#region Token Utils

		void DebugTokens ()
		{
			Console.WriteLine ("last:" + t.val + " lookahead:" + la.val);
		}

		string GetLastTokenValue ()
		{
			return t.val;
		}

		string GetNextTokenValue ()
		{
			return la.val;
		}

		#endregion

		#region Parse Utils

		public void Parse ()
		{
			la = new Token ();
			la.val = "";		
			Get ();
			FOO ();
			Expect ((int)TokenEnum.EOF);

		}

		static readonly bool[,] set = { {
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}, {
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_T,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}, {
				_x,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}, {
				_x,
				_T,
				_T,
				_T,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}, {
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_T,
				_T,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}, {
				_x,
				_T,
				_T,
				_T,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x,
				_T,
				_T,
				_x,
				_x,
				_x,
				_x,
				_x,
				_x
			}

		};
	}

	#endregion

	#region Errors

	public class Errors
	{
		public int count = 0;
		// number of errors detected
		public System.IO.TextWriter errorStream = Console.Out;
		// error messages go to this stream
		public string errMsgFormat = "-- line {0} col {1}: {2}";
		// 0=line, 1=column, 2=text
		public string errorMessage;

		public virtual void SynErr (int line, int col, int n)
		{
			string s;
			switch (n) {
			case (int)TokenEnum.EOF:
				s = "EOF expected";
				break;
			case (int)TokenEnum.Id:
				s = "id expected";
				break;
			case (int)TokenEnum.Number:
				s = "number expected";
				break;
			case (int)TokenEnum.String:
				s = "string expected";
				break;
			case (int)TokenEnum.True:
				s = "true expected";
				break;
			case (int)TokenEnum.False:
				s = "false expected";
				break;
			case 6:
				s = "\"return\" expected";
				break;
			case (int)TokenEnum.Semicolon:
				s = "\";\" expected";
				break;
			case 8:
				s = "\"=\" expected";
				break;
			case (int)TokenEnum.Function:
				s = "\"function\" expected";
				break;
			case (int)TokenEnum.LeftParenthesis:
				s = "\"(\" expected";
				break;
			case (int)TokenEnum.Comma:
				s = "\",\" expected";
				break;
			case (int)TokenEnum.RightParenthesis:
				s = "\")\" expected";
				break;
			case 13:
				s = "\"endFunc\" expected";
				break;
			case (int)TokenEnum.EndIf:
				s = "\"endIf\" expected";
				break;
			case (int)TokenEnum.EndWhile:
				s = "\"endWhile\" expected";
				break;
			case 16:
				s = "\"number\" expected";
				break;
			case 17:
				s = "\"boolean\" expected";
				break;
			case 18:
				s = "\"void\" expected";
				break;
			case 19:
				s = "\"string\" expected";
				break;
			case (int)TokenEnum.Print:
				s = "\"print\" expected";
				break;
			case (int)TokenEnum.While:
				s = "\"while\" expected";
				break;
			case (int)TokenEnum.If:
				s = "\"if\" expected";
				break;
			case (int)TokenEnum.Else:
				s = "\"else\" expected";
				break;
			case (int)TokenEnum.Colon:
				s = "\":\" expected";
				break;
			case (int)TokenEnum.Greater:
				s = "\">\" expected";
				break;
			case (int)TokenEnum.Lesser:
				s = "\"<\" expected";
				break;
			case (int)TokenEnum.Unequal:
				s = "\"!=\" expected";
				break;
			case (int)TokenEnum.Equals:
				s = "\"==\" expected";
				break;
			case 29:
				s = "\".\" expected";
				break;
			case (int)TokenEnum.Plus:
				s = "\"+\" expected";
				break;
			case (int)TokenEnum.Minus:
				s = "\"-\" expected";
				break;
			case (int)TokenEnum.Or:
				s = "\"or\" expected";
				break;
			case (int)TokenEnum.Multiplication:
				s = "\"*\" expected";
				break;
			case (int)TokenEnum.Division:
				s = "\"/\" expected";
				break;
			case (int)TokenEnum.And:
				s = "\"and\" expected";
				break;
			case 36:
				s = "??? expected";
				break;
			case 37:
				s = "invalid Statute";
				break;
			case 38:
				s = "invalid Var";
				break;
			case 39:
				s = "invalid Type";
				break;
			case 40:
				s = "invalid Factor";
				break;

			default:
				s = "error " + n;
				break;
			}
			errorStream.WriteLine (errMsgFormat, line, col, s);
			errorMessage += 
				"line:" + line + " col:" + col + " error:" + s + "\n";
			count++;
		}

		public virtual void SemErr (int line, int col, string s)
		{
			errorStream.WriteLine (errMsgFormat, line, col, s);
			count++;
		}

		public virtual void SemErr (string s)
		{
			errorStream.WriteLine (s);
			count++;
		}

		public virtual void Warning (int line, int col, string s)
		{
			errorStream.WriteLine (errMsgFormat, line, col, s);
		}

		public virtual void Warning (string s)
		{
			errorStream.WriteLine (s);
		}
	}
	// Errors

	public class FatalError: Exception
	{
		public FatalError (string m) : base (m)
		{
		}
	}

	#endregion
}