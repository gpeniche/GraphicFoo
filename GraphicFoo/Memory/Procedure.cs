using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Procedure : Identifier
	{
		private const string temporaryPrefix = "temp";

		public string name;
		public GraphicFooType type;
		private VariableBlock procedureVariables;
		private VariableBlock temporaryVariables;

		public Procedure (
			string name, 
			string rawType, 
			VariableBlock variableBlock)
		{
			this.name = name;
			this.type = ParseType (rawType);
			this.procedureVariables = 
				(variableBlock == null) ? new VariableBlock () : variableBlock;
			this.temporaryVariables = new VariableBlock ();
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

		public Variable AddTemporaryVariable (GraphicFooType type)
		{
			string id = temporaryPrefix + temporaryVariables.Count ();
			Variable variable = new Variable (id, type);
			temporaryVariables.AddVariable (variable);
			return variable;
		}

		public override string ToString ()
		{
			return "Function: " + type.ToString () + " " + name +
			"\nFunction variables: " + procedureVariables.ToString () +
			"Function temporaries: " + temporaryVariables.ToString ();
		}
	}
}
