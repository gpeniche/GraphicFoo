using System;

namespace GraphicFoo
{
	public class Parser
	{
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

		public Parser (Scanner scanner)
		{
			this.scanner = scanner;
			errors = new Errors ();
		}

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
				while (!(set [syFol, kind] || set [repFol, kind] || set [0, kind])) {
					Get ();
					kind = la.kind;
				}
				return StartOf (syFol);
			}
		}
	
		void FOO ()
		{
			Program ();
			Expect (0);
		}

		void Program ()
		{
			while (StartOf (1)) {
				Declaration ();
			}
			Function ();
		}

		void Declaration ()
		{
			Type ();
			Assignation ();
		}

		void Function ()
		{
			FunctionHeader ();
			while (StartOf (2)) {
				Statute ();
			}
			Return ();
			EndFunction ();
		}

		void FunctionHeader ()
		{
			Expect (9);
			Expect (1);
			Expect (10);
			if (StartOf (1)) {
				Type ();
				Expect (1);
				while (la.kind == 11) {
					Get ();
					Type ();
					Expect (1);
				}
			}
			Expect (12);
			Expect (24);
			Type ();
		}

		void Statute ()
		{
			if (la.kind == 1) {
				Assignation ();
			} else if (la.kind == 22) {
				Condition ();
			} else if (la.kind == 21) {
				Loop ();
			} else if (la.kind == 20) {
				Print ();
			} else if (la.kind == 9) {
				CallFunction ();
			} else
				SynErr (37);
		}

		void Return ()
		{
			Expect (6);
			Var ();
			Expect (7);
		}

		void EndFunction ()
		{
			Expect (13);
		}

		void Var ()
		{
			if (la.kind == 1) {
				Get ();
			} else if (la.kind == 2) {
				Get ();
			} else if (la.kind == 3) {
				Get ();
			} else if (la.kind == 4) {
				Get ();
			} else if (la.kind == 5) {
				Get ();
			} else
				SynErr (38);
		}

		void Assignation ()
		{
			Expect (1);
			if (la.kind == 8) {
				Get ();
				Expression ();
			}
			Expect (7);
		}

		void Condition ()
		{
			IfHeader ();
			while (StartOf (2)) {
				Statute ();
			}
			if (la.kind == 23) {
				Get ();
				while (StartOf (2)) {
					Statute ();
				}
			}
			EndIf ();
		}

		void Loop ()
		{
			LoopHeader ();
			while (StartOf (2)) {
				Statute ();
			}
			EndLoop ();
		}

		void Print ()
		{
			Expect (20);
			Expect (10);
			Expression ();
			Expect (12);
			Expect (7);
		}

		void CallFunction ()
		{
			Expect (9);
			Expect (1);
			Expect (10);
			if (StartOf (3)) {
				Expression ();
				while (la.kind == 11) {
					Get ();
					Expression ();
				}
			}
			Expect (12);
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
			if (StartOf (4)) {
				if (la.kind == 25) {
					Get ();
				} else if (la.kind == 26) {
					Get ();
				} else if (la.kind == 27) {
					Get ();
				} else if (la.kind == 28) {
					Get ();
				} else {
					Get ();
				}
				Exp ();
			}
		}

		void EndIf ()
		{
			Expect (14);
		}

		void EndLoop ()
		{
			Expect (15);
		}

		void LoopHeader ()
		{
			Expect (21);
			Expect (10);
			Expression ();
			Expect (12);
		}

		void IfHeader ()
		{
			Expect (22);
			Expect (10);
			Expression ();
			Expect (12);
		}

		void Exp ()
		{
			Term ();
			while (la.kind == 30 || la.kind == 31 || la.kind == 32) {
				if (la.kind == 30) {
					Get ();
				} else if (la.kind == 31) {
					Get ();
				} else {
					Get ();
				}
				Term ();
			}
		}

		void Term ()
		{
			Factor ();
			while (la.kind == 33 || la.kind == 34 || la.kind == 35) {
				if (la.kind == 33) {
					Get ();
				} else if (la.kind == 34) {
					Get ();
				} else {
					Get ();
				}
				Factor ();
			}
		}

		void Factor ()
		{
			if (la.kind == 10) {
				Get ();
				Expression ();
				Expect (12);
			} else if (StartOf (5)) {
				if (la.kind == 30 || la.kind == 31) {
					if (la.kind == 30) {
						Get ();
					} else {
						Get ();
					}
				}
				Var ();
			} else
				SynErr (40);
		}

		public void Parse ()
		{
			la = new Token ();
			la.val = "";		
			Get ();
			FOO ();
			Expect (0);

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
	// end Parser

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
			case 0:
				s = "EOF expected";
				break;
			case 1:
				s = "id expected";
				break;
			case 2:
				s = "number expected";
				break;
			case 3:
				s = "string expected";
				break;
			case 4:
				s = "true expected";
				break;
			case 5:
				s = "false expected";
				break;
			case 6:
				s = "\"return\" expected";
				break;
			case 7:
				s = "\";\" expected";
				break;
			case 8:
				s = "\"=\" expected";
				break;
			case 9:
				s = "\"function\" expected";
				break;
			case 10:
				s = "\"(\" expected";
				break;
			case 11:
				s = "\",\" expected";
				break;
			case 12:
				s = "\")\" expected";
				break;
			case 13:
				s = "\"endFunc\" expected";
				break;
			case 14:
				s = "\"endIf\" expected";
				break;
			case 15:
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
			case 20:
				s = "\"print\" expected";
				break;
			case 21:
				s = "\"while\" expected";
				break;
			case 22:
				s = "\"if\" expected";
				break;
			case 23:
				s = "\"else\" expected";
				break;
			case 24:
				s = "\":\" expected";
				break;
			case 25:
				s = "\">\" expected";
				break;
			case 26:
				s = "\"<\" expected";
				break;
			case 27:
				s = "\"!=\" expected";
				break;
			case 28:
				s = "\"==\" expected";
				break;
			case 29:
				s = "\".\" expected";
				break;
			case 30:
				s = "\"+\" expected";
				break;
			case 31:
				s = "\"-\" expected";
				break;
			case 32:
				s = "\"or\" expected";
				break;
			case 33:
				s = "\"*\" expected";
				break;
			case 34:
				s = "\"/\" expected";
				break;
			case 35:
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
			errorMessage += "line:" + line + " col:" + col + " error:" + s + "\n";
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
}