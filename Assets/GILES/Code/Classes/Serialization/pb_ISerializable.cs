using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GILES.Serialization
{
    /**
	 * Interface for objects that want to customize serialization process at the 
	 * pre-Json.NET level.  Converters for Components fall into this category - 
	 * see pb_SerializableObject class for more on customizing the serialization
	 * pipeline.
	 */
#pragma warning disable IDE1006
    public interface pb_ISerializable : ISerializable
	{
#pragma warning restore IDE1006

        /// The type of component stored.
        System.Type Type { get; set; }

		/// Called after an object is deserialized and constructed to it's base `type`.
		void ApplyProperties(object obj);

		/// Called before serialization, any properties stoed in the returned dictionary
		/// will be saved and re-applied in ApplyProperties.
		Dictionary<string, object> PopulateSerializableDictionary();
	}	
}