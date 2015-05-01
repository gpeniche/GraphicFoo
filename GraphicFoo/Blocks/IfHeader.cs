using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class IfHeader : IBlock
	{

		public string Name {
			get {
				return "If Header";
			}
		}

		public string Syntax {
			get {
				return " if ( %Expression% ) \n";
			}
		}

		public string Example {
			get {
				return "If ( myFoo == 5 )";
			}
		}

		public string Explanation {
			get {
				return "Conditional statements, conditional expressions and" + 
					" conditional constructs are features of a programming" + 
					" language which perform different computations or actions" +
					" depending on whether a programmer-specified boolean" +
					" condition evaluates to true or false. Apart from the" +
					" case of branch predication, this is always achieved by" +
					" selectively altering the control flow based on some" +
					" condition";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/ifheader.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (0, 200, 400, 100));
				blockView.Tag = 88;

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel ifText = 
					new UILabel (new RectangleF (-10, 10, 100, 100));
				ifText.Font = UIFont.SystemFontOfSize (24.0f);
				ifText.TextAlignment = UITextAlignment.Center;
				ifText.TextColor = UIColor.White;
				ifText.Text = "If";

				UILabel leftParenthesis = 
					new UILabel (new RectangleF (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField ifExpression = 
					new UITextField (new RectangleF (90, 10, 200, 100));
				ifExpression.Placeholder = "Add expression";
				ifExpression.ShouldReturn += textField => { 
					ifExpression.ResignFirstResponder ();
					return true; 
				};
				ifExpression.AccessibilityLabel = "Expression";
				ifExpression.TextColor = UIColor.White;

				UILabel rightParenthesis = 
					new UILabel (new RectangleF (220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ") ";

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
				blockView.Add (ifText);
				blockView.Add (leftParenthesis);
				blockView.Add (ifExpression);
				blockView.Add (rightParenthesis);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

