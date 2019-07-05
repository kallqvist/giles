using UnityEngine;
using UnityEngine.EventSystems;

namespace GILES.Interface
{
#pragma warning disable IDE1006
    public class pb_DraggablePanel : MonoBehaviour, IBeginDragHandler, IDragHandler
	{
#pragma warning restore IDE1006
        Rect screenRect = new Rect(0,0,0,0);

		/// The root gameobject of this window.
		public RectTransform windowParent;

		public void OnBeginDrag(PointerEventData eventData)
		{
			screenRect.width = Screen.width;
			screenRect.height = Screen.height;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if(windowParent == null)
			{
				Debug.LogWarning("Window parent is null, cannot drag a null window.");
				return;
			}

			if(screenRect.Contains(eventData.position))
				windowParent.position += (Vector3) eventData.delta;
		}
	}
}
