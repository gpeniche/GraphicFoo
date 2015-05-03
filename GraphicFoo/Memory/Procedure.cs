using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Procedure : Identifier
	{
		private const string temporaryPrefix = "temp";

		public string name;
		public GraphicFooType type;
		public int index;
		private VariableBlock parameters;
		private VariableBlock procedureVariables;
		private VariableBlock temporaryVariables;

		public Procedure (
			string name, 
			string rawType, 
			VariableBlock parameters)
		{
			this.name = name;
			this.type = ParseType (rawType);
			this.index = Quadruple.quadruples.Count;
			this.parameters = 
				(parameters == null) ? new VariableBlock () : parameters;
			this.procedureVariables = new VariableBlock ();
			this.temporaryVariables = new VariableBlock ();
		}

		public Variable ReadVariable (string id)
		{
			Variable variable = parameters.ReadVariable (id);
			if (variable == null) {
				variable = procedureVariables.ReadVariable (id);
			}
			if (variable == null) {
				variable = temporaryVariables.ReadVariable (id);
			}
			return variable;
		}

		public void AddVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			procedureVariables.AddVariable (variable);
		}

		public Variable AddTemporaryVariable (GraphicFooType type)
		{
			string id = temporaryPrefix + temporaryVariables.GetCount ();
			Variable variable = new Variable (id, type);
			temporaryVariables.AddVariable (variable);
			return variable;
		}

		public VariableBlock GetParameters ()
		{
			return parameters;
		}

		public int GetParameterCount ()
		{
			return parameters.GetCount ();
		}

		public override string ToString ()
		{
			return "[" + index + "] Function: " +
			type.ToString () + " " + name +
			"\nFunction parameters: " + parameters.ToString () +
			"Function variables: " + procedureVariables.ToString () +
			"Function temporaries: " + temporaryVariables.ToString ();
		}
	}
}
