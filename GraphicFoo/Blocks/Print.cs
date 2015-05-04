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

		public UIColor Color {
			get {
				return UIColor.FromRGB (28, 55, 156);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel printText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				printText.TextAlignment = UITextAlignment.Center;
				printText.TextColor = Color;
				printText.Text = "Print";
				printText.Font = UIFont.FromName ("Orange Kid", 28f);

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 10, 50, 100));
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = Color;
				leftParenthesis.Text = "(";
				leftParenthesis.Font = UIFont.FromName ("Orange Kid", 55f);

				UITextField printExpression = 
					new UITextField (new CGRect (90, 10, 180, 100));
				printExpression.Placeholder = "Add expression";
				printExpression.ShouldReturn += textField => { 
					printExpression.ResignFirstResponder ();
					return true; 
				};
				printExpression.AccessibilityLabel = "expression";
				printExpression.TextColor = Color;
				printExpression.Font = UIFont.FromName ("Orange Kid", 28f);

				UILabel rightParenthesis = 
					new UILabel (new CGRect (250, 10, 70, 100));
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = Color;
				rightParenthesis.Text = ");";
				rightParenthesis.Font = UIFont.FromName ("Orange Kid", 55f);

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
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

