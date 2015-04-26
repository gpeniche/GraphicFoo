using System;

namespace GraphicFoo
{
	public static class Semantics
	{
		public static bool ExpectType (
			GraphicFooType expected, 
			GraphicFooType actual) 
		{
			if (expected != actual) {
				// TODO return error
			}

			return expected == actual;
		}
	}
}

