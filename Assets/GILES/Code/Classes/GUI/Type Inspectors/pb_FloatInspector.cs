namespace GILES.Interface
{
	/**
	 * Field editor for float types.
	 */
	[pb_TypeInspector(typeof(float))]
#pragma warning disable IDE1006
    public class pb_FloatInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        float value;

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
			value = GetValue<float>();
			input.text = value.ToString();
		}

		public void OnValueChange(string val)
		{
            if (float.TryParse(val, out float v))
            {
                value = v;
                OnGUIChanged();
            }
        }
	}
}
