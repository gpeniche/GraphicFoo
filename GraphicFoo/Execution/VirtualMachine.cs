using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	public static class VirtualMachine
	{

		#region Main

		private static Stack<VariableBlock> clones;

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
			clones = new Stack<VariableBlock> ();
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
					q.target.value = q.v1.value;
					index++;
					break;
				case Operators.Param:
//					programStack.Peek ()[index].target.value = q.v1.value;
					clones.Peek ().ReadVariable (q.target.name).value = q.v1.value;
//					clones.ReadVariable (q.target.name).value = clones.ReadVariable (q.v1.name).value;
//								Console.WriteLine (q.v1.name + " :Val: " + (q.v1.value as float?));
//					Console.WriteLine (q.v1.name + " :Val: " + (clones.Peek ().ReadVariable (q.v1.name).value as float?));
//					Console.WriteLine (q.target.name + " :Param: " + (q.target.value as float?));
//					try {
//						var temp = clones.Pop ();
//						if (temp != null) {
//							Console.WriteLine (q.target.name + " :Param: " + (clones.Peek ().ReadVariable (q.target.name).value as float?));
//							clones.Push (temp);
//						}
//					} catch (Exception) {
//					}
//								Console.WriteLine (q.target.name + " :Param: " + (clones.Peek ().ReadVariable (q.target.name).value as float?));
//					q.target.value = q.v1.value;
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
					q.call.callCount++;
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

		#region add

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
//				Console.WriteLine ("Add v1: " + (v1Value as float?));
//				Console.WriteLine ("Add v2: " + (v2Value as float?));
//				Console.WriteLine ("Add res: " + (q.target.value as float?));
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

		#endregion

		public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>
		(Dictionary<TKey, TValue> original) where TValue : ICloneable
		{
			Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue> (original.Count,
				                               original.Comparer);
			foreach (KeyValuePair<TKey, TValue> entry in original) {
				ret.Add (entry.Key, (TValue)entry.Value.Clone ());
			}
			return ret;
		}

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
//			DebugQuadruples (quadruples);
			programStack.Push (quadruples);
			return;

//			Dictionary<int, Quadruple> quadruples = 
//				Quadruple.quadruples.
//				Where (s => s.Key >= p.index && s.Key <= p.end).
//				ToDictionary (dict => dict.Key, dict => dict.Value);
//


			// TODO set variables
			foreach (KeyValuePair<int, Quadruple> pair in quadruples) {

				Quadruple quadruple = pair.Value;
//				if (quadruple.op != Operators.Param) {
//				Console.WriteLine (pair.Key);
				if (quadruple.v1 != null && quadruple.v1.name == "x" && quadruple.op == Operators.Param) {
					Console.WriteLine (quadruple.v1.value as float?);
				}
				quadruple.v1 = CloneOrFindVariable (quadruple.v1, quadruple.op == Operators.Param);
				if (quadruple.v1 != null && quadruple.v1.name == "x" && quadruple.op == Operators.Param) {
					Console.WriteLine (quadruple.v1.value as float?);
				}
				quadruple.v2 = CloneOrFindVariable (quadruple.v2);
//				if (quadruple.op != Operators.Param)
				quadruple.target = CloneOrFindVariable (quadruple.target);


//				}




//				if (quadurple.v1 != null) {
//					quadurple.v1 = ProgramMemory.FindVariable (null, quadurple.v1.name);
//					if (quadurple.v1 == null)
//						quadurple.v1 = clones.ReadVariable (quadurple.v1.name);
//					if (quadurple.v1 == null) {
//						quadurple.v1 = Variable.Clone (quadurple.v1);
//					}
//				}
//
//				if (quadurple.v2 != null) {
//					quadurple.v2 = ProgramMemory.FindVariable (null, quadurple.v2.name);
//					if (quadurple.v2 == null) {
//						quadurple.v2 = Variable.Clone (quadurple.v2);
//					}
//				}
//
//				if (quadurple.target != null) {
//					quadurple.target = ProgramMemory.FindVariable (null, quadurple.target.name);
//					if (quadurple.target == null) {
//						quadurple.target = Variable.Clone (quadurple.target);
//					}
//				}
			}
			DebugQuadruples (quadruples);
			programStack.Push (quadruples);
		}

		private static Variable CloneOrFindVariable (Variable v, bool withValue = false)
		{
			if (v != null) {
				Variable found = ProgramMemory.FindVariable (null, v.name);
				if (found == null) {
					Variable read = clones.Peek ().ReadVariable (v.name);

					if (read == null) {
						v = Variable.Clone (v, withValue);
						clones.Peek ().AddVariable (v);
//						Console.WriteLine (v.name + " not found on clones " + clones.GetCount ());
//						Console.WriteLine ("======\n" + clones.ToString() + "\n=======");
					} else {
						if (read.name == "x") {
							Console.WriteLine ("read val: " + (read.value as float?));
						}
						return read;
					}
				} else {
					return v;
				}
			}
			return v;
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

