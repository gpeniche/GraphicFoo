using System;

namespace GraphicFoo
{
	/// <summary>
	/// Identifier.
	/// </summary>
	public abstract class Identifier
	{
		/// <summary>
		/// Parses a type to a GraphicFooType.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="type">Type.</param>
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

