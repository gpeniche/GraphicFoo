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

		public void AddVariable (Variable variable)
		{
			variables.Add (variable.name, variable);
		}

		public Variable ReadVariable (string variableName)
		{
			Variable value = null;
			variables.TryGetValue (variableName, out value);
			return value;
		}

		public bool Contains (string key)
		{
			return variables.ContainsKey (key);
		}

		public List<Variable> ToList ()
		{
			return new List<Variable>(variables.Values);
		}

		public int Count ()
		{
			return variables.Count;
		}

		public override string ToString ()
		{
			string output = "\n";
			foreach (Variable variable in variables.Values) {
				output += variable.ToString () + "\n";
			}
			return output;
		}
	}
}

