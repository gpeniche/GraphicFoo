using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Procedure
	{
		public string name;
		public GraphicFooType type;
		public int dir;
		public VariableBlock procedureVariables;
		public VariableBlock temporalVariables;

		public Procedure (string name, GraphicFooType type, int dir)
		{
			this.name = name;
			this.type = type;
			this.dir = dir;
			this.procedureVariables = new VariableBlock ();
			this.temporalVariables = new VariableBlock ();
		}
	}
}
