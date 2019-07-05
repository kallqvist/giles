using UnityEditor;
using GILES.Interface;

namespace GILES.UnityEditor
{
	/**
	 * Custom editor implementation for pb_InspectorLabelLayoutElement.
	 * 
	 * \notes Despite a valiant effort, AVFoundation remains the undisputed king of super-long class names.
	 */
	[CustomEditor(typeof(pb_InspectorLabelLayoutElement))]
#pragma warning disable IDE1006
    public class pb_InspectorLabelLayoutElementEditor : Editor
	{
#pragma warning restore IDE1006
        public override void OnInspectorGUI()
		{
		}
	}
}