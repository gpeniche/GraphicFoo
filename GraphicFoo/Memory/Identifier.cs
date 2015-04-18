using System;

namespace GraphicFoo
{
	public abstract class Identifier
	{
		protected GraphicFooType ParseType (string type) 
		{
			try {
				// Return char and concat substring.
				type = char.ToUpper(type[0]) + type.Substring(1);

				GraphicFooType graphicFooType = 
					(GraphicFooType)Enum.Parse (typeof(GraphicFooType), type);
				return graphicFooType;
			} catch (IndexOutOfRangeException) {
				Console.WriteLine ("String is empty");
				return GraphicFooType.Invalid;
			} catch (NullReferenceException) {
				Console.WriteLine ("String is null");
				return GraphicFooType.Invalid;
			} catch (ArgumentException) {
				Console.WriteLine ("Invalid Type");
				return GraphicFooType.Invalid;
			}
		}
	}
}

