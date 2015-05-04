using UIKit;
using CoreGraphics;

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
				return "";
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

				UITextField returnVar = 
					new UITextField (new CGRect (90, 10, 180, 90));
				returnVar.Placeholder = "Add var";
				returnVar.ShouldReturn += textField => { 
					returnVar.ResignFirstResponder ();
					return true; 
				};
				returnVar.AccessibilityLabel = "var";
				returnVar.TextColor = Color;
				returnVar.Font = UIFont.FromName ("Orange Kid", 28f);

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

