using System;
using UIKit;
using System.Drawing;
using Foundation;

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
					new UIView (new RectangleF (0, 200, 400, 100));

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;

				UITextField varName = new UITextField (new RectangleF (10, 5, 100, 100));
				varName.Placeholder = "var name";
				varName.ShouldReturn += textField => { 
					varName.ResignFirstResponder ();
					return true; 
				};
				varName.AccessibilityLabel = "varName";
				varName.TextColor = UIColor.White;

				UILabel equalLabel = 
					new UILabel (new RectangleF (120, 0, 50, 100));
				equalLabel.Font = UIFont.SystemFontOfSize (80.0f);
				equalLabel.TextAlignment = UITextAlignment.Center;
				equalLabel.TextColor = UIColor.White;
				equalLabel.Text = "=";

				UITextField varValue = 
					new UITextField (new RectangleF (170, 5, 220, 100));
				varValue.Placeholder = "add value";
				varValue.ShouldReturn += textField => { 
					varValue.ResignFirstResponder ();
					return true; 
				};
				varValue.AccessibilityLabel = "varValue";
				varValue.TextColor = UIColor.White;

				UIButton deleteBlock = UIButton.FromType (UIButtonType.Custom);
				deleteBlock.Frame = new RectangleF (290, 8, 20, 20);
				deleteBlock.SetImage (
					UIImage.FromFile ("Graphics/delete-icon.png"),
					UIControlState.Normal
				);
				deleteBlock.Tag = 2;

				UIButton insertPositionBtn = UIButton.FromType (UIButtonType.Custom);
				insertPositionBtn.Frame = new RectangleF (265, 35, 50, 50);
				insertPositionBtn.SetImage (
					UIImage.FromBundle ("Graphics/circle-empty.png"),
					UIControlState.Normal
				);
				insertPositionBtn.SetImage (
					UIImage.FromBundle ("Graphics/circle-full.png"),
					UIControlState.Selected
				);
				insertPositionBtn.SetImage (
					UIImage.FromBundle ("Graphics/circle-full.png"),
					UIControlState.Highlighted
				);
				insertPositionBtn.Tag = 1;

				blockView.Add (backgroundImage);
				blockView.Add (varName);
				blockView.Add (equalLabel);
				blockView.Add (varValue);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

