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
		SimpleCollectionViewController blocksCollectionViewController;
		LineLayout lineLayout;
		UIScrollView scrollView;
		float insertPositionY = 50;
		float insertPositionX = 0;
		int[] blocksOnView = new int[2];
		UIButton lastSelected;

		public IntroController() : base(null, null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			scrollView = new UIScrollView ();
			scrollView.Frame = new RectangleF(0, 0, (float)View.Frame.Size.Width, (float)View.Frame.Size.Height);


			View.BackgroundColor = UIColor.White;

			var title = new UILabel(new RectangleF(260, 20, 320, 30));
			title.Font = UIFont.SystemFontOfSize(24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Graphic Foo";

			var menuButton = new UIButton(UIButtonType.System);
			menuButton.Frame = new RectangleF(700, 20, 50, 50);
			menuButton.SetImage(UIImage.FromBundle ("menu.png"), UIControlState.Normal);
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

			var blocksView = new UIView ();
			blocksView.Frame = new RectangleF (0, 0, 260f, 600);

			// Line Layout
			lineLayout = new LineLayout (){
				HeaderReferenceSize = new CGSize (260, 100),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			};

			blocksCollectionViewController = new SimpleCollectionViewController (lineLayout);
			blocksCollectionViewController.SetParentController (this);

			blocksView.Add (blocksCollectionViewController.View);
			scrollView.Add(blocksView);
			scrollView.Add(title);
			scrollView.Add(menuButton);
			scrollView.Add (CodeTextField);
			scrollView.Add (runButton);

			View.Add (scrollView);

		}

		/// <summary>
		/// Adds a button to the current view.
		/// </summary>
		public void AddBlock(UIView blockView, int typeOfBlock){
			if (insertPositionY != 49 || blocksOnView.Sum () == 0) {
				foreach (UIView view in blockView.Subviews) {
					if (view.Tag == 1) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							insertPositionY = (float)blockView.Frame.Location.Y + (float)blockView.Frame.Size.Height;
							if(blockView.Tag > 0){
								insertPositionX = (float)blockView.Frame.Location.X - 260f + blockView.Tag;
							}else{
								insertPositionX = (float)blockView.Frame.Location.X - 260f;
							}

							((UIButton)sender).Selected = true;
							lastSelected = ((UIButton)sender);
						};
					} else if (view.Tag == 2) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							blockView.RemoveFromSuperview ();
							blocksOnView [typeOfBlock]--;
						};
					}
				}
				if (blockView.Tag < 0) {
					insertPositionX += blockView.Tag;
				}
				blockView.Frame = new CGRect (blockView.Frame.X + insertPositionX, insertPositionY, blockView.Frame.Width, blockView.Frame.Height);
				blocksOnView [typeOfBlock]++;
				if (lastSelected != null) {
					((UIButton)lastSelected).Selected = false;
				}
				View.AddSubview (blockView);
				if (blockView.Tag > 0) {
					insertPositionX += blockView.Tag;
				}
			} else if(blocksOnView.Sum () == 0){
				insertPositionX = 0;
			}
			insertPositionY = 49;
		}
	}
}

