namespace GILES.Interface
{
	/**
	 * Field editor for integer types.
	 */
	[pb_TypeInspector(typeof(int))]
#pragma warning disable IDE1006
    public class pb_IntInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

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
            if (int.TryParse(val, out int v))
            {
                value = v;
                OnGUIChanged();
            }
        }
	}
}
