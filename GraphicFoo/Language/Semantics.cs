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
					"Type mismatch expected " + expected + ", found " + actual
				);
			}
			return match;
		}

		public static SemanticEnum ValidateReturn (
			GraphicFooType procedureType, 
			Variable returnVariable)
		{
			bool noReturn = (returnVariable == null);
			if (procedureType == GraphicFooType.Void) {
				if (!noReturn) {
					Console.WriteLine ("Void functions can't return a type");
					return SemanticEnum.CantHaveReturn;
				}
				return SemanticEnum.ValidReturn;
			} else {
				if (noReturn) {
					Console.WriteLine ("Missing return type");
					return SemanticEnum.MissingReturnType;
				}
				bool match = (procedureType == returnVariable.type);
				if (!match) {
					Console.WriteLine ("Return mismatch");
					return SemanticEnum.TypeMismatch;
				}
				return SemanticEnum.ValidReturn;
			}
		}
	}
}

