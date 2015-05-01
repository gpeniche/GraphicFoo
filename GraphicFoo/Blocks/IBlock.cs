using System;
using UIKit;

namespace GraphicFoo
{
	public interface IBlock
	{
		string Name { get; }

		string Syntax { get; }

		string Example { get; }

		string Explanation { get; }

		UIImage Image { get; }

		UIView BlockView { get; }
	}
}
