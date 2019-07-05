using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using GILES.Serialization;

namespace GILES.UnityEditor
{
	/**
	 *	Editor for pb_MetaDataComponent.
	 */
	[CustomEditor(typeof(pb_MetaDataComponent))]
#pragma warning disable IDE1006
    public class pb_MetaDataEditor : Editor
	{
#pragma warning restore IDE1006
        // Store reference to the component target.
        pb_MetaDataComponent data;

		// The component diff as stored by pb_MetaData.
		pb_ComponentDiff diff;

		// Remember which component diffs have been expanded.
		Dictionary<Component, bool> dropdowns = new Dictionary<Component, bool>();

		void OnEnable()
		{
			data = (pb_MetaDataComponent) target;
			diff = data.metadata.componentDiff;
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			// Non-user-editable data.
			GUILayout.Label("Asset Type: " + data.metadata.AssetType);
			GUILayout.Label("File ID: " + data.metadata.FileId);
			GUILayout.Label("Asset Path: " + data.metadata.AssetBundlePath);

			// Show the stored component diffs.
			GUILayout.Label("Modified Values", EditorStyles.boldLabel);

			int labelWidth = (int) Mathf.Min(Screen.width/2, 100);

			foreach(KeyValuePair<Component, Dictionary<string, object>> kvp in diff.modifiedValues)
			{
				if(!dropdowns.ContainsKey(kvp.Key))
					dropdowns.Add(kvp.Key, false);

				dropdowns[kvp.Key] = EditorGUILayout.Foldout(dropdowns[kvp.Key], kvp.Key.ToString());

				if(dropdowns[kvp.Key])
				{
					foreach(KeyValuePair<string, object> changes in kvp.Value)
					{
						GUILayout.BeginHorizontal();

						GUILayout.Label(changes.Key, GUILayout.MinWidth(labelWidth), GUILayout.MaxWidth(labelWidth));

						GUILayout.Label(changes.Value.ToString().Truncate(128));

						GUILayout.EndHorizontal();
					}
				}
			}
		}

	}
}
