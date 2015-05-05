using System;
using UIKit;
using CoreGraphics;
using Foundation;

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
			footerText.Font = UIFont.FromName ("Orange Kid", 28f);

			blockView.Add (backgroundImageView);
			blockView.Add (footerText);

			return blockView;
		}

		/// <summary>
		/// Creates a text field for a block.
		/// </summary>
		/// <returns>The text field.</returns>
		/// <param name="frame">Frame for the text field.</param>
		/// <param name="placeholder">Placeholder.</param>
		/// <param name="accessibilityLabel">Accessibility label.</param>
		/// <param name="color">Color.</param>
		/// <param name="fontSize">Font size.</param>
		public static UITextField CreateTextField (
			CGRect frame,
			string placeholder,
			string accessibilityLabel,
			UIColor color,
			float fontSize)
		{
			UITextField inputOnBlock = 
				new UITextField (frame);
			inputOnBlock.AttributedPlaceholder = new NSAttributedString (
				placeholder,
				font: UIFont.FromName ("Orange Kid", 16.0f),
				foregroundColor: color,
				strokeWidth: 4 
			);
			inputOnBlock.KeyboardAppearance = UIKeyboardAppearance.Dark;
			inputOnBlock.ShouldReturn += textField => { 
				inputOnBlock.ResignFirstResponder ();
				return true; 
			};
			inputOnBlock.AccessibilityLabel = accessibilityLabel;
			inputOnBlock.TextColor = color;
			inputOnBlock.Font = UIFont.FromName ("Orange Kid", fontSize);
			inputOnBlock.TintColor = color;
			return inputOnBlock;
		}
	}
}

