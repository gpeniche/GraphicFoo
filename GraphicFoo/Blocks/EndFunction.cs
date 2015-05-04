using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class EndFunction : IBlock
	{
		
		public string Name {
			get {
				return "EndFunction";
			}
		}

		public string Syntax {
			get {
				return " endFunc \n";
			}
		}

		public string Example {
			get {
				return "endFunc";
			}
		}

		public string Explanation {
			get {
				return "endFunc";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/endfunction.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (191, 222, 227);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   -98,
					                   Image,
					                   "endFunc",
					                   400,
					                   new CGRect (10, 10, 200, 90),
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

