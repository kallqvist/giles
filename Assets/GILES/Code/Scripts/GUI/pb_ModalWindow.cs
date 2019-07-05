using UnityEngine;
using UnityEngine.UI;

namespace GILES.Interface
{
    /**
	 * A singleton window instance that takes control of the screen when initialized.
	 */
#pragma warning disable IDE1006
    public class pb_ModalWindow : pb_MonoBehaviourSingleton<pb_ModalWindow>
	{
#pragma warning restore IDE1006
        /**
		 *	Place content GUI items as content's children.
		 */
        public GameObject contents;

		/**
		 *	The title shown in the header bar.
		 */
		public Text windowTitle;

		/**
		 *	Set the title text shown for this window.
		 */
		public static void SetTitle(string title)
		{
			Instance.windowTitle.text = title;
		}

		public static void Show()
		{
			foreach(Transform t in Instance.transform)
			{
				t.gameObject.SetActive(true);
			}

			Instance.transform.SetAsLastSibling();
		}

		public static bool IsVisible()
		{
			foreach(Transform t in Instance.transform)
				if(t.gameObject.activeSelf)
					return true;
			return false;
		}

		public static void Hide()
		{
			foreach(Transform t in Instance.transform)
				t.gameObject.SetActive(false);
		}

		/**
		 * Add an already instantiated prefab to the contents of this window.
		 */
		public static void SetContent(GameObject prefab)
		{
			foreach(Transform t in Instance.contents.transform)
				pb_ObjectUtility.Destroy(t.gameObject);

			prefab.transform.SetParent(Instance.contents.transform, false);
		}
	}
}
