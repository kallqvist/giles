using UnityEngine;

namespace GILES
{

    /**
	 * Helper methods for Input
	 */
#pragma warning disable IDE1006
    public static class pb_InputExtension
	{
#pragma warning restore IDE1006

        public static bool Shift()
		{
			return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
		}

		public static bool Control()
		{
			return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
		}
	}
}