using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace GILES.Interface
{
	/**
	 * Field editor for object types.  Attempts to break object into primitives.
	 * This should not have a prefab in the Resources/Required/GUI/TypeInspector folder!
	 */
	[pb_TypeInspector(typeof(object))]
#pragma warning disable IDE1006
    public class pb_ObjectInspector : pb_TypeInspector
	{
#pragma warning disable IDE1006

        object value;
		
		//private static readonly RectOffset RectOffset_Zero = new RectOffset(0,0,0,0);
		private const int VERTICAL_LAYOUT_SPACING = 0;

		void OnGUIChanged()
		{
			SetValue(value);
		}

		public override void InitializeGUI()
		{
			value = GetValue<object>();

			pb_GUIUtility.AddVerticalLayoutGroup(gameObject, new RectOffset(0,0,0,0), VERTICAL_LAYOUT_SPACING, true, false);

			BuildInspectorTree();
		}

		protected override void OnUpdateGUI()
		{
		}

		public void OnValueChange(object val)
		{
		}

		void BuildInspectorTree()
		{
			if(DeclaringType == null)
			{
				Debug.LogWarning("Inspector is targeting a null or primitive type with no available pb_TypeInspector override, or target is null and using delegates in the parent inspector.");
				return;
			}
			
			string name = GetName();
			name = name.Substring(name.LastIndexOf(".") + 1);

			GameObject subpanel = pb_GUIUtility.CreateLabeledVerticalPanel(name);
			subpanel.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(2,2,2,2);
			subpanel.transform.SetParent(transform);		

			foreach(PropertyInfo prop in pb_Reflection.GetSerializableProperties(DeclaringType, BindingFlags.Public | BindingFlags.Instance))
			{
				pb_InspectorResolver.AddTypeInspector(value, subpanel.transform, prop, null).parent = this;
			}

			foreach(FieldInfo field in pb_Reflection.GetSerializableFields(DeclaringType, BindingFlags.Public | BindingFlags.Instance))
			{
				pb_InspectorResolver.AddTypeInspector(value, subpanel.transform, null, field).parent = this;
			}
		}
	}
}
