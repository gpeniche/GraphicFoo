using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class InstructionsController : BaseController
	{
		public InstructionsController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.Black;

			UILabel title = new UILabel (new CGRect (
				                (View.Frame.Width / 2) - 160,
				                25,
				                320,
				                30)
			                );
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Text = "Instructions";
			title.Font = UIFont.FromName ("Orange Kid", 40f);

			UITextView body = new UITextView (new CGRect (
				                  50,
				                  120,
				                  View.Frame.Width - 100,
				                  View.Frame.Height - 240)
			                  );
			body.BackgroundColor = UIColor.Black;
			body.TextColor = UIColor.FromRGB (191, 222, 227);
			body.Font = UIFont.SystemFontOfSize (24.0f);
			body.TextAlignment = UITextAlignment.Center;
			body.Editable = false;
			body.Layer.BorderWidth = 5f;
			body.Layer.BorderColor = new CGColor (191, 222, 227);
			body.Font = UIFont.FromName ("Orange Kid", 28f);
			body.Text = " \n" +
			"- Start off by selecting a function block from your left" +
			" then to keep adding blocks just select the place to" +
			" add the new block and then what type of block you want to add" +
			" \n\n\n - If you want to remove a block just press on the" +
			" cross of which block you want to delete \n\n\n - " +
			" Once you're satisfied with your code your compile and run it," +
			" to do that, just click on the play button\n\n\n - The space" +
			" below your canvas is the Console, the place where you" +
			" see the output or errors, if any, of your program \n\n\n" +
			" - There are specific places to declare your variables," +
			" it can be either at the very top of the entire canvas, or" +
			" at the top of every function you create \n\n\n - To" +
			" get information and an explanation of each block, just " +
			" pressed for a couple of seconds the block you're " +
			" interested in, then the option of more info will " +
			" appear, click on it and you'll get more information about that" +
			" block and it's functionality \n\n\n" +
			" - The last function you create has to be the Main";

			UIButton menuButton = ViewConstructorHelper.LoadMenuButton ();
			menuButton.TouchUpInside += (sender, e) => {
				SidebarController.ToggleMenu ();
			};

			View.Add (title);
			View.Add (body);
			View.Add (menuButton);
		}
	}
}

