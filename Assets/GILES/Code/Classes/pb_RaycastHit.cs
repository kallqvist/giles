using UnityEngine;

namespace GILES
{
    /**
	 * Describes the intersection point of a ray and mesh.
	 */
#pragma warning disable IDE1006
    public class pb_RaycastHit
	{
#pragma warning restore IDE1006

        public Vector3 point;
		public float distance;
		public Vector3 normal;
		public int[] triangle;
	}
}