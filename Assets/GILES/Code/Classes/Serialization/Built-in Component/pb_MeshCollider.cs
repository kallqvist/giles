﻿using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GILES.Serialization
{
    /**
	 * Specialized component wrapper for serializing and deserializing MeshCollider components.
	 */
#pragma warning disable IDE1006
    public class pb_MeshCollider : pb_SerializableObject<UnityEngine.MeshCollider>
	{
#pragma warning restore IDE1006

        public pb_MeshCollider(UnityEngine.MeshCollider obj) : base(obj) {}
		public pb_MeshCollider(SerializationInfo info, StreamingContext context) : base(info, context) {}

		public override Dictionary<string, object> PopulateSerializableDictionary()
		{
			Dictionary<string, object> props = pb_Reflection.ReflectProperties(target);

            /// If sharedMesh points to the meshfilter on the same gameobject, skip serializing
            /// and on reconstruct point to the instance.
            if (props.TryGetValue("sharedMesh", out object m))
            {
                MeshFilter mf = target.gameObject.GetComponent<MeshFilter>();

                if (mf != null && mf.sharedMesh.GetInstanceID() == m.GetHashCode())
                    props.Remove("sharedMesh");
            }

            return props;
		}
	}
}