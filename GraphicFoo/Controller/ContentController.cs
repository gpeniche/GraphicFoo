using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;

namespace GraphicFoo
{
	public class ContentController : BaseController
	{
		public ContentController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.White;

			UILabel title = new UILabel (new RectangleF (0, 50, 320, 30));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Sidebar Navigation";

			UILabel body = new UILabel (new RectangleF (50, 120, 220, 100));
			body.Font = UIFont.SystemFontOfSize (12.0f);
			body.TextAlignment = UITextAlignment.Center;
			body.Lines = 0;
			body.Text = @"This is the content view controller.";

			UIButton menuButton = new UIButton (UIButtonType.System);
			menuButton.Frame = new RectangleF (50, 250, 220, 30);
			menuButton.SetTitle ("Toggle Side Menu", UIControlState.Normal);
			menuButton.TouchUpInside += (sender, e) => {
				SidebarController.ToggleMenu ();
			};

			UIButton runButton = new UIButton (UIButtonType.Custom);
			runButton.Frame = new RectangleF (600, 20, 60, 45);
			runButton.SetTitle ("Run", UIControlState.Normal);
			runButton.SetImage (
				UIImage.FromBundle ("play-button.png"),
				UIControlState.Normal
			);

			UITextField codeTextField = new UITextField ();
			codeTextField.Frame = new RectangleF (310, 350, 220, 30);
			codeTextField.Placeholder = "Add code";

			runButton.TouchUpInside += (sender, e) => {
				string input = codeTextField.Text;
				new UIAlertView ("Code", input, null, "OK", null).Show ();
				Scanner scanner = new Scanner (input);
				Parser parser = new Parser (scanner);
				parser.Parse ();
				string errorMessage = 
					(!string.IsNullOrEmpty (parser.errors.errorMessage)) ? 
					parser.errors.errorMessage : 
					"None";
				new UIAlertView ("Errors", errorMessage, null, "OK", null).Show ();
			};

			View.Add (codeTextField);
			View.Add (runButton);
			View.Add (title);
			View.Add (body);
			View.Add (menuButton);
		}
	}
}

