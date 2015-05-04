using System;
using System.Drawing;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;

namespace GraphicFoo
{
	public class IntroController : BaseController
	{
		const float ORIGINX = 50;
		const float ORIGINY = 70;

		public UIButton lastSelected;

		BlocksCollectionViewController blocksCollectionViewController;
		LineLayout lineLayout;
		UIScrollView scrollView;
		UIView consoleView;
		UITextView textOnConsole;
		float insertPositionY = ORIGINY;
		float insertPositionX = ORIGINX;
		List<UIView> blocksOnView = new List<UIView> ();
		string stringToCompile = "%-1% %0% \n";

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

			scrollView = new UIScrollView ();
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			scrollView.Frame = new CGRect (
				260f,
				0f,
				(float)View.Frame.Size.Width - 260f,
				(float)View.Frame.Size.Height - 200f
			);
			scrollView.ContentSize = new CGSize (
				(float)View.Frame.Size.Width - 260f,
				(float)View.Frame.Size.Height - 200f
			);


			View.BackgroundColor = UIColor.Black;

			UILabel title = new UILabel (new CGRect (300, 20, 320, 30));
			title.Font = UIFont.SystemFontOfSize (24.0f);
			title.TextAlignment = UITextAlignment.Center;
			title.Font = UIFont.FromName ("Orange Kid", 44f);
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Text = "Graphic Foo";
				
			UIButton runButton = new UIButton (UIButtonType.Custom);
			runButton.Frame = new CGRect (550, 20, 60, 45);
			runButton.SetTitle ("Run", UIControlState.Normal);
			runButton.SetImage (
				UIImage.FromBundle ("Graphics/play-button.png"),
				UIControlState.Normal
			);
			runButton.TouchUpInside += (sender, e) => {
				SendToCompile ();
			};

			UIButton menuButton = ViewConstructorHelper.LoadMenuButton ();
			menuButton.TouchUpInside += (sender, e) => {
				SidebarController.ToggleMenu ();
			};

			UIView blocksView = new UIView ();
			blocksView.Frame = new RectangleF (
				0, 
				0, 
				260f, 
				(float)View.Frame.Size.Height
			);

			// Line Layout
			lineLayout = new LineLayout {
				HeaderReferenceSize = new CGSize (260, 100),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			};

			consoleView = new UIView ();
			consoleView.Frame = new CGRect (
				260f,
				(float)View.Frame.Size.Height - 200f,
				(float)View.Frame.Size.Width - 260f,
				200f
			);
			consoleView.Layer.BorderWidth = 2.0f;
			consoleView.Layer.BorderColor = new CGColor (191, 222, 227);
			consoleView.BackgroundColor = UIColor.Black;

			textOnConsole = new UITextView ();
			textOnConsole.Frame = new CGRect (
				0,
				20,
				consoleView.Frame.Size.Width,
				consoleView.Frame.Size.Height - 10f
			);
			textOnConsole.TextAlignment = UITextAlignment.Left;
			textOnConsole.TextColor = UIColor.FromRGB (191, 222, 227);
			textOnConsole.BackgroundColor = UIColor.Black;
			textOnConsole.Editable = false;
			textOnConsole.Font = UIFont.FromName ("Orange Kid", 20f);

			UILabel consoleTitle = new UILabel ();
			consoleTitle.Frame = new CGRect (
				4,
				2,
				200f,
				20f
			);
			consoleTitle.TextAlignment = UITextAlignment.Left;
			consoleTitle.TextColor = UIColor.FromRGB (191, 222, 227);
			consoleTitle.Text = "FooConsole: ";
			consoleTitle.Font = UIFont.FromName ("Orange Kid", 24f);

			blocksCollectionViewController = 
				new BlocksCollectionViewController (lineLayout);
			blocksCollectionViewController.SetParentController (this);

			blocksView.Add (blocksCollectionViewController.View);

			consoleView.Add (textOnConsole);
			consoleView.Add (consoleTitle);

			foreach (string family in UIFont.FamilyNames) {
				Console.Write ("\n - Family + : ");
				foreach (string name in UIFont.FontNamesForFamilyName(family)) {
					Console.Write (name + ", ");
				}
			}
			View.BackgroundColor = UIColor.Black;
			View.Add (blocksView);
			View.Add (scrollView);
			View.Add (consoleView);
			View.Add (title);
			View.Add (menuButton);
			View.Add (runButton);

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
				bottom = ((float)activeview.Frame.Y +
				(float)activeview.Frame.Height + offsetKeyboard);

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

		/// <summary>
		/// Prepares the string to compile and sends it to the scanner.
		/// </summary>
		public void SendToCompile ()
		{
			textOnConsole.Text = "";
			string errorMessage = CompilingHelper.SendToCompile (stringToCompile, blocksOnView);
			textOnConsole.Text = errorMessage;
		}

		/// <summary>
		/// Sets the position add new element.
		/// </summary>
		/// <param name="blockView">Block view from where we get the position 
		/// to insert a new element.</param>
		/// <param name="sender">Sender, Button that activates this method.</param>
		private void SetPositionAddNewElement (UIView blockView, UIView sender)
		{
			insertPositionY = (float)blockView.Frame.Location.Y +
			(float)blockView.Frame.Size.Height;
			if (blockView.AccessibilityHint != "else") {
				if (blockView.Tag > 0) {
					insertPositionX = (float)blockView.Frame.Location.X + blockView.Tag;
				} else {
					insertPositionX = (float)blockView.Frame.Location.X;
				}
			} else {
				insertPositionX = (float)blockView.Frame.Location.X - blockView.Tag;
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
		public void AddBlock (UIView blockView, IBlock blockcell)
		{
			if (insertPositionY != 49 || blocksOnView.Count == 0) {
				((UIButton)blockView.Subviews.FirstOrDefault (b => b.Tag == 1)).TouchUpInside += (sender, e) => 
					SetPositionAddNewElement (blockView, sender as UIView);
				((UIButton)blockView.Subviews.FirstOrDefault (b => b.Tag == 2)).TouchUpInside += (sender, e) => {
					ArrangeElementsOnView (
						blocksOnView.IndexOf (blockView),
						(int)blockView.Frame.Height * -1
					);
					RemoveTextFromCompilingString (blocksOnView.IndexOf (blockView));
					blockView.RemoveFromSuperview ();
					ArrangeSizeOfScrollview (blockView, true);
					blocksOnView.Remove (blockView);
					if (blocksOnView.Count == 0) {
						insertPositionX = ORIGINX;
						insertPositionY = ORIGINY;
						stringToCompile = "%-1% %0% \n";
						lastSelected = null;
					}
				};
				if (blockcell.Name == "Declaration" || blockcell.Name == "Function Header") {
					((UIButton)blockView.Subviews.FirstOrDefault (b => b.Tag == 3)).TouchUpInside += (sender, e) => {
						SelectVarType ((UIButton)sender, (int)(blockView.Tag * 1.5));
					};
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
						(int)blockView.Frame.Height
					);
					blocksOnView.Insert (
						blocksOnView.IndexOf (lastSelected.Superview) + 1,
						blockView
					);
				} else {
					blocksOnView.Add (blockView);
				}
				ArrangeSizeOfScrollview (blockView, false);
				scrollView.AddSubview (blockView);
			} else if (blocksOnView.Count == 0) {
				insertPositionX = 0;
			}
			insertPositionY = 49;
		}

		/// <summary>
		/// Arranges the size of scrollview.
		/// </summary>
		/// <param name="blockView">Block view to add.</param>
		/// <param name="removing">If set to <c>true</c> removing element ont view.</param>
		private void ArrangeSizeOfScrollview (UIView blockView, bool removing)
		{
			if (removing) {
				nfloat maxLeft = blocksOnView.Max (bv => bv.Frame.Left);
				nfloat maxTop = blocksOnView.Max (bv => bv.Frame.Top);
				if (blocksOnView.Count > 1 && maxLeft == blockView.Frame.Left && (maxLeft + blockView.Frame.Width) > 508) {
					UIView secondMax = blocksOnView.OrderByDescending (r => r.Frame.Left).Skip (1).FirstOrDefault ();
					scrollView.ContentSize = new CGSize (
						(float)secondMax.Frame.Left + secondMax.Frame.Width - 50f,
						scrollView.ContentSize.Height
					);
				}
				if (maxTop == blockView.Frame.Top && (maxLeft + blockView.Frame.Height) > 824) {
					scrollView.ContentSize = new CGSize (
						scrollView.ContentSize.Width,
						(float)scrollView.ContentSize.Height - blockView.Frame.Size.Height
					);
				}

				nfloat minLeft = blocksOnView.Min (bv => bv.Frame.Left);
				if (blocksOnView.Count > 1 && minLeft == blockView.Frame.Left && minLeft < 0) {
					UIView secondMin = blocksOnView.OrderBy (r => r.Frame.Left).Skip (1).FirstOrDefault ();
					scrollView.ContentInset = new UIEdgeInsets (0, (secondMin.Frame.Left - ORIGINX) * -1, 0, 0);
				}

			} else {
				if (blocksOnView.Count > 7) {
					if (blockView.Frame.Top > scrollView.ContentSize.Height - blockView.Frame.Size.Height) {
						scrollView.ContentOffset = new CGPoint (
							scrollView.ContentOffset.X,
							blockView.Frame.Top
						);
					}
					scrollView.ContentSize = new CGSize (
						scrollView.ContentSize.Width,
						(float)scrollView.ContentSize.Height + blockView.Frame.Size.Height
					);
				}
				if (blockView.Frame.Left < 0) {
					scrollView.ContentOffset = new CGPoint (
						blockView.Frame.Left - ORIGINX,
						scrollView.ContentOffset.Y
					);
					if (blockView.Frame.Left + blockView.Frame.Width >
					    scrollView.ContentSize.Width) {
						scrollView.ContentOffset = new CGPoint (
							blockView.Frame.Left,
							scrollView.ContentOffset.Y + blockView.Frame.Height
						);
					}
					if (((blockView.Frame.Left - ORIGINX) * -1) > scrollView.ContentInset.Left) {
						scrollView.ContentInset = new UIEdgeInsets (
							0,
							(blockView.Frame.Left - ORIGINX) * -1,
							0,
							0
						);
					}
				} else {
					if (blockView.Frame.Left + blockView.Frame.Width >
					    scrollView.ContentSize.Width) {
						scrollView.ContentSize = new CGSize (
							(float)blockView.Frame.Left + blockView.Frame.Width,
							scrollView.ContentSize.Height
						);
						scrollView.ContentOffset = new CGPoint (
							blockView.Frame.Left - 150,
							scrollView.ContentOffset.Y
						);
					}
				}
			}
		}

		/// <summary>
		/// Selects the type of the variable.
		/// </summary>
		/// <param name="sender">Sender, Button that activates the method.</param>
		/// <param name="offset">Offset.</param>
		public void SelectVarType (UIButton sender, int offset)
		{
			// Create a new Alert Controller
			UIAlertController actionSheetAlert = UIAlertController.Create (
				                                     "Types of variables",
				                                     "Select a type from below",
				                                     UIAlertControllerStyle.ActionSheet
			                                     );

			// Add Actions
			actionSheetAlert.AddAction (UIAlertAction.Create (
				"number",
				UIAlertActionStyle.Default,
				(action) => {
					sender.SetImage (
						UIImage.FromFile ("Graphics/number-icon.png"),
						UIControlState.Normal
					);
					sender.AccessibilityHint = "number";
				}
			));

			actionSheetAlert.AddAction (UIAlertAction.Create (
				"void",
				UIAlertActionStyle.Default,
				(action) => {
					sender.SetImage (
						UIImage.FromFile ("Graphics/void-icon.png"),
						UIControlState.Normal
					);
					sender.AccessibilityHint = "void";
				}
			));

			actionSheetAlert.AddAction (UIAlertAction.Create (
				"boolean",
				UIAlertActionStyle.Default,
				(action) => {
					sender.SetImage (
						UIImage.FromFile ("Graphics/boolean-icon.png"),
						UIControlState.Normal
					);
					sender.AccessibilityHint = "boolean";
				}
			));

			actionSheetAlert.AddAction (UIAlertAction.Create (
				"string", 
				UIAlertActionStyle.Default,
				(action) => {
					sender.SetImage (
						UIImage.FromFile ("Graphics/string-icon.png"),
						UIControlState.Normal
					);
					sender.AccessibilityHint = "string";
				}
			));

			actionSheetAlert.AddAction (UIAlertAction.Create (
				"Cancel",
				UIAlertActionStyle.Cancel,
				((action) => sender.SetImage (
					UIImage.FromFile ("Graphics/delete-icon.png"),
					UIControlState.Normal
				)
				)));

			// Required for iPad - You must specify a source for the Action Sheet since it is
			// displayed as a popover
			UIPopoverPresentationController presentationPopover = 
				actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null) {
				presentationPopover.SourceView = sender.Superview;
				presentationPopover.SourceRect = new CGRect (60 + offset, 100, 0, 0);
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}
			// Display the alert
			this.PresentViewController (actionSheetAlert, true, null);
		}

		/// <summary>
		/// Adds the text to compiling string.
		/// </summary>
		/// <param name="textToAdd">Text to add.</param>
		public void AddTextToCompilingString (string textToAdd)
		{
			stringToCompile = CompilingHelper.AddTextToCompilingString (
				stringToCompile,
				textToAdd,
				blocksOnView,
				lastSelected
			);
		}

		/// <summary>
		/// Removes the text from compiling string.
		/// </summary>
		/// <param name="index">Index from where we're going to remove the text.</param>
		public void RemoveTextFromCompilingString (int index)
		{
			stringToCompile = CompilingHelper.RemoveTextFromCompilingString (
				stringToCompile,
				blocksOnView,
				index
			);
		}

		/// <summary>
		/// Arranges the elements on view after adding or deleting an element.
		/// </summary>
		/// <param name="elementToUpdate">Element to update.</param>
		/// <param name="offset">Offset.</param>
		private void ArrangeElementsOnView (int elementToUpdate, int offset)
		{
			UIView.BeginAnimations (string.Empty, IntPtr.Zero);
			UIView.SetAnimationDuration (0.3);
			for (int index = elementToUpdate + 1; index < blocksOnView.Count; index++) {
				blocksOnView [index].Frame = new CGRect (
					blocksOnView [index].Frame.X,
					blocksOnView [index].Frame.Y + offset,
					blocksOnView [index].Frame.Width,
					blocksOnView [index].Frame.Height
				); 
				scrollView.AddSubview (blocksOnView [index]);
			}
			UIView.CommitAnimations ();
		}
	}
}
