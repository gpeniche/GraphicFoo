// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GraphicFoo
{
	[Register ("GraphicFooViewController")]
	partial class GraphicFooViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField CodeTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton RunButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CodeTextField != null) {
				CodeTextField.Dispose ();
				CodeTextField = null;
			}
			if (RunButton != null) {
				RunButton.Dispose ();
				RunButton = null;
			}
		}
	}
}
