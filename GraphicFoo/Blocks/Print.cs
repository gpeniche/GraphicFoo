using UIKit;
using CoreGraphics;

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
				return " print ( %expression% ) ; \n";
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
					new UIView (new CGRect (0, 200, 400, 100));

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel printText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				printText.Font = UIFont.SystemFontOfSize (24.0f);
				printText.TextAlignment = UITextAlignment.Center;
				printText.TextColor = UIColor.White;
				printText.Text = "Print";

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField printExpression = 
					new UITextField (new CGRect (90, 10, 200, 100));
				printExpression.Placeholder = "Add expression";
				printExpression.ShouldReturn += textField => { 
					printExpression.ResignFirstResponder ();
					return true; 
				};
				printExpression.AccessibilityLabel = "expression";
				printExpression.TextColor = UIColor.White;

				UILabel rightParenthesis = 
					new UILabel (new CGRect (220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ") ;";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (290f, 8f),
					                     new CGPoint (265f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (printText);
				blockView.Add (leftParenthesis);
				blockView.Add (printExpression);
				blockView.Add (rightParenthesis);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

