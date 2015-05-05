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
			View.BackgroundColor = UIColor.FromRGB (.4f, .4f, .4f);

			UILabel title = new UILabel (new CGRect (0, 50, 270, 20));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Text = "Menu";
			title.Font = UIFont.FromName ("Orange Kid", 34f);

			UIButton introButton = new UIButton (UIButtonType.System);
			introButton.Frame = new CGRect (0, 180, 260, 20);
			introButton.SetTitle ("Reset Canvas", UIControlState.Normal);
			introButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new IntroController ());
			introButton.SetTitleColor (UIColor.FromRGB (191, 222, 227), UIControlState.Normal);
			introButton.Font = UIFont.FromName ("Orange Kid", 28f);

			UIButton testButton = new UIButton (UIButtonType.System);
			testButton.Frame = new CGRect (0, 220, 260, 20);
			testButton.SetTitle ("Plain Code", UIControlState.Normal);
			testButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new PlainCodeController ());
			testButton.SetTitleColor (UIColor.FromRGB (191, 222, 227), UIControlState.Normal);
			testButton.Font = UIFont.FromName ("Orange Kid", 28f);

			UIButton instructionsButton = new UIButton (UIButtonType.System);
			instructionsButton.Frame = new CGRect (0, 260, 260, 20);
			instructionsButton.SetTitle ("Instructions", UIControlState.Normal);
			instructionsButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new InstructionsController ());
			instructionsButton.SetTitleColor (UIColor.FromRGB (191, 222, 227), UIControlState.Normal);
			instructionsButton.Font = UIFont.FromName ("Orange Kid", 28f);

			UIButton aboutButton = new UIButton (UIButtonType.System);
			aboutButton.Frame = new CGRect (0, 300, 260, 20);
			aboutButton.SetTitle ("About", UIControlState.Normal);
			aboutButton.TouchUpInside += (sender, e) => 
				SidebarController.ChangeContentView (new AboutController ());
			aboutButton.SetTitleColor (UIColor.FromRGB (191, 222, 227), UIControlState.Normal);
			aboutButton.Font = UIFont.FromName ("Orange Kid", 28f);

			View.Add (title);
			View.Add (introButton);
			View.Add (testButton);
			View.Add (instructionsButton);
			View.Add (aboutButton);

		}
	}
}

