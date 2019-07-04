﻿using UnityEngine;
using System.Collections;
using System.Reflection;

namespace GILES.Interface
{
	/**
	 * Field editor for integer types.
	 */
	[pb_TypeInspector(typeof(int))]
	public class pb_IntInspector : pb_TypeInspector
	{
		int value;

		public UnityEngine.UI.Text title;
		public UnityEngine.UI.InputField input;

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();

			input.onValueChanged.AddListener( OnValueChange );
		}

		protected override void OnUpdateGUI()
		{
			value = GetValue<int>();
			input.text = value.ToString();
		}

		public void OnValueChange(string val)
		{
			int v;

			if(int.TryParse(val, out v))
			{
				value = v;
				OnGUIChanged();
			}
		}
	}
}
