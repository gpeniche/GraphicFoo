using System;
using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public static class ViewConstructorHelper
	{
		public static UIButton LoadMenuButton ()
		{
			UIButton menuButton = new UIButton (UIButtonType.Custom);
			menuButton.Frame = new CGRect (670, 0, 100, 120);
			menuButton.SetImage (
				UIImage.FromBundle ("Graphics/menu.png"),
				UIControlState.Normal
			);
			return menuButton;
		}
	}
}

