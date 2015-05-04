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
				return UIImage.FromBundle ("Graphics/functionheader.png");
			}
		}

		public UIColor Color {
			get {
				return UIColor.FromRGB (191, 222, 227);
			}
		}

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new CGRect (0, 200, 400, 90));
				blockView.Tag = 98;

				UIImageView backgroundImage = 
					new UIImageView (new CGRect (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UILabel functionText = 
					new UILabel (new CGRect (-10, 10, 80, 100));
				functionText.TextAlignment = UITextAlignment.Center;
				functionText.TextColor = Color;
				functionText.Text = "Func";
				functionText.Font = UIFont.FromName ("Orange Kid", 22f);

				UITextField functionName = 
					new UITextField (new CGRect (55, 40, 75, 40));
				functionName.Placeholder = "name";
				functionName.ShouldReturn += textField => { 
					functionName.ResignFirstResponder ();
					return true; 
				};
				functionName.AccessibilityLabel = "funcName";
				functionName.TextColor = Color;
				functionName.Font = UIFont.FromName ("Orange Kid", 22f);

				UILabel leftParenthesis = 
					new UILabel (new CGRect (130, 10, 15, 100));
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = Color;
				leftParenthesis.Text = "(";
				leftParenthesis.Font = UIFont.FromName ("Orange Kid", 50f);


				UITextField functionParameters = 
					new UITextField (new CGRect (148, 40, 100, 40));
				functionParameters.Placeholder = "Add expression";
				functionParameters.ShouldReturn += textField => { 
					functionParameters.ResignFirstResponder ();
					return true; 
				};
				functionParameters.AccessibilityLabel = "parameters";
				functionParameters.TextColor = Color;
				functionParameters.Font = UIFont.FromName ("Orange Kid", 22f);

				UILabel rightParenthesis = 
					new UILabel (new CGRect (250, 10, 15, 100));
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = Color;
				rightParenthesis.Text = "): ";
				rightParenthesis.Font = UIFont.FromName ("Orange Kid", 50f);

				UIButton functionType = new UIButton (new CGRect (255, 30, 60, 60));
				functionType.SetImage (
					UIImage.FromBundle ("Graphics/delete-icon.png"),
					UIControlState.Normal
				);
				functionType.AccessibilityLabel = "funcType";
				functionType.Tag = 3;
				functionType.AccessibilityHint = "none";

				UIView sharedViews = BlockConstructorHelper.ConstructSharedElements (
					                     new CGPoint (-20f, 20f),
					                     new CGPoint (300f, 35f)
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

