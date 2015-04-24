﻿using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;

namespace GraphicFoo
{
	public partial class SideMenuController : BaseController
	{
		
		public SideMenuController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.FromRGB (.9f, .9f, .9f);

			UILabel title = new UILabel (new RectangleF (0, 50, 270, 20));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Menu";

			UIButton introButton = new UIButton (UIButtonType.System);
			introButton.Frame = new RectangleF (0, 180, 260, 20);
			introButton.SetTitle ("Reset", UIControlState.Normal);
			introButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new IntroController ());

			UIButton contentButton = new UIButton (UIButtonType.System);
			contentButton.Frame = new RectangleF (0, 220, 260, 20);
			contentButton.SetTitle ("TestView", UIControlState.Normal);
			contentButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new ContentController ());

			View.Add (title);
			View.Add (introButton);
			View.Add (contentButton);

		}
	}
}

