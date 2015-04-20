using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace GraphicFoo
{
	public partial class IntroController : BaseController
	{
		BlocksCollectionViewController blocksCollectionViewController;
		LineLayout lineLayout;
		UIScrollView scrollView;
		float insertPositionY = 50;
		float insertPositionX = 0;
		List<UIView> blocksOnView = new List<UIView> ();
		UIButton lastSelected;

		public IntroController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var uIScrollView = new UIScrollView ();
			uIScrollView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			scrollView = uIScrollView;
			scrollView.Frame = new RectangleF (
				0,
				0,
				(float)View.Frame.Size.Width,
				(float)View.Frame.Size.Height
			);


			View.BackgroundColor = UIColor.White;

			UILabel title = new UILabel (new RectangleF (260, 20, 320, 30));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.Blue;
			title.Text = "Graphic Foo";

			UIButton menuButton = new UIButton (UIButtonType.System);
			menuButton.Frame = new RectangleF (700, 20, 50, 50);
			menuButton.SetImage (
				UIImage.FromBundle ("menu.png"),
				UIControlState.Normal
			);
			menuButton.TouchUpInside += (sender, e) => {
				SidebarController.ToggleMenu ();
			};

			UITextField CodeTextField = new UITextField ();
			CodeTextField.Frame = new RectangleF (310, 350, 220, 30);
			CodeTextField.Placeholder = "Add code";

			UIButton runButton = new UIButton (UIButtonType.System);
			runButton.Frame = new RectangleF (310, 120, 220, 30);
			runButton.SetTitle ("Run", UIControlState.Normal);
			runButton.TouchUpInside += (sender, e) => {
				string input = CodeTextField.Text;
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

			UIView blocksView = new UIView ();
			blocksView.Frame = new RectangleF (0, 0, 260f, 600);

			// Line Layout
			lineLayout = new LineLayout () {
				HeaderReferenceSize = new CGSize (260, 100),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			};

			blocksCollectionViewController = 
				new BlocksCollectionViewController (lineLayout);
			blocksCollectionViewController.SetParentController (this);

			blocksView.Add (blocksCollectionViewController.View);
			scrollView.Add (blocksView);
			scrollView.Add (title);
			scrollView.Add (menuButton);
			scrollView.Add (CodeTextField);
			scrollView.Add (runButton);

			View.Add (scrollView);

		}

		/// <summary>
		/// Adds a button to the current view.
		/// </summary>
		public void AddBlock (UIView blockView, int typeOfBlock)
		{
			if (insertPositionY != 49 || blocksOnView.Count == 0) {
				foreach (UIView view in blockView.Subviews) {
					if (view.Tag == 1) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							insertPositionY = (float)blockView.Frame.Location.Y + (float)blockView.Frame.Size.Height;
							if (blockView.Tag > 0) {
								insertPositionX = (float)blockView.Frame.Location.X - 260f + blockView.Tag;
							} else {
								insertPositionX = (float)blockView.Frame.Location.X - 260f;
							}
							if (lastSelected != null) {
								lastSelected.Selected = false;
							}
							((UIButton)sender).Selected = true;
							lastSelected = ((UIButton)sender);
						};
					} else if (view.Tag == 2) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							ArrangeElementsOnView (blocksOnView.IndexOf (blockView), -100);
							blockView.RemoveFromSuperview ();
							blocksOnView.Remove (blockView);
						};
					}
				}
				if (blockView.Tag < 0) {
					insertPositionX += blockView.Tag;
				}

				if (lastSelected != null) {
					((UIButton)lastSelected).Selected = false;
				}
				blockView.Frame = new CGRect (
					blockView.Frame.X + insertPositionX,
					insertPositionY,
					blockView.Frame.Width,
					blockView.Frame.Height
				);
				if (lastSelected != null && blocksOnView.IndexOf (lastSelected.Superview) + 1 < blocksOnView.Count) {
					ArrangeElementsOnView (blocksOnView.IndexOf (lastSelected.Superview), 100);
					blocksOnView.Insert (blocksOnView.IndexOf (lastSelected.Superview) + 1, blockView);
				} else {
					blocksOnView.Add (blockView);
				}
				View.AddSubview (blockView);
				if (blockView.Tag > 0) {
					insertPositionX += blockView.Tag;
				}
			} else if (blocksOnView.Count == 0) {
				insertPositionX = 0;
			}
			insertPositionY = 49;
		}

		/// <summary>
		/// Arranges the elements on view after adding or deleting an element.
		/// </summary>
		/// <param name="elementToUpdate">Element to update.</param>
		/// <param name="offset">Offset.</param>
		private void ArrangeElementsOnView (int elementToUpdate, int offset)
		{
			for (int index = elementToUpdate + 1; index < blocksOnView.Count; index++) {
				Console.WriteLine ("index: " + index);
				blocksOnView [index].Frame = new CGRect (
					blocksOnView [index].Frame.X,
					blocksOnView [index].Frame.Y + offset,
					blocksOnView [index].Frame.Width,
					blocksOnView [index].Frame.Height
				); 
				View.AddSubview (blocksOnView [index]);
			}
		}
	}
}

