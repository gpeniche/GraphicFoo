using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class Print : IBlock
	{

		public string Name {
			get {
				return "Print";
			}
		}

		public string Syntax {
			get {
				return " print ( %Expression% ) ; \n";
			}
		}

		public string Example {
			get {
				return "print ( \"hello world\" )";
			}
		}

		public string Explanation {
			get {
				return " Statement that prints (or displays) on the screen ";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/print.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (0, 200, 400, 100));

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel printText = 
					new UILabel (new RectangleF (-10, 10, 100, 100));
				printText.Font = UIFont.SystemFontOfSize (24.0f);
				printText.TextAlignment = UITextAlignment.Center;
				printText.TextColor = UIColor.White;
				printText.Text = "Print";

				UILabel leftParenthesis = 
					new UILabel (new RectangleF (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField printExpression = 
					new UITextField (new RectangleF (90, 10, 200, 100));
				printExpression.Placeholder = "Add expression";
				printExpression.ShouldReturn += textField => { 
					printExpression.ResignFirstResponder ();
					return true; 
				};
				printExpression.AccessibilityLabel = "Expression";
				printExpression.TextColor = UIColor.White;

				UILabel rightParenthesis = 
					new UILabel (new RectangleF (220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ") ;";

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
				blockView.Add (printText);
				blockView.Add (leftParenthesis);
				blockView.Add (printExpression);
				blockView.Add (rightParenthesis);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

