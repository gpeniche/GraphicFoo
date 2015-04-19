using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Linq;

namespace GraphicFoo
{
	public partial class IntroController : BaseController
	{
		SimpleCollectionViewController simpleCollectionViewController;
		LineLayout lineLayout;
		UIScrollView scrollView;
		float insertPosition = 1;
		int[] blocksOnView = new int[2];

		public IntroController() : base(null, null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			scrollView = new UIScrollView ();
			scrollView.Frame = new RectangleF(0, 0, (float)View.Frame.Size.Width, (float)View.Frame.Size.Height);


			View.BackgroundColor = UIColor.White;

			var title = new UILabel(new RectangleF(260, 50, 320, 30));
			title.Font = UIFont.SystemFontOfSize(24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Sidebar Navigation";

			var unified = new UILabel(new RectangleF(260, 80, 320, 30));
			unified.Font = UIFont.SystemFontOfSize(24.0f);
			unified.TextAlignment = UITextAlignment.Center;
			unified.TextColor = UIColor.Blue;
			unified.Text = "Unified";

			var body = new UILabel(new RectangleF(310, 120, 220, 100));
			body.Font = UIFont.SystemFontOfSize(12.0f);
			body.TextAlignment = UITextAlignment.Center;
			body.Lines = 0;
			body.Text = @"This is the intro view controller. 
Click the button below to open the menu to switch controllers.

You can also drag the menu open from the right side of the screen";

			var menuButton = new UIButton(UIButtonType.System);
			menuButton.Frame = new RectangleF(310, 250, 220, 30);
			menuButton.SetTitle("Toggle Side Menu", UIControlState.Normal);
			menuButton.TouchUpInside += (sender, e) => {
				SidebarController.ToggleMenu();
			};

			var CodeTextField = new UITextField();
			CodeTextField.Frame = new RectangleF(310, 350, 220, 30);
			CodeTextField.Placeholder = "Add code";

			var runButton = new UIButton(UIButtonType.System);
			runButton.Frame = new RectangleF(310, 120, 220, 30);
			runButton.SetTitle("Run", UIControlState.Normal);
			runButton.TouchUpInside += (sender, e) => {
				string input = CodeTextField.Text;
				new UIAlertView ("Code", input, null, "OK", null).Show ();
				Scanner scanner = new Scanner (input);
				Parser parser = new Parser (scanner);
				parser.Parse ();
				string errorMessage = 
					(string.IsNullOrEmpty (parser.errors.errorMessage)) ? "None" : parser.errors.errorMessage;
				new UIAlertView ("Errors", errorMessage, null, "OK", null).Show ();
			};

			var secondButton = new UIButton(UIButtonType.System);
			secondButton.Frame = new RectangleF(310, 520, 220, 30);
			secondButton.SetTitle("Run", UIControlState.Normal);
			secondButton.TouchUpInside += (sender, e) => {
				//AddBlock();
				new UIAlertView ("secondButton", "second", null, "OK", null).Show ();
			};

			var blocksView = new UIView ();
			blocksView.Frame = new RectangleF (0, 0, 260f, 600);
			blocksView.BackgroundColor = UIColor.Black;

			// Line Layout
			lineLayout = new LineLayout (){
				HeaderReferenceSize = new CGSize (260, 100),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			};

			simpleCollectionViewController = new SimpleCollectionViewController (lineLayout);
			simpleCollectionViewController.SetParentController (this);

			blocksView.Add (simpleCollectionViewController.View);
			scrollView.Add(blocksView);
			scrollView.Add(title);
			scrollView.Add(unified);
			scrollView.Add(secondButton);
			scrollView.Add(menuButton);
			scrollView.Add (CodeTextField);
			scrollView.Add (runButton);

			View.Add (scrollView);

		}

		/// <summary>
		/// Adds a button to the current view.
		/// </summary>
		public void AddBlock(UIView blockView, int typeOfBlock){
			if (insertPosition != -1 || blocksOnView.Sum() == 0) {
				foreach(UIView view in blockView.Subviews){
					if (view.Tag == 1) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							insertPosition = (float) blockView.Frame.Location.Y + (float)blockView.Frame.Size.Height;
						};
					}else if(view.Tag == 2){
						((UIButton)view).TouchUpInside += (sender, e) => {
							blockView.RemoveFromSuperview();
							blocksOnView[typeOfBlock]--;
						};
					}
				}
				blockView.Frame = new CGRect (blockView.Frame.X, insertPosition, blockView.Frame.Width, blockView.Frame.Height);
				Console.WriteLine("adding", "x: " + blockView.Frame.X + ", y: " + blockView.Frame.Y);
				blocksOnView[typeOfBlock]++;
				View.AddSubview(blockView);
			}
			Console.WriteLine (blocksOnView.Sum ());
			insertPosition = -1;
		}
	}
}

