﻿using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Color types.
	 */
	[pb_TypeInspector(typeof(Color))]
#pragma warning disable IDE1006
    public class pb_ColorInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Color value;

		public UnityEngine.UI.Text title;

		public UnityEngine.UI.InputField
			input_r,
			input_g,
			input_b,
			input_a;

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();

			input_r.onValueChanged.AddListener( OnValueChange_R );
			input_g.onValueChanged.AddListener( OnValueChange_G );
			input_b.onValueChanged.AddListener( OnValueChange_B );
			input_a.onValueChanged.AddListener( OnValueChange_A );
		}

		protected override void OnUpdateGUI()
		{
			value = GetValue<Color>();

			input_r.text = value.r.ToString("g");
			input_g.text = value.g.ToString("g");
			input_b.text = value.b.ToString("g");
			input_a.text = value.a.ToString("g");
		}

		public void OnValueChange_R(string val)
		{
            if (float.TryParse(val, out float v))
            {
                value.r = Mathf.Clamp(v, 0f, 1f);
                if (v < 0f || v > 1f)
                    input_r.text = value.r.ToString("g");
                OnGUIChanged();
            }
        }

		public void OnValueChange_G(string val)
		{
            if (float.TryParse(val, out float v))
            {
                value.g = Mathf.Clamp(v, 0f, 1f);
                if (v < 0f || v > 1f)
                    input_g.text = value.g.ToString("g");
                OnGUIChanged();
            }
        }

		public void OnValueChange_B(string val)
		{

            if (float.TryParse(val, out float v))
            {
                value.b = Mathf.Clamp(v, 0f, 1f);
                if (v < 0f || v > 1f)
                    input_b.text = value.b.ToString("g");
                OnGUIChanged();
            }
        }

		public void OnValueChange_A(string val)
		{
            if (float.TryParse(val, out float v))
            {
                value.a = Mathf.Clamp(v, 0f, 1f);
                if (v < 0f || v > 1f)
                    input_a.text = value.a.ToString("g");
                OnGUIChanged();
            }
        }
	}
}
