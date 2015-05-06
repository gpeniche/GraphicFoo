using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class EndIf : IBlock
	{
		
		public string Name {
			get {
				return "EndIf";
			}
		}

		public string Syntax {
			get {
				return " endIf \n";
			}
		}

		public string Example {
			get {
				return "endIf";
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
				return UIImage.FromBundle ("Graphics/endif.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (239, 137, 28);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   -98,
					                   Image,
					                   "endIf",
					                   400,
					                   new CGRect (0, 10, 300, 90),
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

