using System;
using System.Collections;

namespace GraphicFoo
{
	public static class AssociationRules
	{

		const int varCount = 5;
		static Hashtable[,] rules;

		public static void Initialize ()
		{
			rules = new Hashtable[varCount, varCount];

			for (int i = 0; i < varCount; i++) {
				rules [(int)GraphicFooType.Invalid, i] = 
					rules [i, (int)GraphicFooType.Invalid] = null;
				rules [(int)GraphicFooType.Void, i] = 
					rules [i, (int)GraphicFooType.Void] = null;
			}

			rules [(int)GraphicFooType.Boolean, (int)GraphicFooType.Boolean] = 
				new Hashtable () {
				#region Assignment
				//				Assignation,
				#endregion
				#region Additive
				{ Operators.Plus, GraphicFooType.Number },
				{ Operators.Minus, GraphicFooType.Number },
				{ Operators.Concatenation, GraphicFooType.String },
				#endregion
				#region Multiplicative
				{ Operators.Multiplication, GraphicFooType.Number },
				{ Operators.Division, GraphicFooType.Number },
				#endregion
				#region Relational
				{ Operators.Greater, GraphicFooType.Boolean },
				{ Operators.Lesser, GraphicFooType.Boolean },
				#endregion
				#region Equality
				{ Operators.Equal, GraphicFooType.Boolean },
				{ Operators.Unequal, GraphicFooType.Boolean },
				#endregion
				#region Logical
				{ Operators.Or, GraphicFooType.Boolean },
				{ Operators.And, GraphicFooType.Boolean },
				#endregion
				#region Jumps
				//				Goto,
				//				GotoT,
				//				GotoF,
				#endregion
				#region Procedures
				//				Expand,
				//				GoSub,
				//				Return,
				#endregion
				#region Tools
				//				Write,
				#endregion
			};
			rules [(int)GraphicFooType.Number, (int)GraphicFooType.Number] = 
				new Hashtable () {
				#region Assignment
//				Assignation,
				#endregion
				#region Additive
				{ Operators.Plus, GraphicFooType.Number },
				{ Operators.Minus, GraphicFooType.Number },
				{ Operators.Concatenation, GraphicFooType.String },
				#endregion
				#region Multiplicative
				{ Operators.Multiplication, GraphicFooType.Number },
				{ Operators.Division, GraphicFooType.Number },
				#endregion
				#region Relational
				{ Operators.Greater, GraphicFooType.Boolean },
				{ Operators.Lesser, GraphicFooType.Boolean },
				#endregion
				#region Equality
				{ Operators.Equal, GraphicFooType.Boolean },
				{ Operators.Unequal, GraphicFooType.Boolean },
				#endregion
				#region Logical
				{ Operators.Or, GraphicFooType.Boolean },
				{ Operators.And, GraphicFooType.Boolean },
				#endregion
				#region Jumps
//				Goto,
//				GotoT,
//				GotoF,
				#endregion
				#region Procedures
//				Expand,
//				GoSub,
//				Return,
				#endregion
				#region Tools
//				Write,
				#endregion
			};
			rules [(int)GraphicFooType.String, (int)GraphicFooType.String] = 
				new Hashtable () {
				#region Assignment
				//				Assignation,
				#endregion
				#region Additive
//				{ Operators.Plus, GraphicFooType.Number },
//				{ Operators.Minus, GraphicFooType.Number },
//				{ Operators.Concatenation, GraphicFooType.String },
				#endregion
				#region Multiplicative
//				{ Operators.Multiplication, GraphicFooType.Number },
//				{ Operators.Division, GraphicFooType.Number },
				#endregion
				#region Relational
				{ Operators.Greater, GraphicFooType.Boolean },
				{ Operators.Lesser, GraphicFooType.Boolean },
				#endregion
				#region Equality
				{ Operators.Equal, GraphicFooType.Boolean },
				{ Operators.Unequal, GraphicFooType.Boolean },
				#endregion
				#region Logical
				{ Operators.Or, GraphicFooType.Boolean },
				{ Operators.And, GraphicFooType.Boolean },
				#endregion
				#region Jumps
				//				Goto,
				//				GotoT,
				//				GotoF,
				#endregion
				#region Procedures
				//				Expand,
				//				GoSub,
				//				Return,
				#endregion
				#region Tools
				//				Write,
				#endregion
			};

			rules [(int)GraphicFooType.Boolean, (int)GraphicFooType.Number] = 
				rules [(int)GraphicFooType.Number, (int)GraphicFooType.Boolean] =
					new Hashtable () {
					#region Assignment
				//				Assignation,
					#endregion
					#region Additive
				{ Operators.Plus, GraphicFooType.Number },
				{ Operators.Minus, GraphicFooType.Number },
				{ Operators.Concatenation, GraphicFooType.String },
					#endregion
					#region Multiplicative
				{ Operators.Multiplication, GraphicFooType.Number },
				{ Operators.Division, GraphicFooType.Number },
					#endregion
					#region Relational
				{ Operators.Greater, GraphicFooType.Boolean },
				{ Operators.Lesser, GraphicFooType.Boolean },
					#endregion
					#region Equality
				// TODO remove this mix
				{ Operators.Equal, GraphicFooType.Boolean },
				{ Operators.Unequal, GraphicFooType.Boolean },
					#endregion
					#region Logical
				{ Operators.Or, GraphicFooType.Boolean },
				{ Operators.And, GraphicFooType.Boolean },
					#endregion
					#region Jumps
				//				Goto,
				//				GotoT,
				//				GotoF,
					#endregion
					#region Procedures
				//				Expand,
				//				GoSub,
				//				Return,
					#endregion
					#region Tools
				//				Write,
					#endregion
			};
			rules [(int)GraphicFooType.Boolean, (int)GraphicFooType.String] = 
				rules [(int)GraphicFooType.String, (int)GraphicFooType.Boolean] =
				null;
			rules [(int)GraphicFooType.Number, (int)GraphicFooType.String] = 
				rules [(int)GraphicFooType.String, (int)GraphicFooType.Number] =
					null;

		}

		public static GraphicFooType GetOperationType (
			Operators op,
			GraphicFooType t1,
			GraphicFooType t2)
		{
			if (rules [(int)t1, (int)t2] == null) {
				Console.WriteLine ("No rule for [{1},{2}] is not found.", 
					t1.ToString (), 
					t2.ToString ()
				);
				// TODO return errors
				return t1;
			} else if (!rules [(int)t1, (int)t2].ContainsKey (op)) {
				Console.WriteLine ("Key \"{1}\" not found.", op.ToString ());
				// TODO return errors
				return t1;
			} else {
				return (GraphicFooType)rules [(int)t1, (int)t2] [op];
			}
		}
	}
}

