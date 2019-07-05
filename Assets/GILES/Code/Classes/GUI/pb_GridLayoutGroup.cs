using UnityEngine;
using UnityEngine.UI;

namespace GILES.Interface
{
    /**
	 * Resize grid element width and height to best fit the available space.
	 */
#pragma warning disable IDE1006    
    public class pb_GridLayoutGroup : GridLayoutGroup, pb_IOnResizeHandler
	{
#pragma warning restore IDE1006

        public Vector2 elementSize = new Vector2(100f, 100f);

		public bool maintainAspectRatio = true;

		protected override void Start()
		{
			base.Start();

			OnResize();
		}

		public void OnResize()
		{
			float width = (rectTransform.rect.width - spacing.x);
			float grid = elementSize.x + spacing.x;

			if(width <= grid)
				return;

			Vector2 cell = Vector2.zero;

			cell.x = elementSize.x + (width % grid) / (float)(((int)width) / ((int)grid));

			if(maintainAspectRatio)
				cell.y = elementSize.y * (cell.x / elementSize.x);

			cellSize = cell;
		}
	}
}
