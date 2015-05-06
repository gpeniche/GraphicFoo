using System;

namespace GraphicFoo
{
	/// <summary>
	/// Semantics.
	/// </summary>
	public static class Semantics
	{
		/// <summary>
		/// Matches an expected type.
		/// </summary>
		/// <returns><c>true</c>, if type was expected, <c>false</c> otherwise.</returns>
		/// <param name="expected">Expected.</param>
		/// <param name="actual">Actual.</param>
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

		/// <summary>
		/// Validates the return type of a function.
		/// </summary>
		/// <returns>The return.</returns>
		/// <param name="procedureType">Procedure type.</param>
		/// <param name="returnVariable">Return variable.</param>
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

