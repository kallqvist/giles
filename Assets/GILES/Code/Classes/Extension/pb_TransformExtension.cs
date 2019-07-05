using UnityEngine;

namespace GILES
{
    /**
	 * Utility methods for working with UnityEngine.Transform and pb_Transform types.
	 */
#pragma warning disable IDE1006
    public static class pb_TransformExtension
	{
#pragma warning restore IDE1006

        /**
		 * Set a UnityEngine.Transform with a Runtime.pb_Transform.
		 */
        public static void SetTRS(this Transform transform, pb_Transform pbTransform)
		{
			transform.position = pbTransform.Position;
			transform.localRotation = pbTransform.Rotation;
			transform.localScale = pbTransform.Scale;
		}
	}
}