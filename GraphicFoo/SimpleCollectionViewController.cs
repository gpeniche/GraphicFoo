using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using CoreGraphics;
using ObjCRuntime;
using System.Drawing;

namespace GraphicFoo
{
    public class SimpleCollectionViewController : UICollectionViewController
    {
		static NSString blockCellId = new NSString ("BlockCell");
        static NSString headerId = new NSString ("Header");
        List<IBlock> blocks;
		public IntroController introController;

		public void SetParentController(IntroController intro){
			this.introController = intro;
		}

        public SimpleCollectionViewController (UICollectionViewLayout layout) : base (layout)
        {
            blocks = new List<IBlock> ();
            for (int i = 0; i < 15; i++) {
                blocks.Add (new Declaration ());
            }
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
//			CollectionView.DataSource = SimpleCollectionViewController;
			CollectionView.RegisterClassForCell (typeof(BlockCell), blockCellId);
			CollectionView.Frame = new CGRect (0, 0, 260, introController.View.Frame.Size.Height);
			CollectionView.BackgroundColor = UIColor.Blue;
            CollectionView.RegisterClassForSupplementaryView (typeof(Header), UICollectionElementKindSection.Header, headerId);

			/*UIMenuController.SharedMenuController.MenuItems = new UIMenuItem[] {
				new UIMenuItem ("Custom", new Selector ("custom"))
			};*/
		}

        public override nint NumberOfSections (UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount (UICollectionView collectionView, nint section)
        {
            return blocks.Count;
        }

        public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
        {

			var blockCell = (BlockCell)collectionView.DequeueReusableCell (blockCellId, indexPath);

            var block = blocks [indexPath.Row];

			Console.WriteLine(indexPath.Row);

			switch (indexPath.Row) {
			case 1: // Declaration
				blockCell.title = "Declaration";
				Console.WriteLine("Case 1");
				break;
			case 2: //Whileheader
				blockCell.title = "Whileheader";
				Console.WriteLine("Case 2");
				break;
			default:
				blockCell.title = "Whileheader";
				Console.WriteLine("Default case");
				break;
			}

			blockCell.SetImage();

			blockCell.SetParentController (this.introController);

			return blockCell;
        }

        public override UICollectionReusableView GetViewForSupplementaryElement (UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            var headerView = (Header)collectionView.DequeueReusableSupplementaryView (elementKind, headerId, indexPath);
            headerView.Text = "Your blocks to choose";
            return headerView;
        }

        public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem (indexPath);
			cell.ContentView.BackgroundColor = UIColor.Green;
        }

        public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.CellForItem (indexPath);
            cell.ContentView.BackgroundColor = UIColor.White;
        }

        public override bool ShouldHighlightItem (UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

//      public override bool ShouldSelectItem (UICollectionView collectionView, NSIndexPath indexPath)
//      {
//          return false;
//      }

        // for edit menu
        public override bool ShouldShowMenu (UICollectionView collectionView, NSIndexPath indexPath)
        {
            return true;
        }

        public override bool CanPerformAction (UICollectionView collectionView, Selector action, NSIndexPath indexPath, NSObject sender)
		{
			// Selector should be the same as what's in the custom UIMenuItem
			if (action == new Selector ("custom")) {
				return true;
			}
			else
				return false;
        }

        public override void PerformAction (UICollectionView collectionView, Selector action, NSIndexPath indexPath, NSObject sender)
        {
			System.Diagnostics.Debug.WriteLine ("code to perform action");
        }

		// CanBecomeFirstResponder and CanPerform are needed for a custom menu item to appear
		public override bool CanBecomeFirstResponder {
			get {
				return true;
			}
		}

		/*public override bool CanPerform (Selector action, NSObject withSender)
		{
			if (action == new Selector ("custom"))
				return true;
			else
				return false;
		}*/

        public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate (toInterfaceOrientation, duration);

            var lineLayout = CollectionView.CollectionViewLayout as LineLayout;
            if (lineLayout != null)
            {
                if((toInterfaceOrientation == UIInterfaceOrientation.Portrait) || (toInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown))
                    lineLayout.SectionInset = new UIEdgeInsets (400,0,400,0);
                else
                    lineLayout.SectionInset  = new UIEdgeInsets (220, 0.0f, 200, 0.0f);
            }
        }

    }

    public class BlockCell : UICollectionViewCell
    {
        public UIImage imageView;
		public NSIndexPath IndexPath;
		public string title;
		UIButton blockAction;
		public IntroController introController;

        [Export ("initWithFrame:")]
		public BlockCell (CGRect frame) : base (frame)
        {
            BackgroundView = new UIView{BackgroundColor = UIColor.Orange};

            SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};

            ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
            ContentView.Layer.BorderWidth = 2.0f;
            ContentView.BackgroundColor = UIColor.White;
//			ContentView.Frame = new CGRect (0, 0, 260, 200);
            ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);

			var titleLabel = new UILabel(new RectangleF(0, 50, 320, 30));
			titleLabel.Font = UIFont.SystemFontOfSize(24.0f);
			titleLabel.TextAlignment = UITextAlignment.Center;
			titleLabel.TextColor = UIColor.Blue;
			titleLabel.Text = "Block";

            /*imageView = new UIImageView (UIImage.FromBundle ("placeholder.png"));
            imageView.Center = ContentView.Center;
            imageView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);*/

			// add ok button
			blockAction = UIButton.FromType(UIButtonType.RoundedRect);
			blockAction.Frame = new System.Drawing.RectangleF(50, 100, 100, 100);

			ContentView.Add(blockAction);
			ContentView.Add(titleLabel);

			var titleTest = new UILabel(new CGRect(260, 500, 320, 30));
			titleTest.Font = UIFont.SystemFontOfSize(24.0f);
			titleTest.TextAlignment = UITextAlignment.Center;
			titleTest.TextColor = UIColor.Blue;
			titleTest.Text = "Sidebar Navigation";


			blockAction.TouchUpInside += (sender, e) => {
				introController.AddBlock();
				//new UIAlertView ("Title", title, null, "OK", null).Show ();
			};
        } 

		public void SetImage(){
			blockAction.SetImage (UIImage.FromBundle (title + ".png"), UIControlState.Normal);
		}

		public void SetParentController(IntroController intro){
			this.introController = intro;
		}

		[Export("custom")]
		void Custom()
		{
			// Put all your custom menu behavior code here
			Console.WriteLine ("custom in the cell");
		}

		public override bool CanPerform (Selector action, NSObject withSender)
		{
			if (action == new Selector ("custom"))
				return true;
			else
				return false;
		}
    }

    public class Header : UICollectionReusableView
    {
        UILabel label;

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
            label = new UILabel (){Frame = new CGRect (30,0,250,50), BackgroundColor = UIColor.Yellow};
            AddSubview (label);
        }
    }

}

