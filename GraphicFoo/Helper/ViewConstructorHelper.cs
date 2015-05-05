using System;
using UIKit;
using CoreGraphics;

namespace GraphicFoo
{
	public static class ViewConstructorHelper
	{
		public static UIButton LoadMenuButton ()
		{
			UIButton menuButton = new UIButton (UIButtonType.Custom);
			menuButton.Frame = new CGRect (670, 0, 100, 120);
			menuButton.SetImage (
				UIImage.FromBundle ("Graphics/menu.png"),
				UIControlState.Normal
			);
			return menuButton;
		}

		/// <summary>
		/// Creates the console.
		/// </summary>
		/// <returns>The console.</returns>
		/// <param name="consoleFrame">Console frame.</param>
		public static UIView CreateConsole (CGRect consoleFrame)
		{
			UIView consoleView = new UIView ();
			consoleView.Frame = consoleFrame;
			consoleView.Layer.BorderWidth = 2.0f;
			consoleView.Layer.BorderColor = new CGColor (255, 255, 0);
			consoleView.BackgroundColor = UIColor.Black;

			UITextView textOnConsole = new UITextView ();
			textOnConsole.Frame = new CGRect (
				0,
				20,
				consoleView.Frame.Size.Width,
				consoleView.Frame.Size.Height - 10f
			);
			textOnConsole.TextAlignment = UITextAlignment.Left;
			textOnConsole.TextColor = UIColor.FromRGB (255, 255, 0);
			textOnConsole.BackgroundColor = UIColor.Black;
			textOnConsole.Editable = false;
			textOnConsole.Font = UIFont.FromName ("Orange Kid", 20f);
			textOnConsole.Tag = 1;

			UILabel consoleTitle = new UILabel ();
			consoleTitle.Frame = new CGRect (
				4,
				2,
				consoleView.Frame.Size.Width,
				20f
			);
			consoleTitle.TextAlignment = UITextAlignment.Left;
			consoleTitle.TextColor = UIColor.FromRGB (255, 255, 0);
			consoleTitle.Text = "FooConsole: ";
			consoleTitle.Font = UIFont.FromName ("Orange Kid", 24f);

			consoleView.Add (textOnConsole);
			consoleView.Add (consoleTitle);

			return consoleView;
		}

		public static UIButton CreatePlayButton ()
		{
			UIButton playButton = new UIButton (UIButtonType.Custom);
			playButton.Frame = new CGRect (580, 20, 70, 80);
			playButton.SetImage (
				UIImage.FromBundle ("Graphics/play-button.png"),
				UIControlState.Normal
			);
			return playButton;
		}
	}
}

