using UIKit;
using CoreGraphics;
using Foundation;

namespace GraphicFoo
{
	public class Declaration : IBlock
	{

		public string Name {
			get {
				return "Declaration";
			}
		}

		public string Syntax {
			get {
				return " %varType% %varName% ; \n";
			}
		}

		public string Example {
			get {
				return " number myFoo ";
			}
		}

		public string Explanation {
			get {
				return " Declaration specifies properties of an identifier:" +
				" it declares what a word (identifier) means." +
				" Declarations are most commonly used for functions," +
				" variables, constants, and classes, but can also be used" +
				" for other entities such as enumerations and type" +
				" definitions.";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/declaration.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (10, 214, 140);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;
				backgroundImage.Tag = 0;

				UIButton varType = new UIButton (new CGRect (15, 20, 90, 90));
				varType.SetImage (
					UIImage.FromFile ("Graphics/delete-icon.png"),
					UIControlState.Normal
				);
				varType.AccessibilityLabel = "varType";
				varType.Tag = 3;
				varType.AccessibilityHint = "none";

				UITextField varName = BlockConstructorHelper.CreateTextField (
					                      new CGRect (120, 5, 180, 100),
					                      "var name",
					                      "varName",
					                      Color,
					                      28f
				                      );

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (290f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (varType);
				blockView.Add (varName);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

