using System;
using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public static class ViewConstructorHelper
	{
		public static UIButton LoadMenuButton(){
			UIButton menuButton = new UIButton (UIButtonType.System);
			menuButton.Frame = new CGRect (700, 20, 50, 50);
			menuButton.SetImage (
				UIImage.FromBundle ("Graphics/menu.png"),
				UIControlState.Normal
			);
			return menuButton;
		}
	}
}

