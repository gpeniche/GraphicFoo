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
		public static List<Quadruple> quadruples;

		public Operators op;
		public Variable v1, v2;
		public Variable target;
		public int jumpIndex;

		#region Class Methods

		private Quadruple (
			Operators goTo,
			Variable condition)
		{
			this.op = goTo;
			this.v1 = condition;
			this.v2 = null;
			this.target = null;
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
			this.target = target;
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
			this.target = target;
		}

		public override string ToString ()
		{
			return "Quadruple:\n" +
			op.ToString () + " " +
			((v1 == null) ? "" : v1.ToString ()) + " " +
			((v2 == null) ? "" : v2.ToString ()) + " " +
			((target != null) ? target.ToString () : jumpIndex.ToString ());
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
			quadruples = new List<Quadruple> ();
			AssociationRules.Initialize ();
		}

		public static void PushQuadruple (Quadruple quaduple)
		{
			quadruples.Add (quaduple);
		}

		#region Assigantion Quadruples

		public static void CreateAssignationQuadruple (
			string variable, 
			string target)
		{
			Quadruple quadruple = null;
			//new Quadruple (Operators.Assignation, ;
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

		public static void PopJump ()
		{
			int jumpIndex = jumpStack.Pop ();
			quadruples [jumpIndex].jumpIndex = quadruples.Count;
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

		#endregion
	}
}

