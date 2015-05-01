using System;

namespace GraphicFoo
{
	public enum Operators
	{
		#region Assignment
		Assignation,
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
	}
}

