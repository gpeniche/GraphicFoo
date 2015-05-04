using UIKit;
using CoreGraphics;
using Foundation;

namespace GraphicFoo
{
	public class Return : IBlock
	{

		public string Name {
			get {
				return "Return";
			}
		}

		public string Syntax {
			get {
				return " return %var% ; \n";
			}
		}

		public string Example {
			get {
				return "return myFoo ;";
			}
		}

		public string Explanation {
			get {
				return "A return statement causes execution to leave the" +
				" current subroutine and resume at the point in" +
				" the code immediately after where the subroutine" +
				" was called, known as its return address. The" +
				" return address is saved, usually on the process's" +
				" call stack, as part of the operation of making the" +
				" subroutine call";
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
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   0,
					                   Image,
					                   "Return",
					                   400,
					                   new CGRect (-10, 10, 100, 100),
					                   Color
				                   );

				UITextField returnVar = BlockConstructorHelper.CreateTextField (
					                        new CGRect (90, 10, 180, 90),
					                        "add var",
					                        "var",
					                        Color,
					                        28f
				                        );

				UILabel semicolon = 
					new UILabel (new CGRect (250, 0, 30, 100));
				semicolon.Font = UIFont.SystemFontOfSize (70.0f);
				semicolon.TextAlignment = UITextAlignment.Center;
				semicolon.TextColor = Color;
				semicolon.Text = ";";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
				                     );

				blockView.Add (returnVar);
				blockView.Add (semicolon);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

