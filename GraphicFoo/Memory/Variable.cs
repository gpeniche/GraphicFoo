using System;

namespace GraphicFoo
{
	public class Variable
	{
		public string name;
		public GraphicFooType type;
		public int dir;

		public Variable (string name, GraphicFooType type, int dir)
		{
			this.name = name;
			this.type = type;
			this.dir = dir;
		}
	}
}

