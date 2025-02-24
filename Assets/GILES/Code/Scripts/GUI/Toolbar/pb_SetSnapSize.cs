﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GILES
{
#pragma warning disable IDE1006
    public class pb_SetSnapSize : pb_ToolbarButton
	{
#pragma warning restore IDE1006
        public override string Tooltip { get { return "Set the snap increment"; } }

		Dropdown dropdown;

		protected override void Start()
		{
			dropdown = GetComponent<Dropdown>();
			dropdown.value = (int) Mathf.Ceil(1f / pb_SelectionHandle.positionSnapValue) - 1;
		}

		private bool hideTooltips = false;

		public override void OnPointerClick(PointerEventData data)
		{
			hideTooltips = true;
			HideTooltip();
		}

		public override void OnPointerEnter(PointerEventData data)
		{
			if(!hideTooltips)
				base.OnPointerEnter(data);
		}

		public override void OnPointerExit(PointerEventData data)
		{
			base.OnPointerExit(data);
		}

		public void SetSnapIncrement()
		{
			hideTooltips = false;
			
			if(dropdown.value == 0)
				pb_SelectionHandle.positionSnapValue = 1f;
			else
				pb_SelectionHandle.positionSnapValue = 1f / Mathf.Pow(2, dropdown.value);
		}
	}
}