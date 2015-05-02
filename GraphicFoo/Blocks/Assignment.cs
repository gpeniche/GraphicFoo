using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class Assignment : IBlock
	{

		public string Name {
			get {
				return "Assignment";
			}
		}

		public string Syntax {
			get {
				return " %varName% = %varValue% ; \n";
			}
		}

		public string Example {
			get {
				return "myFoo = 5";
			}
		}

		public string Explanation {
			get {
				return "Assignment statement sets and/or re-sets the value" +
				" stored in the storage location(s) denoted by a variable" +
				" name; in other words, it copies the value into the" +
				" variable. In most imperative programming languages," +
				" the assignment statement (or expression) is a" +
				" fundamental construct";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/assignment.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 100));

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UITextField varName = new UITextField (new CGRect (10, 5, 100, 100));
				varName.Placeholder = "var name";
				varName.ShouldReturn += textField => { 
					varName.ResignFirstResponder ();
					return true; 
				};
				varName.AccessibilityLabel = "varName";
				varName.TextColor = UIColor.White;

				UILabel equalLabel = 
					new UILabel (new CGRect (120, 0, 50, 100));
				equalLabel.Font = UIFont.SystemFontOfSize (80.0f);
				equalLabel.TextAlignment = UITextAlignment.Center;
				equalLabel.TextColor = UIColor.White;
				equalLabel.Text = "=";

				UITextField varValue = 
					new UITextField (new CGRect (170, 5, 220, 100));
				varValue.Placeholder = "add value";
				varValue.ShouldReturn += textField => { 
					varValue.ResignFirstResponder ();
					return true; 
				};
				varValue.AccessibilityLabel = "varValue";
				varValue.TextColor = UIColor.White;

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (290f, 8f),
					                     new CGPoint (265f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (varName);
				blockView.Add (equalLabel);
				blockView.Add (varValue);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

