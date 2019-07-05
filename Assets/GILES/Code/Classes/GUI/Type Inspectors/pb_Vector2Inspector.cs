using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Vector2 types.
	 */
	[pb_TypeInspector(typeof(Vector2))]
#pragma warning disable IDE1006
    public class pb_Vector2Inspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Vector2 vector;

		public UnityEngine.UI.Text title;

		public UnityEngine.UI.InputField input_x, input_y;

		void OnGUIChanged()
		{
			SetValue(vector);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();

			input_x.onValueChanged.AddListener( OnValueChange_X );
			input_y.onValueChanged.AddListener( OnValueChange_Y );
		}

		protected override void OnUpdateGUI()
		{
			vector = GetValue<Vector2>();
			input_x.text = vector.x.ToString();
			input_y.text = vector.y.ToString();
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
	}
}
