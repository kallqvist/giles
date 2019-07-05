using UnityEngine;

namespace GILES
{
    /**
	 * A generic singleton implementation for ScriptableObject classes.
	 */
#pragma warning disable IDE1006
    public class pb_ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
	{
#pragma warning restore IDE1006

        private static ScriptableObject _instance;

		public static T Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = (ScriptableObject) ScriptableObject.CreateInstance<T>();
				}

				return (T) _instance;
			}
		}

		/**
		 * Return the instance if it has been initialized, null otherwise.
		 */
		public static T NullableInstance
		{
			get { return (T) _instance; }
		}

		/**
		 * Set _instance if required.  If overriding in a child class be sure to call base.OnEnable()
		 * before anything elese is executed.
		 */
		protected virtual void OnEnable()
		{
			if(_instance == null)
				_instance = this;
			else
				pb_ObjectUtility.Destroy(this);
		}
	}
}