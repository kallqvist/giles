using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GILES.Serialization
{
    /**
	 * Specialized component wrapper for serializing and deserializing Camera components.
	 */
#pragma warning disable IDE1006
    public class pb_CameraComponent : pb_SerializableObject<UnityEngine.Camera>
	{
#pragma warning restore IDE1006

        public pb_CameraComponent(UnityEngine.Camera obj) : base(obj) {}
		public pb_CameraComponent(SerializationInfo info, StreamingContext context) : base(info, context) {}

		public override Dictionary<string, object> PopulateSerializableDictionary()
		{
			Dictionary<string, object> props = pb_Reflection.ReflectProperties(target);

			props.Remove("worldToCameraMatrix");
			props.Remove("projectionMatrix");

			return props;
		}
	}
}