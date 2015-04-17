using System;
using UIKit;

namespace GraphicFoo
{
	public class Declaration : IBlock
	{
		public Declaration ()
		{
		}

		public string Name {
			get {
				return "Declaration";
			}
		}

		public string Syntax {
			get {
				return "myFirstFoo = 5";
			}
		}

		public UIImage Image {
			get {
				return UIImage.FromBundle ("monkey.png");
			}
		}

	}
}

