using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	/// <summary>
	/// Variable.
	/// </summary>
	public class Variable : Identifier
	{
		public string name;
		public GraphicFooType type;
		public dynamic value = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Variable"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="type">Type.</param>
		public Variable (string name, GraphicFooType type)
		{
			this.name = name;
			this.type = type;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Variable"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="rawType">Raw type.</param>
		public Variable (string name, string rawType)
		{
			this.name = name;
			this.type = ParseType (rawType);
		}

		/// <summary>
		/// Clone the specified variable, used to allocate memory.
		/// </summary>
		/// <param name="variable">Variable.</param>
		/// <param name="withValue">If set to <c>true</c> with value.</param>
		public static Variable Clone (Variable variable, bool withValue = false)
		{
			Variable v = new Variable (variable.name, variable.type);

			if (withValue) {
				v.value = variable.value;
			}

			return v;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Variable"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Variable"/>.</returns>
		public override string ToString ()
		{
			return "[" + type.ToString () + " " + name + "]";
		}

		/// <summary>
		/// Translates the type of the variable to a c# native type
		/// </summary>
		/// <returns>The native type.</returns>
		public Type GetNativeType ()
		{
			if (type == GraphicFooType.Void) {
				return typeof(void);
			} else if (type == GraphicFooType.Boolean) {
				return typeof(bool);
			} else if (type == GraphicFooType.Number) {
				return typeof(float);
			} else {
				return typeof(string);
			}
		}
	}
}

