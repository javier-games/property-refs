// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
using System.Runtime.Serialization;

namespace Monogum.BricksBucket.PropertyRefs.SourceGenerator
{
	[DataContract]
	internal class RegistryData
	{
		[DataMember]
		public RegisteredComponent[] components;
	}

	[DataContract]
	internal class RegisteredComponent
	{
		[DataMember]
		public string type;
		[DataMember]
		public RegisteredProperty[] properties;
	}

	[DataContract]
	internal class RegisteredProperty
	{
		[DataMember]
		public string name;
		[DataMember]
		public string type;
	}
}