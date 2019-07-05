using UnityEngine.UI;

namespace GILES.Interface
{
    /**
	 * A LayoutElement that calculates width based on a text component's width.
	 */
#pragma warning disable IDE1006
    public class pb_LayoutElementText : LayoutElement
	{
#pragma warning restore IDE1006
        public bool expandWidth = true, expandHeight = false;
		public Text text;
		public float paddingWidth = 4f, paddingHeight = 4f;

		public override float minWidth
		{
			get { return GetTextWidth(); }
		}

		public override float preferredWidth
		{
			get { return GetTextWidth(); }
		}

		public override float minHeight
		{
			get { return GetTextHeight(); }
		}

		public override float preferredHeight
		{
			get { return GetTextHeight(); }
		}

		float GetTextWidth()
		{
			if(text != null && expandWidth)
				return text.preferredWidth + (paddingWidth * 2f);
			else
				return -1f;
		}

		float GetTextHeight()
		{
			if(text != null && expandHeight)
				return text.preferredHeight + (paddingHeight * 2f);
			else
				return -1f;
		}
	}
}