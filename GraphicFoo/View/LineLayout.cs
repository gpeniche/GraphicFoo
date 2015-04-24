using System;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreAnimation;

namespace GraphicFoo
{
	public class LineLayout : UICollectionViewFlowLayout
	{
		public const float ITEM_SIZE = 200.0f;
		public const int ACTIVE_DISTANCE = 200;
		public const float ZOOM_FACTOR = 0.3f;

		public LineLayout ()
		{
			ItemSize = new CGSize (ITEM_SIZE, ITEM_SIZE);
			ScrollDirection = UICollectionViewScrollDirection.Vertical;
			SectionInset = new UIEdgeInsets (0, 0, 0, 0);
			MinimumLineSpacing = 0.0f;
		}

		public override bool ShouldInvalidateLayoutForBoundsChange (CGRect newBounds)
		{
			return true;
		}

		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect (CGRect rect)
		{ 
			UICollectionViewLayoutAttributes[] array;
			array = base.LayoutAttributesForElementsInRect (rect);
			CGRect visibleRect = new CGRect (
				                     CollectionView.ContentOffset,
				                     CollectionView.Bounds.Size
			                     );
			foreach (var attributes in array) {
				if (attributes.Frame.IntersectsWith (rect)) {
					float distance = (float)(visibleRect.GetMidX () - attributes.Center.X);
					float normalizedDistance = distance / ACTIVE_DISTANCE;
					if (Math.Abs (distance) < ACTIVE_DISTANCE) {
						float zoom = 1 + ZOOM_FACTOR * (1 - Math.Abs (normalizedDistance));
						attributes.Transform3D = CATransform3D.MakeScale (zoom, zoom, 1.0f);
						attributes.ZIndex = 1;
					}
				}
			}
			return array;
		}

		public override CGPoint TargetContentOffset (
			CGPoint proposedContentOffset, 
			CGPoint scrollingVelocity)
		{
			float offSetAdjustment = float.MaxValue;
			float horizontalCenter = (float)(proposedContentOffset.X +
			                         (CollectionView.Bounds.Size.Width / 2.0));
			CGRect targetRect = new CGRect (
				                    proposedContentOffset.X, 0.0f,
				                    CollectionView.Bounds.Size.Width,
				                    CollectionView.Bounds.Size.Height
			                    );
			var array = base.LayoutAttributesForElementsInRect (targetRect);
			foreach (UICollectionViewLayoutAttributes layoutAttributes in array) {
				float itemHorizontalCenter = (float)layoutAttributes.Center.X;
				if (Math.Abs (itemHorizontalCenter - horizontalCenter) < Math.Abs (offSetAdjustment)) {
					offSetAdjustment = itemHorizontalCenter - horizontalCenter;
				}
			}
			return new CGPoint (proposedContentOffset.X + offSetAdjustment, proposedContentOffset.Y);
		}

	}
}
