using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace GraphicFoo
{
	public partial class GraphicFooViewController : UIViewController
	{
		public GraphicFooViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RunButton.TouchUpInside += delegate {
				string input = CodeTextField.Text;
				new UIAlertView ("Code", input, null, "OK", null).Show ();
				Scanner scanner = new Scanner (input);
				Parser parser = new Parser (scanner);
				parser.Parse ();
				string errorMessage = 
					(string.IsNullOrEmpty (parser.errors.errorMessage)) ? "None" : parser.errors.errorMessage;
				new UIAlertView ("Errors", errorMessage, null, "OK", null).Show ();
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

