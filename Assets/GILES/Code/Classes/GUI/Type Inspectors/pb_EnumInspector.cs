﻿using UnityEngine.UI;

namespace GILES.Interface
{
	/**
	 * Field editor for enum types.
	 */
	[pb_TypeInspector(typeof(System.Enum))]
#pragma warning disable IDE1006
    public class pb_EnumInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        object value;

		public Text title;
		public Button button;
		public Text currentEnumValue;
		string[] enumNames;		/// the available names of this enum
		System.Array enumValues;	/// the available values of this enum

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();
			button.onClick.AddListener( OnClick );

			enumNames = System.Enum.GetNames(DeclaringType);
			enumValues = System.Enum.GetValues(DeclaringType);
		}

		void OnClick()
		{
			// cycle enum value
			if(enumValues != null)
			{
				int len = enumValues.Length;

				// values may not be linear, or integers at all
				int index = System.Array.IndexOf(enumValues, value);

				index++;

				if(index >= len)
					index = 0;

				value = enumValues.GetValue(index);

				RefreshText();
				
				OnGUIChanged();
			}
		}

		protected override void OnUpdateGUI()
		{
			value = GetValue<object>();

			RefreshText();
		}

		void RefreshText()
		{
			if(enumNames != null)
				currentEnumValue.text = value.ToString();// enumNames[value];
			else
				currentEnumValue.text = "Enum: " + value.ToString();
		}
	}
}