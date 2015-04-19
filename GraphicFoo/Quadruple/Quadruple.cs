using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	public class Quadruple
	{
		public static Stack<string> operandStack;
		public static Stack<GraphicFooType> typeStack;
		public static Stack<Operators> operatorStack;
		public static Stack<int> hierarchyStack;

		public Operators op;
		public string v1, v2;
		public Variable target;

		public Quadruple (
			Operators op, 
			string v1, 
			string v2, 
			Variable target)
		{
			this.op = op;
			this.v1 = v1;
			this.v2 = v2;
			this.target = target;
		}

		public static void Initialize ()
		{
			// Stacks
			operandStack = new Stack<string> ();
			typeStack = new Stack<GraphicFooType> ();
			operatorStack = new Stack<Operators> ();
			hierarchyStack = new Stack<int> ();
		}

		private static Quadruple CreateExpressionQuadruple (Operators[] operators)
		{
			int index = (hierarchyStack.Count > 0) ? hierarchyStack.Peek (): 0;
			if (operatorStack.Count > index && 
				 operators.Contains (operatorStack.Peek ())) {
				GraphicFooType t2 = typeStack.Pop ();
				GraphicFooType t1 = typeStack.Pop ();

				if (t1 != GraphicFooType.Invalid && 
					t2 != GraphicFooType.Invalid) {
					string v2 = operandStack.Pop ();
					string v1 = operandStack.Pop ();

					Variable temp = new Variable ("t", t1);
					operandStack.Push (temp.name);
					typeStack.Push (temp.type);

					return new Quadruple (
						operatorStack.Pop (), 
						v1, 
						v2,
						temp	
					);
				}
			} 
			return null;
		}

		public static Quadruple CreateAdditiveQuadruple ()
		{
			return CreateExpressionQuadruple (
				new Operators[] { 
					Operators.Plus, 
					Operators.Minus, 
					Operators.Or 
				});
		}

		public static Quadruple CreateMultiplicativeQuadruple ()
		{
			return CreateExpressionQuadruple (
				new Operators[] {
					Operators.Multiplication,
					Operators.Division,
					Operators.And
				});
		}

		public static Quadruple CreateRelationalQuadruple ()
		{
			return CreateExpressionQuadruple (
				new Operators[] {
					Operators.Greater,
					Operators.Lesser,
					Operators.Unequal,
					Operators.Equal
				});
		}

		public override string ToString ()
		{
			return "\nQuadruple:\n" +
			op.ToString () + " " +
			v1.ToString () + " " +
			v2.ToString () + " " +
			target.name;
		}
	}
}

