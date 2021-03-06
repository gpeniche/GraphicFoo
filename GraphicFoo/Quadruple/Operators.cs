﻿using System;

namespace GraphicFoo
{
	/// <summary>
	/// Operators enum.
	/// </summary>
	public enum Operators
	{
		#region Assignment

		Assignation,
		ReturnAssignation,
		#endregion

		#region Additive

		Plus,
		Minus,
		Concatenation,
		#endregion

		#region Multiplicative

		Multiplication,
		Division,
		#endregion

		#region Relational

		Greater,
		Lesser,
		#endregion

		#region Equality

		Equal,
		Unequal,
		#endregion

		#region Logical

		Or,
		And,
		#endregion

		#region Jumps

		Goto,
		GotoT,
		GotoF,
		#endregion

		#region Procedures

		Expand,
		Param,
		GoSub,
		Return,
		#endregion

		#region Tools

		Print,
		#endregion
		#region Invalid
		InvalidOperator,
		#endregion
	}
}

