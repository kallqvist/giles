﻿using UnityEngine;
using GILES.Serialization;

namespace GILES.Interface
{
#pragma warning disable IDE1006

    public class pb_MeshRendererEditor : pb_ComponentEditor
	{
#pragma warning restore IDE1006

        private MeshRenderer _meshRenderer;

		protected override void InitializeGUI()
		{
			_meshRenderer = (MeshRenderer) target;
			
			pb_GUIUtility.AddVerticalLayoutGroup(gameObject);

			pb_TypeInspector enabled_inspector = pb_InspectorResolver.GetInspector(typeof(bool));

			enabled_inspector.Initialize("Enabled", UpdateEnabled, OnSetEnabled);
			enabled_inspector.onValueBeginChange = () => { Undo.RegisterState( new UndoReflection(_meshRenderer, "enabled"), "MeshRenderer Enabled" ); };
			enabled_inspector.transform.SetParent(transform);

		}

		object UpdateEnabled()
		{
			return _meshRenderer.enabled;
		}

		void OnSetEnabled(object value)
		{
			_meshRenderer.enabled = (bool) value;
			pb_ComponentDiff.AddDiff(target, "enabled", _meshRenderer.enabled);
		}
	}
}