using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class Procedure : Identifier
	{
		public string name;
		public GraphicFooType type;
		private VariableBlock procedureVariables;

		public Procedure (
			string name, 
			string rawType, 
			VariableBlock variableBlock)
		{
			this.name = name;
			this.type = ParseType (rawType);
			this.procedureVariables = 
				(variableBlock == null) ? new VariableBlock () : variableBlock;
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

		public override string ToString ()
		{
			return "Function: " + type.ToString () + " " + name +
			"\nFunction variables: " + procedureVariables.ToString ();
		}
	}
}
