using UIKit;
using CoreGraphics;
using Foundation;

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

		public UIColor Color {
			get {
				return UIColor.FromRGB (47, 195, 23);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));
				blockView.Tag = 98;

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel whileText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				whileText.TextAlignment = UITextAlignment.Center;
				whileText.TextColor = Color;
				whileText.Text = "While";
				whileText.Font = UIFont.FromName ("Orange Kid", 28f);

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 10, 50, 100));
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = Color;
				leftParenthesis.Text = "(";
				leftParenthesis.Font = UIFont.FromName ("Orange Kid", 55f);

				UITextField whileExpression = BlockConstructorHelper.CreateTextField (
					                              new CGRect (90, 10, 170, 100),
					                              "add expression",
					                              "expression",
					                              Color,
					                              28f
				                              );

				UILabel rightParenthesis = 
					new UILabel (new CGRect (220, 10, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (60.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = Color;
				rightParenthesis.Text = ")";
				rightParenthesis.Font = UIFont.FromName ("Orange Kid", 55f);

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
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

