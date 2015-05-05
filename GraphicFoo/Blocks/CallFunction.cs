using UIKit;
using CoreGraphics;
using Foundation;

namespace GraphicFoo
{
	public class CallFunction : IBlock
	{

		public string Name {
			get {
				return "Call Function";
			}
		}

		public string Syntax {
			get {
				return " function %funcName% ; \n";
			}
		}

		public string Example {
			get {
				return "funcFoo()";
			}
		}

		public string Explanation {
			get {
				return "Call a function you previously defined";
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

				UITextField funcName = BlockConstructorHelper.CreateTextField (
					                       new CGRect (15, 5, 260, 100),
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
				blockView.Add (funcName);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

