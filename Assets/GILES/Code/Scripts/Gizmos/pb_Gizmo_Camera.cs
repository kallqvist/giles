using UnityEngine;

namespace GILES
{
	/**
	 * Draw a few arrows pointing in the direction that this light is facing.
	 */
	[pb_Gizmo(typeof(Camera))]
#pragma warning disable IDE1006
    public class pb_Gizmo_Camera : pb_Gizmo
	{
#pragma warning restore IDE1006

        void Start()
		{
			icon = pb_BuiltinResource.GetMaterial(pb_BuiltinResource.mat_CameraGizmo);
		}
	}
}