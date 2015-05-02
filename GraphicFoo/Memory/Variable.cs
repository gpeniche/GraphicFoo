using System;

namespace GraphicFoo
{
	public class Variable : Identifier
	{
		public string name;
		public GraphicFooType type;
		public dynamic value = null;

		public Variable (string name, GraphicFooType type)
		{
			this.name = name;
			this.type = type;
		}

		public Variable (string name, string rawType)
		{
			this.name = name;
			this.type = ParseType (rawType);
		}

		public override string ToString ()
		{
			return "[" + type.ToString () + " " + name + "]";
		}

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

