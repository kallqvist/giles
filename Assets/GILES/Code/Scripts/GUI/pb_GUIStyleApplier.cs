using UnityEngine;
using UnityEngine.UI;

namespace GILES.Interface
{
    /**
	 * Given a pb_GUIStyle, this applies it to the current object.
	 */
#pragma warning disable IDE1006
    public class pb_GUIStyleApplier : MonoBehaviour
	{
#pragma warning restore IDE1006
        public bool ignoreStyle;
		public pb_GUIStyle style;

		void Awake()
		{
			if(!ignoreStyle)
				ApplyStyle();
		}

		public void ApplyStyle()
		{
			if(style == null)
				return;

			ApplyRecursive(gameObject);
		}

		private void ApplyRecursive(GameObject go)
		{
			foreach(Graphic graphic in go.GetComponents<Graphic>())
				style.Apply(graphic);

			foreach(Selectable selectable in go.GetComponents<Selectable>())
				style.Apply(selectable);

			foreach(Transform t in go.transform)
			{
				if(t.gameObject.GetComponent<pb_GUIStyleApplier>() != null)
					continue;

				ApplyRecursive(t.gameObject);
			}
		}
	}
}
