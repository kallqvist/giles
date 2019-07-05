namespace GILES.Interface
{
	/**
	 * Field editor for string types.
	 */
	[pb_TypeInspector(typeof(string))]
#pragma warning disable IDE1006
    public class pb_StringInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        string value;

		public UnityEngine.UI.Text title;
		public UnityEngine.UI.InputField input;

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();

			input.onValueChanged.AddListener( OnValueChange );
		}

		protected override void OnUpdateGUI()
		{
			value = GetValue<string>();
			input.text = value != null ? value.ToString() : "null";
		}

		public void OnValueChange(string val)
		{
			SetValue(val);
		}
	}
}
