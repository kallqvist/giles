using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for UnityEngine.Object types.
	 */
	[pb_TypeInspector(typeof(Object))]
#pragma warning disable IDE1006
    public class pb_UnityObjectInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        UnityEngine.Object value;

		public UnityEngine.UI.Text title;
		public UnityEngine.UI.InputField dropbox;

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			title.text = GetName().SplitCamelCase();
		}

		protected override void OnUpdateGUI()
		{
			value = GetValue<UnityEngine.Object>();
			dropbox.text = (value == null ? "null" : value.ToString());
		}

		public void OnValueChange(UnityEngine.Object val)
		{
			value = val;
			OnGUIChanged();
		}
	}
}