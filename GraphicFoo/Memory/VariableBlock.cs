using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class VariableBlock
	{
		private Dictionary <string, Variable> variables;

		public VariableBlock ()
		{
			variables = new Dictionary <string, Variable> ();
		}

		private void AddVariable (Variable variable)
		{
			variables.Add (variable.name, variable);
		}

		private Variable ReadVariable (string variableName)
		{
			return variables [variableName];
		}
	}
}

