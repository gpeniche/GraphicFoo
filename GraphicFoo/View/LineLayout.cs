using System;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreAnimation;

namespace GraphicFoo
{
	public class LineLayout : UICollectionViewFlowLayout
	{
		public const float ITEM_SIZE = 260.0f;
		public const int ACTIVE_DISTANCE = 200;
		public const float ZOOM_FACTOR = 0.3f;

		public LineLayout ()
		{
			ItemSize = new CGSize (ITEM_SIZE, 200f);
			ScrollDirection = UICollectionViewScrollDirection.Vertical;
			SectionInset = new UIEdgeInsets (0, 0, 0, 0);
			MinimumLineSpacing = 0.0f;
		}

		public override bool ShouldInvalidateLayoutForBoundsChange (CGRect newBounds)
		{
			return true;
		}
	}
}

