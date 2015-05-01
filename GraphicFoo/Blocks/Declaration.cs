using System;
using UIKit;
using System.Drawing;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;

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

		public UIView BlockView {
			get {
				UIView blockView = 
					new UIView (new RectangleF (0, 200, 400, 100));

				UIImageView backgroundImage = 
					new UIImageView (new RectangleF (-36, -9, 400, 132));
				backgroundImage.Image = Image;
				backgroundImage.Tag = 0;

				UIButton varType = new UIButton (new RectangleF (10, 5, 100, 100));
				varType.SetImage (
					UIImage.FromFile ("Graphics/delete-icon.png"),
					UIControlState.Normal
				);
				varType.AccessibilityLabel = "varType";
				varType.Tag = 3;
				varType.AccessibilityHint = "none";

				UITextField varName = 
					new UITextField (new RectangleF (170, 5, 220, 100));
				varName.Placeholder = "var name";
				varName.ShouldReturn += textField => { 
					varName.ResignFirstResponder ();
					return true; 
				};
				varName.AccessibilityLabel = "varName";
				varName.TextColor = UIColor.White;

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
				blockView.Add (varType);
				blockView.Add (varName);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);

				return blockView;
			}
		}

	}
}

