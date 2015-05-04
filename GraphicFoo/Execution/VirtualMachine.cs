using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	public static class VirtualMachine
	{

		#region Main

		public static int startOfMain;
		private static Stack<int> goSubJumps;
		private static Stack<Dictionary<int, Quadruple>> programStack;

		public static void Execute ()
		{
			Load ();
			Run (Quadruple.quadruples, startOfMain);
		}

		private static void Load ()
		{
			goSubJumps = new Stack<int> ();
			programStack = new Stack<Dictionary<int, Quadruple>> ();
			programStack.Push (Quadruple.quadruples);
			if (startOfMain == -1) {
				Console.WriteLine ("Execution error: Main procedure not found");
			}
			LoadConstants ();
		}

		private static void LoadConstants ()
		{
			ProgramMemory.LoadConstants ();
		}

		private static void Run (
			Dictionary<int, Quadruple> quadruples, 
			int start)
		{
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
				case Operators.Param:
					q.target.value = q.v1.value;
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
					Run (programStack.Peek (), q.call.index);
					index++;
					break;
				case Operators.Return:
					if (goSubJumps.Count == 0) {
						// TODO End Execution
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
			}
		}

		#endregion

		#region Operations

		private static void ExecuteArithmeticOperation (Quadruple q)
		{
			float? v1Value = CastToNumeric (q.v1);
			float? v2Value = CastToNumeric (q.v2);

			if (v1Value == null || v2Value == null) {
				Console.WriteLine ("Execution error: Number cast failed");
				//TODO return?
			}

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

		private static void ExecuteRelationalOperation (Quadruple q)
		{
			float? v1Value = CastToNumeric (q.v1);
			float? v2Value = CastToNumeric (q.v2);

			if (v1Value == null || v2Value == null) {
				Console.WriteLine ("Execution error: Number cast failed");
				//TODO return?
			}

			switch (q.op) {
			case Operators.Greater:
				q.target.value = v1Value > v2Value;
				break;
			case Operators.Lesser:
				q.target.value = v1Value < v2Value;
				break;
			}
		}

		private static void ExecuteEqualityOperation (Quadruple q)
		{
			// TODO sanitize nulls
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

		private static void ExecuteLogicalOperation (Quadruple q)
		{
			bool? v1Value = CastToBoolean (q.v1);
			bool? v2Value = CastToBoolean (q.v2);

			bool v1, v2;

			if (v1Value == null || v2Value == null) {
				Console.WriteLine ("Execution error: Boolean cast failed");
				//TODO return?
			}

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

		private static bool ExecuteGotoOperation (Quadruple q, bool condition)
		{
			bool? v1Value = CastToBoolean (q.v1);
			bool v1;

			if (v1Value == null) {
				Console.WriteLine ("Execution error: Boolean cast failed");
				//TODO return?
			}

			v1 = v1Value ?? default(bool);
			return v1 == condition;
		}

		private static void ExecuteProcedureExpansion (Quadruple q)
		{
			Procedure p = q.call;
			Dictionary<int, Quadruple> quadruples = 
				Quadruple.quadruples.
				Where (s => s.Key >= p.index && s.Key <= p.end).
				ToDictionary (dict => dict.Key, dict => dict.Value);
		
			// TODO set variables
			programStack.Push (quadruples);
		}

		private static void Print (Quadruple q)
		{
			Type printType = q.v1.GetNativeType ();

			if (printType == typeof(float)) {
				Console.WriteLine ("Program Output: " + (q.v1.value as float?));
			} else if (printType == typeof(bool)) {
				Console.WriteLine ("Program Output: " + (q.v1.value as bool?));
			} else if (printType == typeof(string)) {
				Console.WriteLine ("Program Output: " + (q.v1.value as string));
			}
		}

		#endregion

		#region Casting

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

