using System;

namespace GraphicFoo
{
	public static class Semantics
	{
		public static bool ExpectType (
			GraphicFooType expected, 
			GraphicFooType actual)
		{
			bool match = (expected == actual);
			if (!match) {
				Console.WriteLine (
					"Type mismatch expected {1}, found {2}", 
					expected, 
					actual
				);
			}
			return match;
		}

		public static bool ValidateReturn (
			GraphicFooType procedureType, 
			Variable returnVariable)
		{
			if (procedureType == GraphicFooType.Void) {
				bool noReturn = (returnVariable == null);
				if (!noReturn) {
					Console.WriteLine ("Void functions can't return a type");
				}
				return noReturn;
			} else {
				bool match = (procedureType == returnVariable.type);
				if (!match) {
					Console.WriteLine ("Return mismatch");
				}
				return match;
			}
		}
	}
}

