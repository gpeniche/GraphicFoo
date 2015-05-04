using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using ObjCRuntime;
using System.Drawing;

namespace GraphicFoo
{
	public class BlocksCollectionViewController : UICollectionViewController
	{
		static NSString blockCellId = new NSString ("BlockCell");
		static NSString headerId = new NSString ("Header");
		List<IBlock> blocks;
		public IntroController introController;

		public void SetParentController (IntroController intro)
		{
			introController = intro;
		}

		public BlocksCollectionViewController (UICollectionViewLayout layout) : base (layout)
		{
			blocks = new List<IBlock> ();
			blocks.Add (new FunctionHeader ());
			blocks.Add (new EndFunction ());
			blocks.Add (new Return ());
			blocks.Add (new IfHeader ());
			blocks.Add (new EndIf ());
			blocks.Add (new Else ());
			blocks.Add (new Print ());
			blocks.Add (new Declaration ());
			blocks.Add (new Assignment ());
			blocks.Add (new LoopHeader ());
			blocks.Add (new EndLoop ());

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			CollectionView.RegisterClassForCell (typeof(BlockCell), blockCellId);
			CollectionView.Frame = new CGRect (
				0,
				0,
				260,
				introController.View.Frame.Size.Height
			);
			CollectionView.BackgroundColor = UIColor.Black;
			CollectionView.RegisterClassForSupplementaryView (
				typeof(Header),
				UICollectionElementKindSection.Header,
				headerId
			);

			UIMenuItem menuItem = new UIMenuItem ("More info", new Selector ("custom"));

			UIMenuController.SharedMenuController.MenuItems = new [] {
				menuItem
			};
		}

		public override nint NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override nint GetItemsCount (
			UICollectionView collectionView, nint section)
		{
			return blocks.Count;
		}

		public override UICollectionViewCell GetCell (
			UICollectionView collectionView, NSIndexPath indexPath)
		{

			BlockCell blockCell = (BlockCell)collectionView.DequeueReusableCell (
				                      blockCellId,
				                      indexPath
			                      );

			IBlock block = blocks [indexPath.Row];

			blockCell.blockCell = block;

			blockCell.SetTitle ();
			blockCell.SetExample ();
			blockCell.SetImage ();
			blockCell.SetParentController (introController);

			return blockCell;
		}

		public override UICollectionReusableView GetViewForSupplementaryElement (
			UICollectionView collectionView,
			NSString elementKind,
			NSIndexPath indexPath)
		{
			Header headerView = (Header)collectionView.DequeueReusableSupplementaryView (
				                    elementKind,
				                    headerId,
				                    indexPath
			                    );
			UILabel title = new UILabel (new CGRect (0, -20, 260, 100));
			title.Text = "Your blocks";
			title.TextAlignment = UITextAlignment.Center;
			title.TextColor = UIColor.FromRGB (191, 222, 227);
			title.Font = UIFont.FromName ("Orange Kid", 34f);
			title.BackgroundColor = UIColor.Black;

			headerView.BackgroundColor = UIColor.Black;
			headerView.Add (title);
			return headerView;
		}

		public override void ItemHighlighted (
			UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.DarkGray;
		}

		public override void ItemUnhighlighted (
			UICollectionView collectionView,
			NSIndexPath indexPath)
		{
			var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.Black;
		}

		public override bool ShouldHighlightItem (
			UICollectionView collectionView,
			NSIndexPath indexPath)
		{
			return true;
		}

		//      public override bool ShouldSelectItem (UICollectionView collectionView, NSIndexPath indexPath)
		//      {
		//          return false;
		//      }

		// for edit menu
		public override bool ShouldShowMenu (
			UICollectionView collectionView,
			NSIndexPath indexPath)
		{
			return true;
		}

		public override bool CanPerformAction (
			UICollectionView collectionView,
			Selector action,
			NSIndexPath indexPath,
			NSObject sender)
		{
			// Selector should be the same as what's in the custom UIMenuItem
			return action == new Selector ("custom");
		}

		public override void PerformAction (
			UICollectionView collectionView,
			Selector action,
			NSIndexPath indexPath,
			NSObject sender)
		{
			System.Diagnostics.Debug.WriteLine ("code to perform action");
		}

		// CanBecomeFirstResponder and CanPerform are needed for a custom menu item to appear
		public override bool CanBecomeFirstResponder {
			get {
				return true;
			}
		}

		public override bool CanPerform (Selector action, NSObject withSender)
		{
			return action == new Selector ("custom");
		}

		public override void WillRotate (
			UIInterfaceOrientation toInterfaceOrientation,
			double duration)
		{
			/*base.WillRotate (toInterfaceOrientation, duration);

			LineLayout lineLayout = CollectionView.CollectionViewLayout as LineLayout;
			if (lineLayout != null) {
				if ((toInterfaceOrientation == UIInterfaceOrientation.Portrait) ||
				    (toInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown))
					lineLayout.SectionInset = new UIEdgeInsets (400, 0, 400, 0);
				else
					lineLayout.SectionInset = new UIEdgeInsets (220, 0.0f, 200, 0.0f);
			}*/
		}

	}

	public class BlockCell : UICollectionViewCell
	{
		public NSIndexPath IndexPath;
		UIButton blockAction;
		public IntroController introController;
		public IBlock blockCell;
		UILabel exampleLabel;
		UILabel nameLabel;

		[Export ("initWithFrame:")]
		public BlockCell (CGRect frame) : base (frame)
		{
			ContentView.Layer.BorderColor = new CGColor (191, 222, 227);
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.Black;
			ContentView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);

			blockAction = UIButton.FromType (UIButtonType.Custom);
			blockAction.Frame = new CGRect (0, 50, 280, 130);

			exampleLabel = 
				new UILabel (new RectangleF (30, 90, 210, 50));
			exampleLabel.TextAlignment = UITextAlignment.Center;
			exampleLabel.Font = UIFont.FromName ("Orange Kid", 28f);

			nameLabel = new UILabel (new RectangleF (-30, 10, 320, 30));
			nameLabel.TextAlignment = UITextAlignment.Center;
			nameLabel.Font = UIFont.FromName ("Orange Kid", 34f);

			ContentView.Add (blockAction);
			ContentView.Add (exampleLabel);
			ContentView.Add (nameLabel);

			blockAction.TouchUpInside += (sender, e) => {
				if (introController.lastSelected == null || introController.lastSelected.Selected == true) {
					introController.AddTextToCompilingString (blockCell.Syntax);
				}
				introController.AddBlock (blockCell.BlockView, blockCell);
			};
		}

		public void SetTitle ()
		{
			nameLabel.TextColor = blockCell.Color;
			nameLabel.Text = blockCell.Name;
		}

		public void SetExample ()
		{
			exampleLabel.TextColor = blockCell.Color;
			exampleLabel.Text = blockCell.Example;
		}

		public void SetImage ()
		{
			blockAction.SetImage (blockCell.Image, UIControlState.Normal);
		}

		public void SetParentController (IntroController intro)
		{
			introController = intro;
		}

		[Export ("custom")]
		void Custom ()
		{
			// Put all your custom menu behavior code here
			new UIAlertView (
				blockCell.Name,
				blockCell.Explanation,
				null,
				"Got it!",
				null
			).Show ();
		}

		public override bool CanPerform (Selector action, NSObject withSender)
		{
			return action == new Selector ("custom");
		}
	}

	public sealed class Header : UICollectionReusableView
	{
		readonly UILabel label;

		public UILabel Label {
			get {
				return label;
			}
		}

		public string Text {
			get {
				return label.Text;
			}
			set {
				label.Text = value;
				SetNeedsDisplay ();
			}
		}

		[Export ("initWithFrame:")]
		public Header (CGRect frame) : base (frame)
		{
			label = new UILabel {
				Frame = new CGRect (30, 0, 250, 50),
				BackgroundColor = UIColor.FromRGB (55f, 55f, 55f)
			};
			AddSubview (label);
		}
	}

}

