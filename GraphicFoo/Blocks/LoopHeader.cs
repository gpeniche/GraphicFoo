using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class LoopHeader : IBlock
	{

		public int Type {
			get {
				return 1;
			}
		}

		public string Name {
			get {
				return "LoopHeader";
			}
		}

		public string Syntax {
			get {
				return " loopHeader ( %Expression% ) ";
			}
		}

		public string Example {
			get {
				return "LoopHeader ()";
			}
		}

		public string Explanation {
			get {
				return "LoopHeader ()";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/Loopheader.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (260, 200, 400, 100));
				blockView.Tag = 88;

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel whileText = 
					new UILabel (new RectangleF (-10, 10, 100, 100));
				whileText.Font = UIFont.SystemFontOfSize (24.0f);
				whileText.TextAlignment = UITextAlignment.Center;
				whileText.TextColor = UIColor.White;
				whileText.Text = "While";

				UILabel leftParenthesis = 
					new UILabel (new RectangleF (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField whileStatement = 
					new UITextField (new RectangleF (90, 10, 200, 100));
				whileStatement.Placeholder = "Add statement";
				whileStatement.ShouldReturn += textField => { 
					whileStatement.ResignFirstResponder ();
					return true; 
				};
				whileStatement.AccessibilityLabel = "Expression";

				UILabel rightParenthesis = 
					new UILabel (new RectangleF (220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ") {";

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
				blockView.Add (whileText);
				blockView.Add (leftParenthesis);
				blockView.Add (whileStatement);
				blockView.Add (rightParenthesis);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

