using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class AboutController : BaseController
	{
		public AboutController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.Black;

			UILabel title = new UILabel (new CGRect (
				                (View.Frame.Width / 2) - 160,
				                50,
				                320,
				                30)
			                );
			title.Font = UIFont.SystemFontOfSize (36.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Text = "About Graphic Foo";

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
			body.Layer.BorderColor = new CGColor(191, 222, 227);
			body.Text = " \n" +
			"- Graphic is an app which is intented for people that is just" +
			" starting to discover the programming as an excellent tool" +
			" to create and be creative nowadays, so Graphic Foo tries" +
			" to be an easy and friendly interface to start creating and" +
			" developing those early skills \n\n" +
			"- The creators of Graphic Foo wish you a really pleasant time" +
			" using it and that you can become a great programmer and one" +
			" day a leader on the programming world \n\n" +
			" Enjoy it!";

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

