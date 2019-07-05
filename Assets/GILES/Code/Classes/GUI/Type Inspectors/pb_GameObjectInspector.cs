using UnityEngine;

namespace GILES.Interface
{
	/**
	 * Field editor for GameObject types.
	 */
	[pb_TypeInspector(typeof(GameObject))]
#pragma warning disable IDE1006
    public class pb_GameObjectInspector : pb_TypeInspector
	{
#pragma warning restore IDE1006

        GameObject value;

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			value = GetValue<GameObject>();
		}

		protected override void OnUpdateGUI()
		{
		}

		public void OnValueChange(object val)
		{
		}
	}
}