using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	/// <summary>
	/// Variable block this is a a wrapper for a dictionary contiang a set of variables.
	/// </summary>
	public class VariableBlock
	{
		/// <summary>
		/// The variable dictionary.
		/// </summary>
		private Dictionary <string, Variable> variables;

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.VariableBlock"/> class.
		/// </summary>
		public VariableBlock ()
		{
			variables = new Dictionary <string, Variable> ();
		}

		/// <summary>
		/// Adds a variable.
		/// </summary>
		/// <param name="variable">Variable.</param>
		public void AddVariable (Variable variable)
		{
			variables.Add (variable.name, variable);
		}

		/// <summary>
		/// Reads a variable.
		/// </summary>
		/// <returns>The variable.</returns>
		/// <param name="variableName">Variable name.</param>
		public Variable ReadVariable (string variableName)
		{
			Variable value = null;
			variables.TryGetValue (variableName, out value);
			return value;
		}

		/// <summary>
		/// Contains the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public bool Contains (string key)
		{
			return variables.ContainsKey (key);
		}

		/// <summary>
		/// Gets the variable at index.
		/// </summary>
		/// <returns>The <see cref="GraphicFoo.Variable"/>.</returns>
		/// <param name="index">Index.</param>
		public Variable GetVariableAt (int index)
		{
			return variables.Skip (index).First ().Value;
		}

		/// <summary>
		/// Returns variables as a list.
		/// </summary>
		/// <returns>The list.</returns>
		public List<Variable> ToList ()
		{
			return new List<Variable> (variables.Values);
		}

		/// <summary>
		/// Groups the variables by type, used to initialize constants.
		/// </summary>
		public void GroupByType ()
		{
			// Get dictionary structure
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

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <returns>The count.</returns>
		public int GetCount ()
		{
			return variables.Count;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="GraphicFoo.VariableBlock"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="GraphicFoo.VariableBlock"/>.</returns>
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

