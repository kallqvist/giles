using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Vector3 types.
	 */
	[pb_TypeInspector(typeof(Vector3))]
#pragma warning disable IDE1006
    public class pb_Vector3Inspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Vector3 vector;

		public UnityEngine.UI.Text title;

		public UnityEngine.UI.InputField
			input_x,
			input_y,
			input_z;

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
		}

		protected override void OnUpdateGUI()
		{
			vector = GetValue<Vector3>();

			input_x.text = vector.x.ToString();
			input_y.text = vector.y.ToString();
			input_z.text = vector.z.ToString();
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
	}
}
