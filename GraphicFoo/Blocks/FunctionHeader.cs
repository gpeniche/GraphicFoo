using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public class FunctionHeader : IBlock
	{

		public string Name {
			get {
				return "Function Header";
			}
		}

		public string Syntax {
			get {
				return " function %funcName% ( %parameters% ) : %funcType% \n";
			}
		}

		public string Example {
			get {
				return "Function foo (myFoo):void";
			}
		}

		public string Explanation {
			get {
				return "A named section of a program that performs a specific" +
				" task. In this sense, a function is a type of procedure" +
				" or routine. Some programming languages make a" +
				" distinction between a function, which returns a value," +
				" and a procedure, which performs some operation but does" +
				" not return a value.\nMost programming languages come" +
				" with a prewritten set of functions that are kept in a" +
				" library. You can also write your own functions to" +
				" perform specialized tasks.";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Graphics/loopheader.png");
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 600, 100));
				blockView.Tag = 220;

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 600, 132));
				backgroundImage.Image = Image;

				UILabel functionText = 
					new UILabel (new CGRect (5, 10, 100, 100));
				functionText.Font = UIFont.SystemFontOfSize (20.0f);
				functionText.TextAlignment = UITextAlignment.Center;
				functionText.TextColor = UIColor.White;
				functionText.Text = "Func";

				UITextField functionName = 
					new UITextField (new CGRect (90, 10, 100, 100));
				functionName.Placeholder = "name";
				functionName.ShouldReturn += textField => { 
					functionName.ResignFirstResponder ();
					return true; 
				};
				functionName.AccessibilityLabel = "funcName";
				functionName.TextColor = UIColor.White;

				UILabel leftParenthesis = 
					new UILabel (new CGRect (150, 0, 15, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize (65.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				UITextField functionParameters = 
					new UITextField (new CGRect (170, 10, 180, 100));
				functionParameters.Placeholder = "Add expression";
				functionParameters.ShouldReturn += textField => { 
					functionParameters.ResignFirstResponder ();
					return true; 
				};
				functionParameters.AccessibilityLabel = "parameters";
				functionParameters.TextColor = UIColor.White;

				UILabel rightParenthesis = 
					new UILabel (new CGRect (350, 0, 15, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize (65.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = "): ";

				UIButton functionType = new UIButton (new CGRect (365, 16, 80, 80));
				functionType.SetImage (
					UIImage.FromBundle ("Graphics/delete-icon.png"),
					UIControlState.Normal
				);
				functionType.AccessibilityLabel = "funcType";
				functionType.Tag = 3;
				functionType.AccessibilityHint = "none";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (465f, 8f),
					                     new CGPoint (440f, 35f)
				                     );

				blockView.Add (backgroundImage);
				blockView.Add (functionText);
				blockView.Add (functionName);
				blockView.Add (leftParenthesis);
				blockView.Add (functionParameters);
				blockView.Add (rightParenthesis);
				blockView.Add (functionType);
				foreach (UIView view in sharedViews.Subviews) {
					blockView.Add (view);
				}

				return blockView;
			}
		}
	}
}

