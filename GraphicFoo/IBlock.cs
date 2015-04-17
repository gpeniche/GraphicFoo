using System;
using UIKit;

namespace GraphicFoo
{
	public interface IBlock
	{
		string Name { get; }

		string Syntax { get; }

		UIImage Image { get; }
	}
}
