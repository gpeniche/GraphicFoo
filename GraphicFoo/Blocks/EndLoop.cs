using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class EndLoop : IBlock
	{
		
		public string Name {
			get {
				return "EndWhile";
			}
		}

		public string Syntax {
			get {
				return " endWhile \n";
			}
		}

		public string Example {
			get {
				return "endWhile";
			}
		}

		public string Explanation {
			get {
				return "Statement that closes and defines where your" +
				" function ends";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/endloop.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (47, 195, 23);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   -98,
					                   Image,
					                   "endWhile",
					                   400,
					                   new CGRect (-10, 10, 300, 90),
					                   Color
				                   );

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
				                     );

				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

