using UnityEngine;
using UnityEditor;
using System.Linq;

namespace GILES.Interface
{
	[CanEditMultipleObjects, CustomEditor(typeof(pb_GUIStyleApplier))]
#pragma warning disable IDE1006
    public class pb_GUIStyleApplierEditor : Editor
	{
#pragma warning restore IDE1006
        SerializedProperty style;
		SerializedProperty ignore;

		[SerializeField] bool show = true;

		void OnEnable()
		{
			style = serializedObject.FindProperty("style");
			ignore = serializedObject.FindProperty("ignoreStyle");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUI.BeginChangeCheck();

			if(!ignore.boolValue)
				EditorGUILayout.PropertyField(style);

			EditorGUILayout.PropertyField(ignore);

			if(!ignore.boolValue && style.objectReferenceValue != null)
			{
				show = EditorGUILayout.Foldout(show, style.objectReferenceValue.name);

				if(show)
				{

					Editor editor = Editor.CreateEditor(style.objectReferenceValue);

					if(editor != null)
						editor.OnInspectorGUI();
				}
			}


			if(EditorGUI.EndChangeCheck() || GUILayout.Button("Apply"))
			{
				// foreach(pb_GUIStyleApplier applier in serializedObject.targetObjects)
				// 	applier.ApplyStyle();

				foreach(pb_GUIStyleApplier applier in Resources.FindObjectsOfTypeAll<pb_GUIStyleApplier>().Where(x => x.style == style.objectReferenceValue))
					applier.ApplyStyle();

				Canvas.ForceUpdateCanvases();
				SceneView.RepaintAll();
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}