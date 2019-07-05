using UnityEngine;
using Newtonsoft.Json;

namespace GILES.Serialization
{
#pragma warning disable IDE1006
    public static class pb_Serialization
	{
#pragma warning restore IDE1006

        public static readonly JsonSerializerSettings ConverterSettings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new pb_ContractResolver(),
			TypeNameHandling = TypeNameHandling.Objects
		};

		public static readonly JsonSerializer Serializer = JsonSerializer.Create(ConverterSettings);

		/**
		 * Checks for a custom written built-in component converter first, then if none
		 * found returns the default converter.
		 */
		public static pb_ISerializable CreateSerializableObject<T>(T obj)
		{
			if(obj is UnityEngine.Camera)
				return (pb_ISerializable) new pb_CameraComponent( obj as Camera );

			if(obj is UnityEngine.MeshFilter)
				return (pb_ISerializable) new pb_MeshFilter( obj as MeshFilter );

			if(obj is UnityEngine.MeshCollider)
				return (pb_ISerializable) new pb_MeshCollider( obj as MeshCollider );

			if(obj is UnityEngine.MeshRenderer)
				return (pb_ISerializable) new pb_MeshRenderer( obj as MeshRenderer );

			return new pb_SerializableObject<T>(obj);
		}
	}
}