using System;

namespace GILES.Interface
{
	/**
	 * pb_TypeInspector attribute declares the type that a class inheriting pb_TypeInspector edits.
	 * \sa pb_TypeInspector pb_InspectorResolver pb_Inspector
	 */
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
#pragma warning disable IDE1006
    public class pb_TypeInspectorAttribute : Attribute
	{
#pragma warning restore IDE1006

        /// The type for which this class is providing an editor.
        public Type type;

		/**
		 * Attribute constructor accepts a type argument for the inspected type.
		 */
		public pb_TypeInspectorAttribute(System.Type type)
		{
			this.type = type;
		}

		/**
		 * Return true if the class marked with this attribute can inspect the type.
		 */
		public virtual bool CanEditType(Type rhs)
		{
			return type.IsAssignableFrom(rhs);
		}
	}
}