using System;
using System.Collections.Generic;
using System.Linq;

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

		public Variable GetVariableAt (int index) 
		{
			return variables.Skip (index).First ().Value;
		}

		public List<Variable> ToList ()
		{
			return new List<Variable> (variables.Values);
		}

		public void GroupByType ()
		{
			Dictionary<GraphicFooType, List<KeyValuePair<string, Variable>>> d = 
				variables.GroupBy (x => x.Value.type).
				ToDictionary (x => x.Key, x => x.ToList ());

			if (d.ContainsKey (GraphicFooType.Number)) {
				foreach (var v in d [GraphicFooType.Number]) {
					v.Value.value = (dynamic)float.Parse (v.Value.name);
				}
			}

			if (d.ContainsKey (GraphicFooType.String)) {
				foreach (var x in d [GraphicFooType.String]) {
					x.Value.value = (dynamic)x.Value.name;
				}
			}

			if (d.ContainsKey (GraphicFooType.Boolean)) {
				foreach (var x in d [GraphicFooType.Boolean]) {
					x.Value.value = (dynamic)Boolean.Parse (x.Value.name);
				}
			}
		}

		public int GetCount ()
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

