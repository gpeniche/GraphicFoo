using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class LoopHeader : IBlock
	{

		public string Name {
			get {
				return "While Header";
			}
		}

		public string Syntax {
			get {
				return " while ( %expression% ) \n";
			}
		}

		public string Example {
			get {
				return "While ( myFoo == 5 )";
			}
		}

		public string Explanation {
			get {
				return "A while loop is a control flow statement that allows" +
				" code to be executed repeatedly based on a given boolean" +
				" condition. The while loop can be thought of as a" +
				" repeating if statement, here you start your while" +
				" statement. ";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/loopheader.png");
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

				UILabel whileText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				whileText.Font = UIFont.SystemFontOfSize (24.0f);
				whileText.TextAlignment = UITextAlignment.Center;
				whileText.TextColor = UIColor.White;
				whileText.Text = "While";

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField whileExpression = 
					new UITextField (new CGRect (90, 10, 200, 100));
				whileExpression.Placeholder = "Add expression";
				whileExpression.ShouldReturn += textField => { 
					whileExpression.ResignFirstResponder ();
					return true; 
				};
				whileExpression.AccessibilityLabel = "expression";
				whileExpression.TextColor = UIColor.White;

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
				blockView.Add (whileText);
				blockView.Add (leftParenthesis);
				blockView.Add (whileExpression);
				blockView.Add (rightParenthesis);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

