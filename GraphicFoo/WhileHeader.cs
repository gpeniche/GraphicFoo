using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class WhileHeader : IBlock
	{
		public WhileHeader ()
		{
		}

		public int Type{
			get{
				return 1;
			}
		}

		public string Name {
			get {
				return "WhileHeader";
			}
		}

		public string Syntax {
			get {
				return "WhileHeader ()";
			}
		}

		public string Example {
			get {
				return "WhileHeader ()";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Whileheader.png");
			}
		}

		public UIView BlockView {
			get {
				var blockView = new UIView (new RectangleF(260, 200, 400, 100));
				//blockView.BackgroundColor = UIColor.Blue;

				var backgroundImage = new UIImageView (new RectangleF(-36, -9, 400, 132));
				backgroundImage.Image = this.Image;

				var whileText = new UILabel(new RectangleF(-10, 10, 100, 100));
				whileText.Font = UIFont.SystemFontOfSize(24.0f);
				whileText.TextAlignment = UITextAlignment.Center;
				whileText.TextColor = UIColor.White;
				whileText.Text = "While";

				var leftParenthesis = new UILabel(new RectangleF(55, 0, 50, 100));
				leftParenthesis.Font = UIFont.SystemFontOfSize(75.0f);
				leftParenthesis.TextAlignment = UITextAlignment.Center;
				leftParenthesis.TextColor = UIColor.White;
				leftParenthesis.Text = "(";

				var whileStatement = new UITextField(new RectangleF(90, 10, 200, 100));
				whileStatement.Placeholder = "Add statement";

				var rightParenthesis = new UILabel(new RectangleF(220, 0, 70, 100));
				rightParenthesis.Font = UIFont.SystemFontOfSize(80.0f);
				rightParenthesis.TextAlignment = UITextAlignment.Center;
				rightParenthesis.TextColor = UIColor.White;
				rightParenthesis.Text = ") {";

				var deleteBlock = UIButton.FromType(UIButtonType.Custom);
				deleteBlock.Frame = new RectangleF(290, 8, 20, 20);
				deleteBlock.SetImage(UIImage.FromFile ("delete-icon.png"), UIControlState.Normal);
				deleteBlock.Tag = 2;

				var insertPositionBtn = UIButton.FromType(UIButtonType.Custom);
				insertPositionBtn.Frame = new System.Drawing.RectangleF(250, 20, 80, 80);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-empty.png"), UIControlState.Normal);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-full.png"), UIControlState.Selected);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-full.png"), UIControlState.Highlighted);
				insertPositionBtn.Tag = 1;

				//var lineImage = new UIImageView (UIImage.FromFile ("circle-full.png"));

				blockView.Add (backgroundImage);
				blockView.Add (whileText);
				blockView.Add (leftParenthesis);
				blockView.Add (whileStatement);
				blockView.Add (rightParenthesis);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);
				//blockView.Add (lineImage);

				return blockView;
			}
		}

	}
}

