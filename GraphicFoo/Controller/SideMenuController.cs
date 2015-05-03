using System;
using UIKit;
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

			UILabel title = new UILabel (new CGRect (0, 50, 270, 20));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Menu";

			UIButton introButton = new UIButton (UIButtonType.System);
			introButton.Frame = new CGRect (0, 180, 260, 20);
			introButton.SetTitle ("Reset", UIControlState.Normal);
			introButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new IntroController ());

			UIButton testButton = new UIButton (UIButtonType.System);
			testButton.Frame = new CGRect (0, 220, 260, 20);
			testButton.SetTitle ("TestView", UIControlState.Normal);
			testButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new ContentController ());

			UIButton instructionsButton = new UIButton (UIButtonType.System);
			instructionsButton.Frame = new CGRect (0, 260, 260, 20);
			instructionsButton.SetTitle ("Instructions", UIControlState.Normal);
			instructionsButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new InstructionsController ());

			View.Add (title);
			View.Add (introButton);
			View.Add (testButton);
			View.Add (instructionsButton);

		}
	}
}

