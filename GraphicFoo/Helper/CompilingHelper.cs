using System;
using UIKit;
using System.Collections.Generic;

namespace GraphicFoo
{
	public static class CompilingHelper
	{
		/// <summary>
		/// Prepares the string to compile and sends it to the scanner.
		/// </summary>
		/// <returns>The errors after compiling.</returns>
		/// <param name="stringToCompile">String to compile.</param>
		/// <param name="blocksOnView">Blocks on view.</param>
		public static string SendToCompile (string stringToCompile, List<UIView> blocksOnView)
		{
			string stringForScanner = stringToCompile;
			foreach (UIView activeview in blocksOnView) {
				string subStr = string.Empty, originalsubStr;
				foreach (UIView view in activeview.Subviews) {
					if (view.Class.Name == "UITextField" ||
					    (view.Class.Name == "UIButton" && view.Tag == 3)) {
						//Updates string to send to parser and scanner

						if (string.IsNullOrWhiteSpace (subStr)) {
							int fIndex = stringToCompile.IndexOf (
								             "%" + blocksOnView.IndexOf (activeview) + "%"
							             );
							int sIndex = stringToCompile.IndexOf (
								             "%" + (blocksOnView.IndexOf (activeview) - 1) + "%"
							             );
							originalsubStr = subStr = stringToCompile.Substring (
								sIndex,
								fIndex - sIndex
							);
						} else {
							originalsubStr = subStr;
						}
						if (view.Class.Name == "UITextField") {
							subStr = subStr.Replace (
								"%" + view.AccessibilityLabel + "%",
								((UITextField)view).Text
							);
						} else {
							subStr = subStr.Replace (
								"%" + view.AccessibilityLabel + "%",
								((UIButton)view).AccessibilityHint
							);
						}
						stringForScanner = stringForScanner.Replace (
							originalsubStr,
							subStr
						);
					}
				}
			}
			for (int e = -1; e <= blocksOnView.Count; e++) {
				stringForScanner = stringForScanner.Replace ("%" + e + "%", "");
			}
			Console.WriteLine ("stringForScanner: " + stringForScanner);
			Console.WriteLine ("stringToCompile: " + stringToCompile);
			Scanner scanner = new Scanner (stringForScanner);
			Parser parser = new Parser (scanner);
			parser.Parse ();
			string errorMessage = 
				(!string.IsNullOrEmpty (parser.errors.errorMessage)) ? 
				parser.errors.errorMessage : 
				"None";
			return errorMessage;
		}

		/// <summary>
		/// Adds the text to the compiling string.
		/// </summary>
		/// <returns>The text for the compiling string.</returns>
		/// <param name="stringToCompile">String to compile.</param>
		/// <param name="textToAdd">Text to add to the compiling string.</param>
		/// <param name="blocksOnView">Blocks on view.</param>
		/// <param name="lastSelected">Last element selected on the view.</param>
		public static string AddTextToCompilingString (
			string stringToCompile,
			string textToAdd,
			List<UIView> blocksOnView,
			UIButton lastSelected)
		{
			if (lastSelected == null) {
				if (blocksOnView.Count == 0 && stringToCompile.IndexOf ("%0%") < 0) {
					stringToCompile = stringToCompile.Replace (
						"%-1%",
						"%-1% \n %0%"
					);
				} 
				stringToCompile = stringToCompile.Replace (
					"%0%",
					textToAdd + "%0%"
				);
			} else {
				int index = blocksOnView.IndexOf (lastSelected.Superview);
				if (stringToCompile.Contains ("%" + (index + 1) + "%")) {
					stringToCompile = ArrangeIndexes (stringToCompile, blocksOnView, index, true);
				}
				stringToCompile = stringToCompile.Replace (
					"%" + index + "%",
					"%" + index + "%" + textToAdd + "%" + (index + 1) + "%"
				);
			}
			return stringToCompile;
		}

		/// <summary>
		/// Removes the text from compiling string.
		/// </summary>
		/// <returns>The text for the compiling string.</returns>
		/// <param name="stringToCompile">String to compile.</param>
		/// <param name="blocksOnView">Blocks on view.</param>
		/// <param name="index">Index from where we're going to remove the text.</param>
		public static string RemoveTextFromCompilingString (string stringToCompile, List<UIView> blocksOnView, int index)
		{
			int fIndex = stringToCompile.IndexOf ("%" + index + "%");
			int sIndex = stringToCompile.IndexOf ("%" + (index - 1) + "%");
			string strToDelete = stringToCompile.Substring (
				                     sIndex,
				                     fIndex - sIndex
			                     );
			stringToCompile = stringToCompile.Replace (
				strToDelete,
				""
			);
			stringToCompile = ArrangeIndexes (stringToCompile, blocksOnView, index, false);
			return stringToCompile;
		}

		/// <summary>
		/// Arranges the indexes.
		/// </summary>
		/// <returns>The indexes.</returns>
		/// <param name="stringToCompile">String to compile.</param>
		/// <param name="blocksOnView">Blocks on view.</param>
		/// <param name="fromIndex">From index to start the arrange.</param>
		/// <param name="goingUp">If set to <c>true</c> going up.</param>
		public static string ArrangeIndexes (string stringToCompile, List<UIView> blocksOnView, int fromIndex, bool goingUp)
		{
			if (goingUp) {
				for (int i = blocksOnView.Count - 1; i > fromIndex; i--) {
					stringToCompile = stringToCompile.Replace (
						"%" + i + "%",
						"%" + (i + 1) + "%"
					);
				}
			} else {
				for (int i = fromIndex; i <= blocksOnView.Count; i++) {

					stringToCompile = stringToCompile.Replace (
						"%" + i + "%",
						"%" + (i - 1) + "%"
					);
				}
			}
			return stringToCompile;
		}
	}
}

