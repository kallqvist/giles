using UnityEngine;

namespace GILES
{
    /**
	 * A series of helper methods for working with collections.
	 */
#pragma warning disable IDE1006
    public static class pb_CollectionUtil
	{
#pragma warning restore IDE1006

        /**
		 * Return an array filled with `value` and of length.
		 */
        public static T[] Fill<T>(T value, int length)
		{
			T[] arr = new T[length];
			for(int i = 0; i < length; i++)
				arr[i] = value;
			return arr;
		}
		
	}
}