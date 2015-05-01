using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class EndLoop : IBlock
	{
		
		public string Name {
			get {
				return "EndWhile";
			}
		}

		public string Syntax {
			get {
				return " endWhile \n";
			}
		}

		public string Example {
			get {
				return "endWhile";
			}
		}

		public string Explanation {
			get {
				return "endWhile";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/endloop.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (0, 200, 400, 100));
				blockView.Tag = -88;

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel whileFooterText = 
					new UILabel (new RectangleF (-10, 10, 300, 100));
				whileFooterText.Font = UIFont.SystemFontOfSize (24.0f);
				whileFooterText.TextAlignment = UITextAlignment.Center;
				whileFooterText.TextColor = UIColor.White;
				whileFooterText.Text = "endWhile";

				UIButton deleteBlock = UIButton.FromType (UIButtonType.Custom);
				deleteBlock.Frame = new RectangleF (290, 8, 20, 20);
				deleteBlock.SetImage (
					UIImage.FromFile ("Graphics/delete-icon.png"), 
					UIControlState.Normal
				);
				deleteBlock.Tag = 2;

				UIButton insertPositionBtn = 
					UIButton.FromType (UIButtonType.Custom);
				insertPositionBtn.Frame = new RectangleF (265, 35, 50, 50);
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

				blockView.Add (backgroundImage);
				blockView.Add (whileFooterText);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

