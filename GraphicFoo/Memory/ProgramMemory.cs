using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class ProgramMemory
	{
		private VariableBlock globalVariables;
		private Dictionary <string, Procedure> procedures;

		public ProgramMemory ()
		{
			globalVariables = new VariableBlock ();
			procedures = new Dictionary <string, Procedure> ();
		}

		public void AddGlobalVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			globalVariables.AddVariable (variable);
		}

		public Procedure AddProcedure (string id, string type, VariableBlock variableBlock = null)
		{
			Procedure procedure = new Procedure (id, type, variableBlock);
			procedures.Add (procedure.name, procedure);
			return procedure;
		}

		private Procedure ReadProcedure (string procedureName)
		{
			return procedures [procedureName];
		}

		public override string ToString ()
		{
			string output = "\n";
			output += "Global Variables: " + globalVariables.ToString ();
			output += "Procedures: \n";
			foreach (Procedure procedure in procedures.Values)
			{
				output += procedure.ToString () + "\n";
			}
			return output;
		}
	}
}

