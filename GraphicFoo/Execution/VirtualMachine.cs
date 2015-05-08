using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	/// <summary>
	/// Virtual machine.
	/// </summary>
	public static class VirtualMachine
	{

		#region Main

		public static int startOfMain;
		private static Stack<VariableBlock> clones;
		private static Stack<int> goSubJumps;
		private static Stack<Dictionary<int, Quadruple>> programStack;
		private static Stack<Variable> returns;

		private static Dictionary<int, Quadruple> exec;
		public static string output;

		private static bool end;

		/// <summary>
		/// Executes the virtual machine.
		/// </summary>
		public static void Execute ()
		{
			Load ();
			Run (Quadruple.quadruples, startOfMain);
		}
			

		/// <summary>
		/// Load this instance.
		/// </summary>
		private static void Load ()
		{
			goSubJumps = new Stack<int> ();
			programStack = new Stack<Dictionary<int, Quadruple>> ();
			clones = new Stack<VariableBlock> ();
			programStack.Push (Quadruple.quadruples);
			exec = new Dictionary<int, Quadruple> ();
			returns = new Stack<Variable> ();
			output = "\n";
			end = false;
			if (startOfMain == -1) {
				Console.WriteLine ("Execution error: Main procedure not found");
			}
			LoadConstants ();
		}

		/// <summary>
		/// Loads the program constants.
		/// </summary>
		private static void LoadConstants ()
		{
			ProgramMemory.LoadConstants ();
		}

		/// <summary>
		/// Run the specified quadruples at start.
		/// </summary>
		/// <param name="quadruples">Quadruples.</param>
		/// <param name="start">Start.</param>
		private static void Run (
			Dictionary<int, Quadruple> quadruples, 
			int start)
		{
		
			end = false;
			int index = start;

			while (true) {
				if (!quadruples.ContainsKey (index)) {
					return;
				}
				Quadruple q = quadruples [index];
				Console.WriteLine (
					"Executing [" + index + "/" + (quadruples.Count - 1) + "]"
				);
				switch (q.op) {
				case Operators.Assignation:
					q.target.value = q.v1.value;
					index++;
					break;
				case Operators.ReturnAssignation:
					try {
						q.v1.value = returns.Pop ().value;
						Console.WriteLine (q.v1.value as float?);
					} catch (InvalidOperationException) {
					}
					index++;
					break;
				case Operators.Param:
					clones.Peek ().ReadVariable (q.target.name).value = q.v1.value;
					index++;
					break;
				case Operators.Plus:
				case Operators.Minus:
				case Operators.Multiplication:
				case Operators.Division:
					ExecuteArithmeticOperation (q);
					index++;
					break;
				case Operators.Greater:
				case Operators.Lesser:
					ExecuteRelationalOperation (q);
					index++;
					break;
				case Operators.Equal:
				case Operators.Unequal:
					ExecuteEqualityOperation (q);
					index++;
					break;
				case Operators.Or:
				case Operators.And:
					ExecuteLogicalOperation (q);
					index++;
					break;
				case Operators.Goto:
					index = q.jumpIndex;
					break;
				case Operators.GotoT:
					index = 
						(ExecuteGotoOperation (q, true)) ? 
						q.jumpIndex : 
						index + 1;
					break;
				case Operators.GotoF:
					index = 
						(ExecuteGotoOperation (q, false)) ? 
						q.jumpIndex : 
						index + 1;
					break;
				case Operators.Expand:
					ExecuteProcedureExpansion (q);
					index++;
					break;
				case Operators.GoSub:
					goSubJumps.Push (index);
					exec = programStack.Peek ();
					Run (exec, q.call.index);
					index++;
					break;
				case Operators.Return:
					if (q.v1 != null) {
						returns.Push (q.v1);
					}

					if (goSubJumps.Count == 0) {
						end = true;
						index++;
						return;
					} else {
						index = goSubJumps.Pop () + 1;
						Run (programStack.Pop (), index);
					}
					break;
				case Operators.Print:
					Print (q);
					index++;
					break;
				default:
					index++;
					break;
				}
				if (end && index == (quadruples.Count)) {
					Console.WriteLine ("kill switch");
					return;
				}
			}
		}

		#endregion

		#region Operations

		/// <summary>
		/// Executes an arithmetic operation.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void ExecuteArithmeticOperation (Quadruple q)
		{
			float? v1Value = CastToNumeric (q.v1);
			float? v2Value = CastToNumeric (q.v2);

			switch (q.op) {
			case Operators.Plus:
				q.target.value = v1Value + v2Value;
				break;
			case Operators.Minus:
				q.target.value = v1Value - v2Value;
				break;
			case Operators.Multiplication:
				q.target.value = v1Value * v2Value;
				break;
			case Operators.Division:
				try {
					q.target.value = v1Value / v2Value;
				} catch (DivideByZeroException) {
					Console.WriteLine (
						"Execution error: Division of {0} by zero.", 
						v1Value
					);
				}
				break;
			}
		}

		/// <summary>
		/// Executes a relational operation.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void ExecuteRelationalOperation (Quadruple q)
		{
			float? v1Value = CastToNumeric (q.v1);
			float? v2Value = CastToNumeric (q.v2);

			switch (q.op) {
			case Operators.Greater:
				q.target.value = v1Value > v2Value;
				break;
			case Operators.Lesser:
				q.target.value = v1Value < v2Value;
				break;
			}
		}

		/// <summary>
		/// Executes an equality operation.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void ExecuteEqualityOperation (Quadruple q)
		{
			if (q.v1.GetNativeType () == typeof(float)) {
				q.target.value = 
					(q.v1.value as float?) == (q.v2.value as float?);
			} else if (q.v1.GetNativeType () == typeof(bool)) {
				q.target.value = 
					(q.v1.value as bool?) == (q.v2.value as bool?);
			} else if (q.v1.GetNativeType () == typeof(string)) {
				q.target.value = 
					(q.v1.value as string).Equals (q.v2.value as string);
			} 

			if (q.op == Operators.Unequal) {
				q.target.value = !(q.target.value as bool?);
			}
		}

		/// <summary>
		/// Executes a logical operation.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void ExecuteLogicalOperation (Quadruple q)
		{
			bool? v1Value = CastToBoolean (q.v1);
			bool? v2Value = CastToBoolean (q.v2);

			bool v1, v2;

			v1 = v1Value ?? default(bool);
			v2 = v2Value ?? default(bool);

			switch (q.op) {
			case Operators.Or:
				q.target.value = v1 || v2;
				break;
			case Operators.And:
				q.target.value = v1 && v2;
				break;
			}
		}

		/// <summary>
		/// Executes a goto operation.
		/// </summary>
		/// <returns><c>true</c>, if goto operation was executed, <c>false</c> otherwise.</returns>
		/// <param name="q">Q.</param>
		/// <param name="condition">If set to <c>true</c> condition.</param>
		private static bool ExecuteGotoOperation (Quadruple q, bool condition)
		{
			bool? v1Value = CastToBoolean (q.v1);
			bool v1;

			v1 = v1Value ?? default(bool);
			return v1 == condition;
		}

		/// <summary>
		/// Executes a procedure expansion.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void ExecuteProcedureExpansion (Quadruple q)
		{
			Procedure p = q.call;
			Dictionary<int, Quadruple> quadruples = new Dictionary<int, Quadruple> ();
			VariableBlock block = new VariableBlock ();
			clones.Push (block);

			for (int i = p.index; i <= p.end; i++) {

				Quadruple original = Quadruple.quadruples [i];
				Quadruple clone = new Quadruple (
					                  original.op,
					                  CloneOrFindVariable (original.v1),
					                  CloneOrFindVariable (original.v2),
					                  CloneOrFindVariable (original.target),
					                  original.call,
					                  original.jumpIndex
				                  );
				quadruples.Add (i, clone);
			}
			programStack.Push (quadruples);
			return;

		}

		/// <summary>
		/// Clones or finds a variable to expand or allocate memory.
		/// </summary>
		/// <returns>The or find variable.</returns>
		/// <param name="v">V.</param>
		/// <param name="withValue">If set to <c>true</c> with value.</param>
		private static Variable CloneOrFindVariable (Variable v, bool withValue = false)
		{
			if (v != null) {
				Variable found = ProgramMemory.FindVariable (null, v.name);
				if (found == null) {
					Variable read = clones.Peek ().ReadVariable (v.name);

					if (read == null) {
						v = Variable.Clone (v, withValue);
						clones.Peek ().AddVariable (v);
					} else {
						return read;
					}
				} else {
					return v;
				}
			}
			return v;
		}

		/// <summary>
		/// Prints program output.
		/// </summary>
		/// <param name="q">Q.</param>
		private static void Print (Quadruple q)
		{
			Type printType = q.v1.GetNativeType ();

			if (printType == typeof(float)) {
				output += "Program Output: " + (q.v1.value as float?) + "\n";
				Console.WriteLine ("Program Output: " + (q.v1.value as float?));
			} else if (printType == typeof(bool)) {
				output += "Program Output: " + (q.v1.value as bool?) + "\n";
				Console.WriteLine ("Program Output: " + (q.v1.value as bool?));
			} else if (printType == typeof(string)) {
				Console.WriteLine ("Program Output: " + (q.v1.value as string));
				output += "Program Output: " + (q.v1.value as string) + "\n";
			}
		}

		#endregion

		#region Casting

		/// <summary>
		/// Casts to boolean.
		/// </summary>
		/// <returns>The to boolean.</returns>
		/// <param name="v">V.</param>
		private static bool? CastToBoolean (Variable v)
		{
			Type type = v.GetNativeType ();
			if (type == typeof(float)) {
				float? value = (v.value as float?);
				if (value == null) {
					return null;
				} else if (value == 0f) {
					return false;
				} else {
					return true;
				}
			} else if (type == typeof(string)) {
				string value = (v.value as string);
				if (value == null) {
					return null;
				} else if (value == "") {
					return false;
				} else {
					return true;
				}
			} else {
				return (v.value as bool?);
			}
		}

		/// <summary>
		/// Casts to numeric.
		/// </summary>
		/// <returns>The to numeric.</returns>
		/// <param name="v">V.</param>
		private static float? CastToNumeric (Variable v)
		{
			Type type = v.GetNativeType ();
			if (type == typeof(string)) {
				string value = (v.value as string);
				if (value == null) {
					return null;
				} else {
					return (float)value.Length;
				}
			} else if (type == typeof(bool)) {
				bool? value = (v.value as bool?);
				if (value == true) {
					return 1f;
				} else if (value == false) {
					return 0f;
				} else {
					return null;
				}
			} else {
				return (v.value as float?);
			}
		}

		#endregion

		#region Debug

		/// <summary>
		/// Debugs quadruples.
		/// </summary>
		/// <param name="quadruples">Quadruples.</param>
		private static void DebugQuadruples (
			Dictionary<int, Quadruple> quadruples)
		{
			string output = "\n=====\nSubroutine Quadruples\n=====";
			foreach (KeyValuePair<int, Quadruple> pair in quadruples) {
				output += "\n[" + pair.Key + "] " + pair.Value.ToString ();
			}
			Console.WriteLine (output);
		}

		#endregion
	}
}

