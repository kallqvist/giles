using System;

namespace GILES.Serialization
{
	/**
	 * Classes marked with this attribute will be ignored when serializing a level.
	 */
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
#pragma warning disable IDE1006
    public class pb_JsonIgnoreAttribute : Attribute {}
#pragma warning restore IDE1006

}