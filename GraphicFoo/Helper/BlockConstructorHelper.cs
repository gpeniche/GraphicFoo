using System;
using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public static class BlockConstructorHelper
	{
		/// <summary>
		/// Constructs the shared elements among blocks.
		/// </summary>
		/// <returns>The shared elements.</returns>
		/// <param name="deletePoint">Delete position.</param>
		/// <param name="inserPoint">Inser position.</param>
		public static UIView ConstructSharedElements (CGPoint deletePoint, CGPoint inserPoint)
		{
			UIView sharedView = new UIView ();
			UIButton deleteBlock = UIButton.FromType (UIButtonType.Custom);
			deleteBlock.Frame = new CGRect (deletePoint.X, deletePoint.Y, 20, 20);
			deleteBlock.SetImage (
				UIImage.FromFile ("Graphics/delete-icon.png"),
				UIControlState.Normal
			);
			deleteBlock.Tag = 2;

			UIButton insertPositionBtn = UIButton.FromType (UIButtonType.Custom);
			insertPositionBtn.Frame = new CGRect (
				inserPoint.X,
				inserPoint.Y,
				50,
				50
			);
			insertPositionBtn.SetImage (
				UIImage.FromBundle ("Graphics/circle-empty.png"),
				UIControlState.Normal
			);
			insertPositionBtn.SetImage (
				UIImage.FromBundle ("Graphics/circle-full.png"),
				UIControlState.Selected
			);
			insertPositionBtn.SetImage (
				UIImage.FromBundle ("Graphics/circle-full.png"),
				UIControlState.Highlighted
			);
			insertPositionBtn.Tag = 1;

			sharedView.Add (deleteBlock);
			sharedView.Add (insertPositionBtn);

			return sharedView;
		}

		public static UIView ContructEndBlock (
			int tag,
			UIImage backgroundImage,
			string textOnBlock,
			float width,
			CGRect labelRect)
		{
			UIView blockView = 
				new UIView (new CGRect (0, 200, width, 100));
			blockView.Tag = tag;

			UIImageView backgroundImageView = 
				new UIImageView (new CGRect (-36, -9, width, 132));
			backgroundImageView.Image = backgroundImage;

			UILabel footerText = 
				new UILabel (labelRect);
			footerText.Font = UIFont.SystemFontOfSize (24.0f);
			footerText.TextAlignment = UITextAlignment.Center;
			footerText.TextColor = UIColor.White;
			footerText.Text = textOnBlock;

			blockView.Add (backgroundImageView);
			blockView.Add (footerText);

			return blockView;
		}
	}
}

