using System;
using System.Collections.Generic;

namespace GraphicFoo
{
	/// <summary>
	/// Procedure.
	/// </summary>
	public class Procedure : Identifier
	{
		private const string temporaryPrefix = "temp";

		public string name;
		public GraphicFooType type;
		public int index;
		public int end;
		public bool isMain;
		public int callCount;
		private VariableBlock parameters;
		private VariableBlock procedureVariables;
		private VariableBlock temporaryVariables;

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphicFoo.Procedure"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="rawType">Raw type.</param>
		/// <param name="parameters">Parameters.</param>
		public Procedure (
			string name, 
			string rawType, 
			VariableBlock parameters)
		{
			this.name = name;
			this.type = ParseType (rawType);
			this.index = Quadruple.quadruples.Count;
			this.end = -1;
			this.isMain = name == "main";
			this.callCount = 0;

			if (isMain) {
				VirtualMachine.startOfMain = index;
				callCount = 1;
			}

			this.parameters = 
				(parameters == null) ? new VariableBlock () : parameters;
			this.procedureVariables = new VariableBlock ();
			this.temporaryVariables = new VariableBlock ();
		}

		/// <summary>
		/// Reads a variable from the procedure.
		/// </summary>
		/// <returns>The variable.</returns>
		/// <param name="id">Identifier.</param>
		public Variable ReadVariable (string id)
		{
			// Search first in parameters, then in local variables and last in
			// temporary variables
			Variable variable = parameters.ReadVariable (id);
			if (variable == null) {
				variable = procedureVariables.ReadVariable (id);
			}
			if (variable == null) {
				variable = temporaryVariables.ReadVariable (id);
			}
			return variable;
		}

		/// <summary>
		/// Adds a variable.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="type">Type.</param>
		public void AddVariable (string id, string type)
		{
			Variable variable = new Variable (id, type);
			procedureVariables.AddVariable (variable);
		}

		/// <summary>
		/// Adds a temporary variable.
		/// </summary>
		/// <returns>The temporary variable.</returns>
		/// <param name="type">Type.</param>
		public Variable AddTemporaryVariable (GraphicFooType type)
		{
			string id = temporaryPrefix + temporaryVariables.GetCount ();
			Variable variable = new Variable (id, type);
			temporaryVariables.AddVariable (variable);
			return variable;
		}

		/// <summary>
		/// Gets the function parameters.
		/// </summary>
		/// <returns>The parameters.</returns>
		public VariableBlock GetParameters ()
		{
			return parameters;
		}

		/// <summary>
		/// Gets the parameter count.
		/// </summary>
		/// <returns>The parameter count.</returns>
		public int GetParameterCount ()
		{
			return parameters.GetCount ();
		}

		/// <summary>
		/// Sets the last function quadruple.
		/// </summary>
		/// <param name="end">End.</param>
		public void SetEnd (int end) 
		{
			this.end = end;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Procedure"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="GraphicFoo.Procedure"/>.</returns>
		public override string ToString ()
		{
			return "[" + index + ", " + end + "] Function: " +
			type.ToString () + " " + name +
			"\nFunction parameters: " + parameters.ToString () +
			"Function variables: " + procedureVariables.ToString () +
			"Function temporaries: " + temporaryVariables.ToString ();
		}
	}
}
