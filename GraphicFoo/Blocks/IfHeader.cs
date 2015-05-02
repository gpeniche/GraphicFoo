using UIKit;
using CoreGraphics;

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
				return " if ( %expression% ) \n";
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
					new UIView (new CGRect (0, 200, 400, 100));
				blockView.Tag = 88;

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel ifText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				ifText.Font = UIFont.SystemFontOfSize (24.0f);
				ifText.TextAlignment = UITextAlignment.Center;
				ifText.TextColor = UIColor.White;
				ifText.Text = "If";

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField ifExpression = 
					new UITextField (new CGRect (90, 10, 200, 100));
				ifExpression.Placeholder = "Add expression";
				ifExpression.ShouldReturn += textField => { 
					ifExpression.ResignFirstResponder ();
					return true; 
				};
				ifExpression.AccessibilityLabel = "expression";
				ifExpression.TextColor = UIColor.White;

				UILabel rightParenthesis = 
					new UILabel (new CGRect (220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ")";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (290f, 8f),
					                     new CGPoint (265f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (ifText);
				blockView.Add (leftParenthesis);
				blockView.Add (ifExpression);
				blockView.Add (rightParenthesis);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

