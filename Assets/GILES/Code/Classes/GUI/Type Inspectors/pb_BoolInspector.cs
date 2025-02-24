﻿namespace GILES.Interface
{
	/**
	 * Field editor for boolean types.
	 */
	[pb_TypeInspector(typeof(bool))]
#pragma warning disable IDE1006
    public class pb_BoolInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        bool value;

		public UnityEngine.UI.Text title;
		public UnityEngine.UI.Toggle input;

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();
			input.onValueChanged.AddListener( OnValueChange );
		}
			
		protected override void OnUpdateGUI()
		{
			value = GetValue<bool>();
			input.isOn = value;
		}

		public void OnValueChange(bool val)
		{
			value = val;
			SetValue(value);
		}
	}
}