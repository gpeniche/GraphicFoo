using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	/// <summary>
	/// Program memory.
	/// </summary>
	public static class ProgramMemory
	{
		#region Initialize

		private const string temporaryPrefix = "temp";

		private static VariableBlock constants;
		private static VariableBlock globalTemporary;
		private static VariableBlock globalVariables;
		private static Dictionary <string, Procedure> procedures;

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public static void Initialize ()
		{
			constants = new VariableBlock ();
			globalTemporary = new VariableBlock ();
			globalVariables = new VariableBlock ();
			procedures = new Dictionary <string, Procedure> ();
		}

		#endregion

		#region Populate
		/// <summary>
		/// Adds a constant.
		/// </summary>
		/// <returns>The constant.</returns>
		/// <param name="id">Identifier.</param>
		/// <param name="type">Type.</param>
		public static Variable AddConstant (string id, GraphicFooType type)
		{
			if (constants.Contains (id)) {
				return constants.ReadVariable (id);
			}
			Variable variable = new Variable (id, type);
			constants.AddVariable (variable);
			return variable;
		}
		/// <summary>
		/// Adds a global variable.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="type">Type.</param>
		public static void AddGlobalVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			globalVariables.AddVariable (variable);
		}
		/// <summary>
		/// Adds a global temporary.
		/// </summary>
		/// <returns>The global temporary.</returns>
		/// <param name="type">Type.</param>
		public static Variable AddGlobalTemporary (GraphicFooType type)
		{
			string id = temporaryPrefix + globalTemporary.GetCount ();
			Variable variable = new Variable (id, type);
			globalTemporary.AddVariable (variable);
			return variable;
		}
		/// <summary>
		/// Adds a procedure.
		/// </summary>
		/// <returns>The procedure.</returns>
		/// <param name="id">Identifier.</param>
		/// <param name="type">Type.</param>
		/// <param name="parameterBlock">Parameter block.</param>
		public static Procedure AddProcedure (
			string id, 
			string type, 
			VariableBlock parameterBlock)
		{
			if (procedures.ContainsKey (id)) {
				// TODO sem error
				return null;
			}

			Procedure procedure = new Procedure (id, type, parameterBlock);
			procedures.Add (procedure.name, procedure);
			return procedure;
		}

		#endregion

		#region Access
		/// <summary>
		/// Reads a procedure.
		/// </summary>
		/// <returns>The procedure.</returns>
		/// <param name="procedureName">Procedure name.</param>
		public static Procedure ReadProcedure (string procedureName)
		{
			Procedure procedure = null;
			procedures.TryGetValue (procedureName, out procedure);

			if (procedure == null) {
				Console.WriteLine (
					"Procedure {0} not found", 
					procedureName
				);
			}
			return procedure;
		}
		/// <summary>
		/// Finds a variable on a given scope.
		/// </summary>
		/// <returns>The variable.</returns>
		/// <param name="scope">Scope.</param>
		/// <param name="id">Identifier.</param>
		public static Variable FindVariable (Procedure scope, string id)
		{
			Variable variable = null;

			// Variables are only found on a procedure if a scope is given
			if (scope != null) {
				variable = scope.ReadVariable (id);
				if (variable != null) {
					return variable;
				}
			}

			// Continue search on globals
			variable = globalVariables.ReadVariable (id);
			if (variable != null) {
				return variable;
			}

			variable = globalTemporary.ReadVariable (id);
			if (variable != null) {
				return variable;
			}

			variable = constants.ReadVariable (id);
			if (variable != null) {
				return variable;
			}

			Console.WriteLine (
				"Variable {0} not found on scope {1}", 
				id, 
				(scope == null) ? "global" : scope.name
			);
			return variable;
		}

		#endregion

		#region Execute
		/// <summary>
		/// Loads all constants for execution.
		/// </summary>
		public static void LoadConstants ()
		{
			constants.GroupByType ();
		}

		#endregion

		#region Debug

		/// <summary>
		/// Finds a variable, used only for debugging.
		/// </summary>
		/// <returns>The find variable.</returns>
		/// <param name="id">Identifier.</param>
		public static Variable DebugFindVariable (string id)
		{
			Variable variable = null;

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

			return variable;
		}

		/// <summary>
		/// Debugs the program memory.
		/// </summary>
		public static void DebugProgramMemory ()
		{
			string output = "\n=====\nProgram Memory\n=====\n";
			output += "Constants: " + constants.ToString ();
			output += "Global Variables: " + globalVariables.ToString ();
			output += "Global Temporaries: " + globalTemporary.ToString ();
			output += "Procedures: \n";
			foreach (Procedure procedure in procedures.Values) {
				output += procedure.ToString () + "\n";
			}
			Console.WriteLine (output);
		}

		#endregion
	}
}

