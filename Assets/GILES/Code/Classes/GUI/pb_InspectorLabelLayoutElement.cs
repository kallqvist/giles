using UnityEngine.UI;

namespace GILES.Interface
{
    /**
	 * Component attached to pb_TypeInspectors to guarantee a uniform label width
	 * for int, enum, toggle, float, and string fields.
	 */
#pragma warning disable IDE1006
    public class pb_InspectorLabelLayoutElement : LayoutElement
	{
#pragma warning restore IDE1006

        const int LABEL_WIDTH = 64;

		public override float minWidth
		{
			get { return LABEL_WIDTH; }
		}

		public override float preferredWidth
		{
			get { return LABEL_WIDTH; }
		}
	}
}