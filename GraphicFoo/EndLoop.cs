using System;
using UIKit;
using System.Drawing;

namespace GraphicFoo
{
	public class EndLoop : IBlock
	{
		public EndLoop ()
		{
		}

		public int Type{
			get{
				return 1;
			}
		}

		public string Name {
			get {
				return "EndLoop";
			}
		}

		public string Syntax {
			get {
				return "} EndLoop";
			}
		}

		public string Example {
			get {
				return "} EndLoop";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("Endloop.png");
			}
		}

		public UIView BlockView {
			get {
				var blockView = new UIView (new RectangleF(260, 200, 400, 100));
				blockView.Tag = -88;

				var backgroundImage = new UIImageView (new RectangleF(-36, -9, 400, 132));
				backgroundImage.Image = this.Image;

				var whileFooterText = new UILabel(new RectangleF(-10, 10, 300, 100));
				whileFooterText.Font = UIFont.SystemFontOfSize(24.0f);
				whileFooterText.TextAlignment = UITextAlignment.Center;
				whileFooterText.TextColor = UIColor.White;
				whileFooterText.Text = "} WhileFooter";

				var deleteBlock = UIButton.FromType(UIButtonType.Custom);
				deleteBlock.Frame = new RectangleF(290, 8, 20, 20);
				deleteBlock.SetImage(UIImage.FromFile ("delete-icon.png"), UIControlState.Normal);
				deleteBlock.Tag = 2;

				var insertPositionBtn = UIButton.FromType(UIButtonType.Custom);
				insertPositionBtn.Frame = new RectangleF(265, 35, 50, 50);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-empty.png"), UIControlState.Normal);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-full.png"), UIControlState.Selected);
				insertPositionBtn.SetImage(UIImage.FromFile ("circle-full.png"), UIControlState.Highlighted);
				insertPositionBtn.Tag = 1;

				//var lineImage = new UIImageView (UIImage.FromFile ("circle-full.png"));

				blockView.Add (backgroundImage);
				blockView.Add (whileFooterText);
				blockView.Add (deleteBlock);
				blockView.Add (insertPositionBtn);
				//blockView.Add (lineImage);

				return blockView;
			}
		}

	}
}

