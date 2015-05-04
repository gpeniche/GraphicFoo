using UIKit;
using CoreGraphics;
using Foundation;

namespace GraphicFoo
{
	public class CallAssignFunction : IBlock
	{

		public string Name {
			get {
				return "Call-Assign Function";
			}
		}

		public string Syntax {
			get {
				return " %varName% = %funcName% ; \n";
			}
		}

		public string Example {
			get {
				return "myFoo = funcFoo()";
			}
		}

		public string Explanation {
			get {
				return "Call a function you previously defined and assign the" +
				" returning value to an existing variable";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/return.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (191, 222, 227);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UITextField varName = BlockConstructorHelper.CreateTextField (
					                      new CGRect (15, 12, 100, 90),
					                      "var name",
					                      "varName",
					                      Color,
					                      28f
				                      );

				UILabel equalLabel = 
					new UILabel (new CGRect (120, 0, 50, 100));
				equalLabel.TextAlignment = UITextAlignment.Center;
				equalLabel.TextColor = Color;
				equalLabel.Text = "=";
				equalLabel.Font = UIFont.FromName ("Orange Kid", 70.0f);

				UITextField funcName = BlockConstructorHelper.CreateTextField (
					                       new CGRect (170, 5, 130, 100),
					                       "function to call",
					                       "funcName",
					                       Color,
					                       28f
				                       );

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (varName);
				blockView.Add (equalLabel);
				blockView.Add (funcName);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

