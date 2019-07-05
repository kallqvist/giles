using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GILES.Serialization
{
    /**
	 * Specialized component wrapper for serializing and deserializing MeshFilter components.
	 */
#pragma warning disable IDE1006
    public class pb_MeshFilter : pb_SerializableObject<UnityEngine.MeshFilter>
	{
#pragma warning restore IDE1006

        public pb_MeshFilter(UnityEngine.MeshFilter obj) : base(obj) {}
		public pb_MeshFilter(SerializationInfo info, StreamingContext context) : base(info, context) {}

		public override Dictionary<string, object> PopulateSerializableDictionary()
		{
            Dictionary<string, object> props = new Dictionary<string, object>
            {
                { "sharedMesh", target.sharedMesh },
                { "tag", target.tag },
                { "name", target.name },
                { "hideFlags", target.hideFlags }
            }; // pb_Reflection.ReflectProperties(target);

            return props;
		}
	}
}