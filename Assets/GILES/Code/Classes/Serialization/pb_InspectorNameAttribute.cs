using System;

namespace GILES.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
#pragma warning disable IDE1006
    public class pb_InspectorNameAttribute : Attribute
    {
#pragma warning restore IDE1006

        public readonly string name;

        public pb_InspectorNameAttribute(string name)
        {
            this.name = name;
        }
    }
}