using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class ProgramMemory
	{
		private VariableBlock constants;
		private VariableBlock globalVariables;
		private Dictionary <string, Procedure> procedures;

		public ProgramMemory ()
		{
			constants = new VariableBlock ();
			globalVariables = new VariableBlock ();
			procedures = new Dictionary <string, Procedure> ();
		}

		public Variable AddConstant (string id, GraphicFooType type)
		{
			if (constants.Contains (id)) {
				return constants.ReadVariable (id);
			}
			Variable variable = new Variable (id, type);
			constants.AddVariable (variable);
			return variable;
		}

		public void AddGlobalVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			globalVariables.AddVariable (variable);
		}

		public Procedure AddProcedure (
			string id, 
			string type, 
			VariableBlock variableBlock = null)
		{
			Procedure procedure = new Procedure (id, type, variableBlock);
			procedures.Add (procedure.name, procedure);
			return procedure;
		}

		private Procedure ReadProcedure (string procedureName)
		{
			return procedures [procedureName];
		}

		public Variable DebugFindVariable (string id)
		{
			Variable variable;
			foreach (Procedure procedure in procedures.Values) {
				variable = procedure.ReadVariable (id);
				if (variable != null) {
					return variable;
				}
			}

			variable = globalVariables.ReadVariable (id);

			if (variable != null) {
				return variable;
			}

			return null;
		}

		public override string ToString ()
		{
			string output = "\n";
			output += "Constants: " + constants.ToString ();
			output += "Global Variables: " + globalVariables.ToString ();
			output += "Procedures: \n";
			foreach (Procedure procedure in procedures.Values) {
				output += procedure.ToString () + "\n";
			}
			return output;
		}
	}
}

