using UnityEngine;

namespace GILES
{
    /**
	 * Static helper methods for rounding values.
	 */
#pragma warning disable IDE1006
    public static class pb_Snap
	{
#pragma warning restore IDE1006

        /**
		* Snap a value to the nearest increment.
		*/
        public static float Snap(float value, float increment)
		{
			return Mathf.Round(value / increment) * increment;
		}

		public static Vector2 Snap(Vector2 value, float increment)
		{
			return new Vector2(
				Snap(value.x, increment),  
				Snap(value.y, increment) );
		}

		public static Vector3 Snap(Vector3 value, float increment)
		{
			return new Vector3(
				Snap(value.x, increment), 
				Snap(value.y, increment), 
				Snap(value.z, increment) );
		}
	}
}