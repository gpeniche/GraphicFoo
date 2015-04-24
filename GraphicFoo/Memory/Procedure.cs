using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Procedure : Identifier
	{
		private const string temporalPrefix = "temp";

		public string name;
		public GraphicFooType type;
		private VariableBlock procedureVariables;
		private VariableBlock temporalVariables;

		public Procedure (
			string name, 
			string rawType, 
			VariableBlock variableBlock)
		{
			this.name = name;
			this.type = ParseType (rawType);
			this.procedureVariables = 
				(variableBlock == null) ? new VariableBlock () : variableBlock;
			this.temporalVariables = new VariableBlock ();
		}

		public void AddVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			procedureVariables.AddVariable (variable);
		}

		public Variable ReadVariable (string id)
		{
			return procedureVariables.ReadVariable (id);
		}

		public Variable AddTemporalVariable (GraphicFooType type)
		{
			string id = temporalPrefix + temporalVariables.Count ();
			Variable variable = new Variable (id, type);
			temporalVariables.AddVariable (variable);
			return variable;

		}

		public override string ToString ()
		{
			return "Function: " + type.ToString () + " " + name +
			"\nFunction variables: " + procedureVariables.ToString ();
		}
	}
}
