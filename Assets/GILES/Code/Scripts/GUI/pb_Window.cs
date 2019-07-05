using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace GILES.Interface
{
#pragma warning disable IDE1006
    public class pb_Window : UIBehaviour, IPointerDownHandler
	{
#pragma warning restore IDE1006
        public virtual void OnPointerDown(PointerEventData eventData)
		{
			EventSystem.current.SetSelectedGameObject(gameObject, eventData);
			transform.SetAsLastSibling();
		}
	}
}