using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace GraphicFoo
{
	public class IntroController : BaseController
	{
		BlocksCollectionViewController blocksCollectionViewController;
		LineLayout lineLayout;
		UIScrollView scrollView;
		float insertPositionY = 50;
		float insertPositionX = 0;
		List<UIView> blocksOnView = new List<UIView> ();
		UIButton lastSelected;
		string stringToCompile = "function main() : void %-1% " +
		                         "%0%" +
		                         " return; endFunc";

		private UIView activeview;
		// Controller that activated the keyboard
		private float scrollamount = 0.0f;
		// amount to scroll
		private float bottom = 0.0f;
		// bottom point
		private const float offsetKeyboard = 10.0f;
		// extra offset
		private bool moveViewUp = false;
		// which direction are we moving

		public IntroController () : base (null, null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Keyboard popup
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification, KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification, KeyBoardDownNotification);

			UIScrollView uIScrollView = new UIScrollView ();
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
				
			UIButton runButton = new UIButton (UIButtonType.Custom);
			runButton.Frame = new RectangleF (600, 20, 60, 45);
			runButton.SetTitle ("Run", UIControlState.Normal);
			runButton.SetImage (
				UIImage.FromBundle ("play-button.png"),
				UIControlState.Normal
			);
			runButton.TouchUpInside += (sender, e) => {
				SendToCompile ();
			};

			UIView blocksView = new UIView ();
			blocksView.Frame = new RectangleF (0, 0, 260f, 600);

			// Line Layout
			lineLayout = new LineLayout {
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
			scrollView.Add (runButton);

			View.Add (scrollView);

		}

		private void KeyBoardUpNotification (NSNotification notification)
		{
			// get the keyboard size
			/*NSValue val = new NSValue(notification.UserInfo.ValueForKey(UIKeyboard.FrameBeginUserInfoKey).Handle);
			RectangleF r = val.RectangleFValue;*/
			CGRect r = UIKeyboard.BoundsFromNotification (notification);

			// Find what opened the keyboard
			foreach (UIView view in blocksOnView) {
				foreach (UIView inView in view.Subviews) {
					if (inView.IsFirstResponder)
						activeview = view;
				}
			}
			if (activeview != null) {
				// Bottom of the controller = initial position + height + offset      
				bottom = ((float)activeview.Frame.Y + (float)activeview.Frame.Height + offsetKeyboard);

				// Calculate how far we need to scroll
				scrollamount = ((float)r.Height - (float)(View.Frame.Size.Height - bottom));

				// Perform the scrolling
				if (scrollamount > 0) {
					moveViewUp = true;
					ScrollTheView (moveViewUp);
				} else {
					moveViewUp = false;
				}
			}
		}

		private void KeyBoardDownNotification (NSNotification notification)
		{
			if (moveViewUp) {
				ScrollTheView (false);
			}
		}

		/// <summary>
		/// Scrolls the view.
		/// </summary>
		/// <param name="move">If set to <c>true</c> move up.</param>
		private void ScrollTheView (bool move)
		{
			// scroll the view up or down
			UIView.BeginAnimations (string.Empty, IntPtr.Zero);
			UIView.SetAnimationDuration (0.3);

			CGRect frame = View.Frame;

			if (move) {
				frame.Y -= scrollamount;
			} else {
				frame.Y += scrollamount;
				scrollamount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations ();
		}

		private void SendToCompile ()
		{
			string stringForScanner = stringToCompile;
			foreach (UIView activeview in blocksOnView) {
				string subStr = string.Empty, originalsubStr;
				foreach (UIView view in activeview.Subviews) {
					if (view.Class.Name == "UITextField") {
						//Updates string to send to parser and scanner

						if (string.IsNullOrWhiteSpace (subStr)) {
							int fIndex = stringToCompile.IndexOf (
								             "%" + blocksOnView.IndexOf (activeview) + "%"
							             );
							int sIndex = stringToCompile.IndexOf (
								             "%" + (blocksOnView.IndexOf (activeview) - 1) + "%"
							             );
							originalsubStr = subStr = stringToCompile.Substring (
								sIndex,
								fIndex - sIndex
							);
						} else {
							originalsubStr = subStr;
						}
						subStr = subStr.Replace (
							"%" + view.AccessibilityLabel + "%",
							((UITextField)view).Text
						);
						stringForScanner = stringForScanner.Replace (
							originalsubStr,
							subStr
						);
					}
				}
			}
			for (int e = -1; e <= blocksOnView.Count; e++) {
				stringForScanner = stringForScanner.Replace ("%" + e + "%", "");
			}
			Console.WriteLine ("stringForScanner: " + stringForScanner);
			Console.WriteLine ("stringToCompile: " + stringToCompile);
			Scanner scanner = new Scanner (stringForScanner);
			Parser parser = new Parser (scanner);
			parser.Parse ();
			string errorMessage = 
				(!string.IsNullOrEmpty (parser.errors.errorMessage)) ? 
				parser.errors.errorMessage : 
				"None";
			new UIAlertView ("Errors", errorMessage, null, "OK", null).Show ();
		}

		private void SetPositionAddNewElement (UIView blockView, UIView sender)
		{
			insertPositionY = (float)blockView.Frame.Location.Y +
			(float)blockView.Frame.Size.Height;
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
		}

		/// <summary>
		/// Adds a button to the current view.
		/// </summary>
		public void AddBlock (UIView blockView)
		{
			if (insertPositionY != 49 || blocksOnView.Count == 0) {
				foreach (UIView view in blockView.Subviews) {
					if (view.Tag == 1) {
						((UIButton)view).TouchUpInside += (sender, e) => 
							SetPositionAddNewElement (blockView, sender as UIView);
					} else if (view.Tag == 2) {
						((UIButton)view).TouchUpInside += (sender, e) => {
							ArrangeElementsOnView (blocksOnView.IndexOf (blockView), -100);
							RemoveTextToCompilingString (blocksOnView.IndexOf (blockView));
							blockView.RemoveFromSuperview ();
							blocksOnView.Remove (blockView);
						};
					}
				}
				if (blockView.Tag < 0) {
					insertPositionX += blockView.Tag;
				}

				if (lastSelected != null) {
					lastSelected.Selected = false;
				}
				blockView.Frame = new CGRect (
					blockView.Frame.X + insertPositionX,
					insertPositionY,
					blockView.Frame.Width,
					blockView.Frame.Height
				);
				if (lastSelected != null &&
				    blocksOnView.IndexOf (lastSelected.Superview) + 1 < blocksOnView.Count) {
					ArrangeElementsOnView (
						blocksOnView.IndexOf (lastSelected.Superview),
						100
					);
					blocksOnView.Insert (
						blocksOnView.IndexOf (lastSelected.Superview) + 1,
						blockView
					);
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
		/// Adds the text to compiling string.
		/// </summary>
		/// <param name="textToAdd">Text to add.</param>
		public void AddTextToCompilingString (string textToAdd)
		{
			if (lastSelected == null) {
				stringToCompile = stringToCompile.Replace ("%0%", textToAdd + "%0%");
			} else {
				int index = blocksOnView.IndexOf (lastSelected.Superview);
				if (stringToCompile.Contains ("%" + (index + 1) + "%")) {
					ArrangeIndexes (index, true);
				}
				stringToCompile = stringToCompile.Replace (
					"%" + index + "%",
					"%" + index + "%" + textToAdd + "%" + (index + 1) + "%"
				);
			}
		}

		public void RemoveTextToCompilingString (int index)
		{
			int fIndex = stringToCompile.IndexOf ("%" + index + "%");
			int sIndex = stringToCompile.IndexOf ("%" + (index - 1) + "%");
			string strToDelete = stringToCompile.Substring (
				                     sIndex,
				                     fIndex - sIndex
			                     );
			ArrangeIndexes (index, false);
			stringToCompile = stringToCompile.Replace (
				strToDelete,
				""
			);
		}

		/// <summary>
		/// Arranges the indexes.
		/// </summary>
		/// <param name="fromIndex">From index to start the arrange.</param>
		/// <param name="goingUp">If set to <c>true</c> going up.</param>
		public void ArrangeIndexes (int fromIndex, bool goingUp)
		{
			if (goingUp) {
				for (int i = blocksOnView.Count - 1; i > fromIndex; i--) {
					stringToCompile = stringToCompile.Replace (
						"%" + i + "%",
						"%" + (i + 1) + "%"
					);
				}
			} else {
				for (int i = fromIndex; i <= blocksOnView.Count; i++) {
					stringToCompile = stringToCompile.Replace (
						"%" + i + "%",
						"%" + (i - 1) + "%"
					);
				}
			}

		}

		/// <summary>
		/// Arranges the elements on view after adding or deleting an element.
		/// </summary>
		/// <param name="elementToUpdate">Element to update.</param>
		/// <param name="offset">Offset.</param>
		private void ArrangeElementsOnView (int elementToUpdate, int offset)
		{
			for (int index = elementToUpdate + 1; index < blocksOnView.Count; index++) {
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

