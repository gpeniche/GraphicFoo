using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	/// <summary>
	/// Quadruple.
	/// </summary>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="call">Call.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="v1">V1.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="v1">V1.</param>
		/// <param name="target">Target.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="v1">V1.</param>
		/// <param name="call">Call.</param>
		private Quadruple (
			Operators op, 
			Variable v1, 
			Procedure call)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = null;
			this.call = call;
			this.target = null;
			this.jumpIndex = -1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		/// <param name="target">Target.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Quadruple"/> class.
		/// Only public method, used for memory allocation
		/// </summary>
		/// <param name="op">Op.</param>
		/// <param name="v1">V1.</param>
		/// <param name="v2">V2.</param>
		/// <param name="target">Target.</param>
		/// <param name="call">Call.</param>
		/// <param name="jumpIndex">Jump index.</param>
		public Quadruple (
			Operators op, 
			Variable v1, 
			Variable v2, 
			Variable target,
			Procedure call,
			int jumpIndex)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = v2;
			this.call = call;
			this.target = target;
			this.jumpIndex = jumpIndex;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Quadruple"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Quadruple"/>.</returns>
		public override string ToString ()
		{
			return op.ToString () + " " +
			((call == null) ? "" : call.name) + " " +
			((v1 == null) ? "" : v1.ToString ()) + " " +
			((v2 == null) ? "" : v2.ToString ()) + " " +
			((target == null) ? "" : target.ToString ()) + " " +
			((jumpIndex != -1) ? jumpIndex.ToString () : "");
		}

		/// <summary>
		/// Debugs the quadruples.
		/// </summary>
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

		/// <summary>
		/// Initialize this instance.
		/// </summary>
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

		/// <summary>
		/// Pushes a quadruple to the main dictionary.
		/// </summary>
		/// <param name="quaduple">Quaduple.</param>
		public static void PushQuadruple (Quadruple quaduple)
		{
			quadruples.Add (quadruples.Count, quaduple);
		}

		#region Assigantion Quadruples

		/// <summary>
		/// Creates an assignation quadruple.
		/// </summary>
		/// <param name="variableId">Variable identifier.</param>
		/// <param name="targetId">Target identifier.</param>
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

		/// <summary>
		/// Creates a return assignation quadruple.
		/// </summary>
		/// <param name="variableId">Variable identifier.</param>
		/// <param name="targetId">Target identifier.</param>
		public static void CreateReturnAssignationQuadruple (
			string variableId, 
			string targetId)
		{
			Procedure variable = ProgramMemory.ReadProcedure (variableId);
			Variable target = ProgramMemory.FindVariable (scope, targetId);
			Quadruple quadruple = 
				new Quadruple (Operators.ReturnAssignation, target, variable);
			PushQuadruple (quadruple);
		}

		#endregion

		#region Expression Quadruples

		/// <summary>
		/// Creates an expression quadruple.
		/// </summary>
		/// <param name="operators">Operators.</param>
		private static void CreateExpressionQuadruple (
			Operators[] operators)
		{
			// Get index of fake bottom
			int index = (hierarchyStack.Count > 0) ? hierarchyStack.Peek () : 0;
			if (operatorStack.Count > index &&
			    operators.Contains (operatorStack.Peek ())) {
				// Pop types
				GraphicFooType t2 = typeStack.Pop ();
				GraphicFooType t1 = typeStack.Pop ();

				if (t1 != GraphicFooType.Invalid &&
				    t2 != GraphicFooType.Invalid) {

					// Pop operands
					string id2 = operandStack.Pop ();
					string id1 = operandStack.Pop ();

					// Find variables
					Variable v1 = ProgramMemory.FindVariable (scope, id1);
					Variable v2 = ProgramMemory.FindVariable (scope, id2);

					// Pop operator
					Operators op = operatorStack.Pop ();

					// Check association rules
					GraphicFooType resultingType = 
						AssociationRules.GetOperationType (op, t1, t2);

					// Create new temporary variable
					Variable temp;
					if (scope == null) {
						temp = ProgramMemory.AddGlobalTemporary (resultingType);
					} else {
						temp = scope.AddTemporaryVariable (resultingType);
					}
					// Push temp
					operandStack.Push (temp.name);
					typeStack.Push (temp.type);

					// Create quadruple
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

		/// <summary>
		/// Creates an additive quadruple.
		/// </summary>
		public static void CreateAdditiveQuadruple ()
		{
			CreateExpressionQuadruple (
				new Operators[] { 
					Operators.Plus, 
					Operators.Minus, 
					Operators.Or 
				});
		}

		/// <summary>
		/// Creates an multiplicative quadruple.
		/// </summary>
		public static void CreateMultiplicativeQuadruple ()
		{
			CreateExpressionQuadruple (
				new Operators[] {
					Operators.Multiplication,
					Operators.Division,
					Operators.And
				});
		}

		/// <summary>
		/// Creates a relational quadruple.
		/// </summary>
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

		/// <summary>
		/// Creates a jump quadruple.
		/// </summary>
		/// <param name="goTo">Go to.</param>
		/// <param name="condition">Condition.</param>
		private static void CreateJumpQuadruple (
			Operators goTo, 
			Variable condition = null)
		{
			Quadruple quadruple = new Quadruple (goTo, condition);
			PushQuadruple (quadruple);
		}

		/// <summary>
		/// Pushes a jump.
		/// </summary>
		public static void PushJump ()
		{
			jumpStack.Push (quadruples.Count - 1);
		}

		/// <summary>
		/// Pops a jump.
		/// </summary>
		/// <returns><c>true</c>, if jump was poped, <c>false</c> otherwise.</returns>
		/// <param name="offset">Offset.</param>
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

		/// <summary>
		/// Pops two jumps, used on if-else blocks.
		/// </summary>
		public static void PopTwoJumps ()
		{
			int firstJump = jumpStack.Pop ();
			int secondJump = jumpStack.Pop ();
			quadruples [firstJump + 1].jumpIndex = quadruples.Count;
			quadruples [quadruples.Count - 1].jumpIndex = secondJump + 1;
		}

		/// <summary>
		/// Creates a goto quadruple.
		/// </summary>
		public static void CreateGotoQuadruple ()
		{
			CreateJumpQuadruple (Operators.Goto);
		}

		/// <summary>
		/// Creates a goto false quadruple.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public static void CreateGotoFalseQuadruple (string id)
		{
			Variable condition = ProgramMemory.FindVariable (scope, id);
			CreateJumpQuadruple (Operators.GotoF, condition);

		}

		#endregion

		#region Procedure Quadruples

		/// <summary>
		/// Matchs the function parameters.
		/// </summary>
		/// <param name="procedure">Procedure.</param>
		/// <param name="parameters">Parameters.</param>
		private static void MatchParameters (
			Procedure procedure, 
			VariableBlock parameters)
		{
			// Check parameter count consistency
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

			// Assign parameters
			for (int i = 0; i < parameterCallCount; i++) {
				if (parameterList [i].type == procedureParameterList [i].type) {
					// TODO define paramX
					Quadruple param = 
						new Quadruple (
							Operators.Param, 
							ProgramMemory.FindVariable (
								scope, 
								parameterList [i].name
							),
							procedure.GetParameters ().GetVariableAt (i)
						);
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

		/// <summary>
		/// Creates a return quadruple.
		/// </summary>
		/// <returns>The return quadruple.</returns>
		/// <param name="id">Identifier.</param>
		public static SemanticEnum CreateReturnQuadruple (string id)
		{
			Variable returnVariable = null;
			if (id != "") {
				returnVariable = ProgramMemory.FindVariable (scope, id);
			}
			// Validate return type
			SemanticEnum returnStatus = Semantics.ValidateReturn (scope.type, returnVariable);
			if (returnStatus == SemanticEnum.ValidReturn) {
				scope.SetEnd (Quadruple.quadruples.Count);
				Quadruple quadruple = 
					new Quadruple (Operators.Return, returnVariable);
				PushQuadruple (quadruple);
			}
			return returnStatus;
		}

		/// <summary>
		/// Creates the function call quadruples.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="parameters">Parameters.</param>
		public static void CreateFunctionCallQuadruples (
			string id, 
			VariableBlock parameters)
		{
			Procedure procedure = ProgramMemory.ReadProcedure (id);
			// Expand procedure
			Quadruple expand = new Quadruple (Operators.Expand, procedure);
			PushQuadruple (expand);
			// Assign parameters
			MatchParameters (procedure, parameters);
			// Go subroutine
			Quadruple goSub = new Quadruple (Operators.GoSub, procedure);
			PushQuadruple (goSub);
		}

		#endregion

		#region Other Quadruples

		/// <summary>
		/// Creates a print quadruple.
		/// </summary>
		/// <param name="exprId">Expr identifier.</param>
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

