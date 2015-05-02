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
				return UIImage.FromBundle ("Graphics/print.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = BlockConstructorHelper.ContructEndBlock (
					                   0,
					                   Image,
					                   "Return",
					                   400,
					                   new CGRect (-10, 10, 100, 100)
				                   );

				UITextField returnVar = 
					new UITextField (new CGRect (90, 10, 200, 100));
				returnVar.Placeholder = "Add var";
				returnVar.ShouldReturn += textField => { 
					returnVar.ResignFirstResponder ();
					return true; 
				};
				returnVar.AccessibilityLabel = "var";
				returnVar.TextColor = UIColor.White;

				UILabel semicolon = 
					new UILabel (new CGRect (220, 0, 70, 100));
				semicolon.Font = UIFont.SystemFontOfSize (80.0f);
				semicolon.TextAlignment = UITextAlignment.Center;
				semicolon.TextColor = UIColor.White;
				semicolon.Text = ";";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (290f, 8f),
					                     new CGPoint (265f, 35f)
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

