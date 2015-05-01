using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class Else : IBlock
	{
		
		public string Name {
			get {
				return "Else";
			}
		}

		public string Syntax {
			get {
				return " else \n";
			}
		}

		public string Example {
			get {
				return "else { myFoo = 5 }";
			}
		}

		public string Explanation {
			get {
				return " If the if expression is false the execution continues" +
				" in the following branch – either in the else block " +
				"(which is usually optional), or if there is no else" +
				" branch, then after the end If.";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/else.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (0, 200, 400, 100));
				blockView.Tag = -88;
				blockView.AccessibilityHint = "else";

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel ifFooterText = 
					new UILabel (new RectangleF (-10, 10, 300, 100));
				ifFooterText.Font = UIFont.SystemFontOfSize (24.0f);
				ifFooterText.TextAlignment = UITextAlignment.Center;
				ifFooterText.TextColor = UIColor.White;
				ifFooterText.Text = "else";

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
				blockView.Add (ifFooterText);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

