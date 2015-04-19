using System;

namespace GraphicFoo
{
	public class Variable : Identifier
	{
		public string name;
		public GraphicFooType type;

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
			return type.ToString () + " " + name;
		}
	}
}

