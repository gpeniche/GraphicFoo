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
				return "endWhile";
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
					                   -88,
					                   Image,
					                   "endWhile",
					                   400,
					                   new CGRect (-10, 10, 300, 100)
				                   );

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (290f, 8f),
					                     new CGPoint (265f, 35f)
				                     );

				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

