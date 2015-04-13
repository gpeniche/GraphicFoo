using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class ProgramMemory
	{
		public VariableBlock globalVariables;
		private Dictionary <string, Procedure> procedures;

		public ProgramMemory ()
		{
			globalVariables = new VariableBlock ();
			procedures = new Dictionary <string, Procedure> ();
		}

		private void AddProcedure (Procedure procedure)
		{
			procedures.Add (procedure.name, procedure);
		}

		private Procedure ReadProcedure (string procedureName)
		{
			return procedures [procedureName];
		}
	}
}

