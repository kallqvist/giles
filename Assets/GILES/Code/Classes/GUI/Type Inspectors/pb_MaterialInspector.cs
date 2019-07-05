using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for Material types.
	 */
	[pb_TypeInspector(typeof(Material))]
#pragma warning disable IDE1006
    public class pb_MaterialInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        Material value;

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
			value = GetValue<Material>();
			dropbox.text = (value == null ? "null" : value.ToString());
		}

		public void OnValueChange(Material val)
		{
			value = val;
			OnGUIChanged();
		}
	}
}