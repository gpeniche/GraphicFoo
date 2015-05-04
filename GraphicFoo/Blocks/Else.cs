using UIKit;
using CoreGraphics;

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
				return "else myFoo = 5 ";
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

		public UIColor Color {
			get {
				return UIColor.FromRGB (239, 137, 28);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));
				blockView.Tag = -98;
				blockView.AccessibilityHint = "else";

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel ifFooterText = 
					new UILabel (new CGRect (-10, 10, 300, 90));
				ifFooterText.TextAlignment = UITextAlignment.Center;
				ifFooterText.TextColor = Color;
				ifFooterText.Text = "else";
				ifFooterText.Font = UIFont.FromName ("Orange Kid", 28f);

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (ifFooterText);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

