using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Vector4 types.
	 */
	[pb_TypeInspector(typeof(Vector4))]
#pragma warning disable IDE1006
    public class pb_Vector4Inspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Vector4 vector;

		public UnityEngine.UI.Text title;

		public UnityEngine.UI.InputField
			input_x,
			input_y,
			input_z,
			input_w;

		void OnGUIChanged()
		{
			SetValue(vector);
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
			vector = GetValue<Vector4>();
			input_x.text = vector.x.ToString();
			input_y.text = vector.y.ToString();
			input_z.text = vector.z.ToString();
			input_w.text = vector.w.ToString();
		}

		public void OnValueChange_X(string val)
		{
            if (float.TryParse(val, out float v))
            {
                vector.x = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_Y(string val)
		{
            if (float.TryParse(val, out float v))
            {
                vector.y = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_Z(string val)
		{
            if (float.TryParse(val, out float v))
            {
                vector.z = v;
                OnGUIChanged();
            }
        }

		public void OnValueChange_W(string val)
		{
            if (float.TryParse(val, out float v))
            {
                vector.w = v;
                OnGUIChanged();
            }
        }
	}
}
