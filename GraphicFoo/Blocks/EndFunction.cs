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
				return UIImage.FromBundle ("Graphics/endloop.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   -220,
					                   Image,
					                   "endFunc",
					                   600,
					                   new CGRect (20, 10, 200, 100)
				                   );

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (465f, 8f),
					                     new CGPoint (440f, 35f)
				                     );

				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

