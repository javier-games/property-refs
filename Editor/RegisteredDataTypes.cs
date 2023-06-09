using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monogum.BricksBucket.PropertyRefs.Editor
{
	[Serializable]
	internal class Registry
	{
		[SerializeField]
		public List<RegisteredComponent> components;
	}
	
	[Serializable]
	internal class RegisteredComponent
	{
		[SerializeField]
		public string type;

		[SerializeField]
		public List<RegisteredProperty> properties;
	}

	[Serializable]
	internal class RegisteredProperty
	{
		[SerializeField]
		public string name;

		[SerializeField]
		public string type;
	}
}