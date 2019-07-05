using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Quaternion types.
	 */
	[pb_TypeInspector(typeof(Quaternion))]
#pragma warning disable IDE1006
    public class pb_QuaternionInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Quaternion quaternion;

		public UnityEngine.UI.Text title;

		public UnityEngine.UI.InputField
			input_x,
			input_y,
			input_z,
			input_w;

		void OnGUIChanged()
		{
			SetValue(quaternion);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();

			input_x.onValueChanged.AddListener( OnValueChange_X );
			input_y.onValueChanged.AddListener( OnValueChange_Y );
			input_z.onValueChanged.AddListener( OnValueChange_Z );
			input_w.onValueChanged.AddListener( OnValueChange_W );
		}

		protected override void OnUpdateGUI()
		{
			quaternion = GetValue<Quaternion>();
			input_x.text = quaternion.x.ToString();
			input_y.text = quaternion.y.ToString();
			input_z.text = quaternion.z.ToString();
			input_w.text = quaternion.w.ToString();
		}

		public void OnValueChange_X(string val)
		{
            if (float.TryParse(val, out float v))
            {
                quaternion.x = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_Y(string val)
		{
            if (float.TryParse(val, out float v))
            {
                quaternion.y = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_Z(string val)
		{
            if (float.TryParse(val, out float v))
            {
                quaternion.z = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_W(string val)
		{
            if (float.TryParse(val, out float v))
            {
                quaternion.w = v;
                OnGUIChanged();
            }
        }
	}
}
