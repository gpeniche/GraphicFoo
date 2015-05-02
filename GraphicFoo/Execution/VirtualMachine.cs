using System;

namespace GraphicFoo
{
	public static class VirtualMachine
	{
		public static void Execute ()
		{
			Load ();
			Run ();
		}

		private static void Load ()
		{
			LoadConstants ();
		}

		private static void LoadConstants ()
		{
			ProgramMemory.LoadConstants ();
		}

		private static void Run ()
		{
			foreach (Quadruple q in Quadruple.quadruples) {
				switch (q.op) {
				case Operators.Plus:
				case Operators.Minus:
				case Operators.Multiplication:
				case Operators.Division:
					ExecuteArithmeticOperation (q);
					break;
				case Operators.Assignation:
					q.target.value = q.v1.value;
					break;
				case Operators.Print:
					Print (q);
					break;
				default:
					break;
				}
			}
		}

		private static void ExecuteArithmeticOperation (Quadruple q)
		{
			float? v1Value = CastVariable (q.v1);
			float? v2Value = CastVariable (q.v2);

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

		private static float? CastVariable (Variable v)
		{
			if (v.GetNativeType () == typeof(bool)) {
				bool? v1BoolValue = (v.value as bool?);
				if (v1BoolValue == true) {
					return 1f;
				} else if (v1BoolValue == false) {
					return 0f;
				} else {
					return null;
				}
			} else {
				return (v.value as float?);
			}
		}

		private static void Print (Quadruple q)
		{
			Type printType = q.v1.GetNativeType ();

			if (printType == typeof(float)) {
				Console.WriteLine ("Program Output:" + (q.v1.value as float?));
			} else if (printType == typeof(bool)) {
				Console.WriteLine ("Program Output:" + (q.v1.value as bool?));
			} else if (printType == typeof(string)) {
				Console.WriteLine ("Program Output:" + (q.v1.value as string));
			}
		}
	}
}

