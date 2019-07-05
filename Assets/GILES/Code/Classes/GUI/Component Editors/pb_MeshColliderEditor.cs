using UnityEngine;

namespace GILES.Interface
{
#pragma warning disable IDE1006

    public class pb_meshColliderEditor : pb_ComponentEditor
	{
#pragma warning restore IDE1006

        private MeshCollider _meshCollider;

		protected override void InitializeGUI()
		{
			_meshCollider = (MeshCollider) target;

			pb_GUIUtility.AddVerticalLayoutGroup(gameObject);

			pb_TypeInspector enabled_inspector = pb_InspectorResolver.GetInspector(typeof(bool));

			enabled_inspector.Initialize("Enabled", UpdateEnabled, OnSetEnabled);
			enabled_inspector.onValueBeginChange = () => { Undo.RegisterState( new UndoReflection(_meshCollider, "enabled"), "Mesh Collider Enabled" ); };
			enabled_inspector.transform.SetParent(transform);
		}

		object UpdateEnabled()
		{
			return _meshCollider.enabled;
		}

		void OnSetEnabled(object value)
		{
			_meshCollider.enabled = (bool) value;
		}
	}
}