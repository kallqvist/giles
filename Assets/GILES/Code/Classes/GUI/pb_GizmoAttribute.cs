using UnityEngine;
using System;

namespace GILES
{
	/**
	 * pb_GizmoAttribute declares the type that a class inheriting pb_Gizmo draws over.
	 * \sa pb_Gizmo pb_GizmoManager
	 */
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
#pragma warning disable IDE1006
    public class pb_GizmoAttribute : Attribute
	{
#pragma warning restore IDE1006

        /// The type for which this class is providing an editor.
        public Type type;

		/**
		 * Attribute constructor accepts a type argument for the inspected type.
		 */
		public pb_GizmoAttribute(System.Type type)
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