using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	public class Quadruple
	{
		public static Procedure scope = null;

		public static Stack<string> operandStack;
		public static Stack<GraphicFooType> typeStack;
		public static Stack<Operators> operatorStack;
		public static Stack<int> hierarchyStack;
		public static Stack<int> jumpStack;
		public static Dictionary<int, Quadruple> quadruples;

		public Operators op;
		public Variable v1, v2;
		public Variable target;
		public Procedure call;
		public int jumpIndex;

		#region Class Methods

		private Quadruple (
			Operators op,
			Procedure call)
		{
			this.op = op;
			this.v1 = null;
			this.v2 = null;
			this.target = null;
			this.call = call;
			this.jumpIndex = -1;
		}

		private Quadruple (
			Operators op,
			Variable v1)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = null;
			this.target = null;
			this.call = null;
			this.jumpIndex = -1;
		}

		private Quadruple (
			Operators op, 
			Variable v1, 
			Variable target)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = null;
			this.call = null;
			this.target = target;
			this.jumpIndex = -1;
		}

		private Quadruple (
			Operators op, 
			Variable v1, 
			Variable v2, 
			Variable target)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = v2;
			this.call = null;
			this.target = target;
			this.jumpIndex = -1;
		}

		public override string ToString ()
		{
			return op.ToString () + " " +
			((call == null) ? "" : call.name) + " " +
			((v1 == null) ? "" : v1.ToString ()) + " " +
			((v2 == null) ? "" : v2.ToString ()) + " " +
			((target == null) ? "" : target.ToString ()) + " " +
			((jumpIndex != -1) ? jumpIndex.ToString () : "");
		}

		public static void DebugQuadruples ()
		{
			string output = "\n=====\nGenerated Quadruples\n=====";
			for (int i = 0; i < quadruples.Count; i++) {
				output += "\n[" + i + "] " + quadruples [i].ToString ();
			}
			Console.WriteLine (output);
		}

		#endregion

		#region Static Methods

		public static void Initialize ()
		{
			// Stacks
			operandStack = new Stack<string> ();
			typeStack = new Stack<GraphicFooType> ();
			operatorStack = new Stack<Operators> ();
			hierarchyStack = new Stack<int> ();
			jumpStack = new Stack<int> ();
			quadruples = new Dictionary<int, Quadruple> ();
			AssociationRules.Initialize ();
		}

		public static void PushQuadruple (Quadruple quaduple)
		{
			quadruples.Add (quadruples.Count, quaduple);
		}

		#region Assigantion Quadruples

		public static void CreateAssignationQuadruple (
			string variableId, 
			string targetId)
		{
			Variable variable = ProgramMemory.FindVariable (scope, variableId);
			Variable target = ProgramMemory.FindVariable (scope, targetId);
			Quadruple quadruple = 
				new Quadruple (Operators.Assignation, variable, target);
			PushQuadruple (quadruple);
		}

		#endregion

		#region Expression Quadruples

		private static void CreateExpressionQuadruple (
			Operators[] operators)
		{
			int index = (hierarchyStack.Count > 0) ? hierarchyStack.Peek () : 0;
			if (operatorStack.Count > index &&
			    operators.Contains (operatorStack.Peek ())) {
				GraphicFooType t2 = typeStack.Pop ();
				GraphicFooType t1 = typeStack.Pop ();

				if (t1 != GraphicFooType.Invalid &&
				    t2 != GraphicFooType.Invalid) {

					string id2 = operandStack.Pop ();
					string id1 = operandStack.Pop ();

					Variable v1 = ProgramMemory.FindVariable (scope, id1);
					Variable v2 = ProgramMemory.FindVariable (scope, id2);

					Operators op = operatorStack.Pop ();

					GraphicFooType resultingType = 
						AssociationRules.GetOperationType (op, t1, t2);

					Variable temp;
					if (scope == null) {
						temp = ProgramMemory.AddGlobalTemporary (resultingType);
					} else {
						temp = scope.AddTemporaryVariable (resultingType);
					}
					operandStack.Push (temp.name);
					typeStack.Push (temp.type);

					Quadruple qudruple = new Quadruple (
						                     op, 
						                     v1, 
						                     v2,
						                     temp	
					                     );
					PushQuadruple (qudruple);
				}
			} 
		}

		public static void CreateAdditiveQuadruple ()
		{
			CreateExpressionQuadruple (
				new Operators[] { 
					Operators.Plus, 
					Operators.Minus, 
					Operators.Or 
				});
		}

		public static void CreateMultiplicativeQuadruple ()
		{
			CreateExpressionQuadruple (
				new Operators[] {
					Operators.Multiplication,
					Operators.Division,
					Operators.And
				});
		}

		public static void CreateRelationalQuadruple ()
		{
			CreateExpressionQuadruple (
				new Operators[] {
					Operators.Greater,
					Operators.Lesser,
					Operators.Unequal,
					Operators.Equal
				});
		}

		#endregion

		#region Jump Quadruples

		private static void CreateJumpQuadruple (
			Operators goTo, 
			Variable condition = null)
		{
			Quadruple quadruple = new Quadruple (goTo, condition);
			PushQuadruple (quadruple);
		}

		public static void PushJump ()
		{
			jumpStack.Push (quadruples.Count - 1);
		}

		public static bool PopJump (int offset = 0)
		{
			try {
				int jumpIndex = jumpStack.Pop ();
				quadruples [jumpIndex].jumpIndex = quadruples.Count + offset;
				return true;
			} catch (InvalidOperationException) {
				return false;
			}
		}

		public static void PopTwoJumps ()
		{
			int firstJump = jumpStack.Pop ();
			int secondJump = jumpStack.Pop ();
			quadruples [firstJump + 1].jumpIndex = quadruples.Count;
			quadruples [quadruples.Count - 1].jumpIndex = secondJump + 1;
		}

		public static void CreateGotoQuadruple ()
		{
			CreateJumpQuadruple (Operators.Goto);
		}

		public static void CreateGotoFalseQuadruple (string id)
		{
			Variable condition = ProgramMemory.FindVariable (scope, id);
			CreateJumpQuadruple (Operators.GotoF, condition);

		}

		#endregion

		#region Procedure Quadruples

		private static void MatchParameters (
			Procedure procedure, 
			VariableBlock parameters)
		{
			int procedureParameterCount = procedure.GetParameterCount ();
			int parameterCallCount = parameters.GetCount ();

			if (procedureParameterCount != parameterCallCount) {
				Console.WriteLine (
					"Call to {1}, with wrong numer of parameters ({2})", 
					procedure.name, 
					parameterCallCount
				);
				return;
			}

			List<Variable> parameterList = parameters.ToList ();
			List<Variable> procedureParameterList = 
				procedure.GetParameters ().ToList ();
			
			for (int i = 0; i < parameterCallCount; i++) {
				if (parameterList [i].type == procedureParameterList [i].type) {
					// TODO define paramX
					Quadruple param = 
						new Quadruple (Operators.Param, parameterList [i]);
					PushQuadruple (param);
				} else {
					Console.WriteLine (
						"On call to {1}, parameter [{2}] {3} does not match the expected type {4}", 
						procedure.name, 
						i,
						procedureParameterList [i].name,
						procedureParameterList [i].type.ToString ()
					);
					return;
				}
			}
		}

		public static SemanticEnum CreateReturnQuadruple (string id)
		{
			Variable returnVariable = null;
			if (id != "") {
				returnVariable = ProgramMemory.FindVariable (scope, id);
			}
			SemanticEnum returnStatus = Semantics.ValidateReturn (scope.type, returnVariable);
			if (returnStatus == SemanticEnum.ValidReturn) {
				Quadruple quadruple = 
					new Quadruple (Operators.Return, returnVariable);
				PushQuadruple (quadruple);
			}
			return returnStatus;
		}

		public static void CreateFunctionCallQuadruples (
			string id, 
			VariableBlock parameters)
		{
			Procedure procedure = ProgramMemory.ReadProcedure (id);
			Quadruple expand = new Quadruple (Operators.Expand, procedure);
			PushQuadruple (expand);

			MatchParameters (procedure, parameters);

			Quadruple goSub = new Quadruple (Operators.GoSub, procedure);
			PushQuadruple (goSub);
		}

		#endregion

		#region Other Quadruples

		public static void CreatePrintQuadruple (string exprId)
		{
			Variable expression = ProgramMemory.FindVariable (scope, exprId);
			Quadruple quadruple = new Quadruple (Operators.Print, expression);
			PushQuadruple (quadruple);
		}

		#endregion

		#endregion
	}
}

