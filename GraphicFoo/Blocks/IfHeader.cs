using UIKit;
using CoreGraphics;
using Foundation;

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

		public UIColor Color {
			get {
				return UIColor.FromRGB (239, 137, 28);
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

				UILabel ifText = 
					new UILabel (new CGRect (-10, 10, 100, 100));
				ifText.TextAlignment = UITextAlignment.Center;
				ifText.TextColor = Color;
				ifText.Text = "If";
				ifText.Font = UIFont.FromName ("Orange Kid", 28f);

				UILabel leftParenthesis = 
					new UILabel (new CGRect (55, 10, 50, 100));
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = Color;
				leftParenthesis.Text = "(";
				leftParenthesis.Font = UIFont.FromName ("Orange Kid", 55f);

				UITextField ifExpression = BlockConstructorHelper.CreateTextField (
					                           new CGRect (90, 10, 190, 100),
					                           "add expression",
					                           "expression",
					                           Color,
					                           28f
				                           );

				UILabel rightParenthesis = 
					new UILabel (new CGRect (240, 10, 70, 100));
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

