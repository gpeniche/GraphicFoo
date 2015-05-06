using UIKit;
using CoreGraphics;
using System.Linq;

namespace GraphicFoo
{
	public class PlainCodeController : BaseController
	{
		UITextView textOnConsole;
		UITextView programText;

		public PlainCodeController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UILabel title = new UILabel (new CGRect (100, 50, 320, 30));
			title.Font = UIFont.FromName ("Orange Kid", 44f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Text = "Graphic Foo";

			programText = new UITextView ();
			programText.Frame = new CGRect (
				0,
				100,
				View.Frame.Size.Width,
				View.Frame.Size.Height - 300f
			);
			programText.TextColor = UIColor.FromRGB (191, 222, 227);
			programText.BackgroundColor = UIColor.Black;
			programText.Font = UIFont.FromName ("Orange Kid", 22f);
			programText.TintColor = UIColor.FromRGB (191, 222, 227);
			programText.KeyboardAppearance = UIKeyboardAppearance.Dark;
			programText.Layer.BorderWidth = 2.0f;
			programText.Layer.BorderColor = new CGColor (191, 222, 227);

			UIButton menuButton = ViewConstructorHelper.LoadMenuButton ();
			menuButton.TouchUpInside += (sender, e) => SidebarController.ToggleMenu ();

			UIButton runButton = ViewConstructorHelper.CreatePlayButton ();
			runButton.TouchUpInside += (sender, e) => SendToCompile ();

			UIView consoleView = ViewConstructorHelper.CreateConsole (new CGRect (
				                     0,
				                     (float)View.Frame.Size.Height - 200f,
				                     (float)View.Frame.Size.Width,
				                     200f)
			                     );
			textOnConsole = ((UITextView)consoleView.Subviews.FirstOrDefault (v => v.Tag == 1));

			View.BackgroundColor = UIColor.Black;
			View.Add (runButton);
			View.Add (title);
			View.Add (programText);
			View.Add (menuButton);
			View.Add (consoleView);
		}

		/// <summary>
		/// Prepares the string to compile and sends it to the scanner.
		/// </summary>
		public void SendToCompile ()
		{
			textOnConsole.Text = "";
			string errorMessage = CompilingHelper.SendToCompilePlainCode (programText.Text);
			textOnConsole.Text = errorMessage;
		}
	}
}


