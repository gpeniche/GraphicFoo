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
			deleteBlock.Frame = new CGRect (deletePoint.X, deletePoint.Y, 40, 40);
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

		/// <summary>
		/// Contructs the end block.
		/// </summary>
		/// <returns>The end block.</returns>
		/// <param name="tag">Tag.</param>
		/// <param name="backgroundImage">Background image.</param>
		/// <param name="textOnBlock">Text on block.</param>
		/// <param name="width">Width.</param>
		/// <param name="labelRect">Label rect.</param>
		/// <param name="color">Color.</param>
		public static UIView ContructEndBlock (
			int tag,
			UIImage backgroundImage,
			string textOnBlock,
			float width,
			CGRect labelRect,
			UIColor color)
		{
			UIView blockView = 
				new UIView (new CGRect (0, 200, width, 90));
			blockView.Tag = tag;

			UIImageView backgroundImageView = 
				new UIImageView (new CGRect (-36, -9, width, 132));
			backgroundImageView.Image = backgroundImage;

			UILabel footerText = 
				new UILabel (labelRect);
			footerText.TextAlignment = UITextAlignment.Center;
			footerText.TextColor = color;
			footerText.Text = textOnBlock;
			footerText.Font = UIFont.FromName("Orange Kid", 28f);

			blockView.Add (backgroundImageView);
			blockView.Add (footerText);

			return blockView;
		}
	}
}

