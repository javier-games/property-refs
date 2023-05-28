using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if !UNITY_IOS || UNITY_EDITOR
using System.Reflection;
using System.Linq;
#endif

// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable PartialTypeWithSinglePart
namespace Monogum.BricksBucket.PropertyRefs
{
	/// <summary>
	/// Registry of the properties to use by PropertyRefs.
	/// </summary>
	public static partial class PropertiesRegistry
	{
		/// <summary>
		/// Whether the registry is using code generation. If the registry does not uses code
		/// generation it will use System.Reflection.
		/// </summary>
		public static bool IsCodeGenerationEnabled { get; }

		private static Dictionary<string, Dictionary<string, Action<object, object>>> Set { get; }

		private static Dictionary<string, Dictionary<string, Func<object, object>>> Get { get; }

		private static Dictionary<string, Dictionary<string, Type>> PropertyType { get; }

		
		/// <summary>
		/// Whether the list of registered types contains the given component.
		/// </summary>
		/// <returns>
		/// <value>TRUE</value>, if the component is contained.
		/// </returns>
		/// <param name="component">Component to look for.</param>
		public static bool ContainsComponent(Object component)
		{
			if (component == null)
			{
				return false;
			}

			if (!IsCodeGenerationEnabled)
			{
#if UNITY_IOS && !UNITY_EDITOR
				return false;
#else
				return true;
#endif
			}

			var componentName = component.GetType().FullName;
			if (string.IsNullOrWhiteSpace(componentName)) { return false; }

			if (Get == null || Set == null) { return false; }

			return Set.ContainsKey(componentName) && Get.ContainsKey(componentName);
		}

		/// <summary>
		/// Whether the registry contains the given property for the given component.
		/// </summary>
		/// <returns>
		/// <value>TRUE</value>, if the component and the property are contained.
		/// </returns>
		/// <param name="component">
		/// Component of the property to look for.
		/// </param>
		/// <param name="propertyName">Property to look for.</param>
		public static bool ContainsProperty(Object component, string propertyName)
		{
			if (string.IsNullOrWhiteSpace(propertyName) || !ContainsComponent(component))
			{
				return false;
			}

			if (!IsCodeGenerationEnabled)
			{
#if UNITY_IOS && !UNITY_EDITOR
				return false;
#else
				var supportedProperties = GetAvailableProperties(component);
				return supportedProperties.Any(
					property => property.Name == propertyName
						&& IsSupportedProperty(property)
				);
#endif
			}

			var componentName = component.GetType().FullName;
			if (string.IsNullOrWhiteSpace(componentName)) { return false; }

			return Set[componentName].ContainsKey(propertyName)
				&& Get[componentName].ContainsKey(propertyName);
		}

		/// <summary>
		/// Gets the type of the property.
		/// </summary>
		/// <param name="component">Component of the property.</param>
		/// <param name="propertyName">Property name.</param>
		/// <returns>The value.</returns>
		public static Type GetPropertyType(Object component, string propertyName)
		{
			if (!ContainsProperty(component, propertyName))
			{
				return null;
			}

			if (!IsCodeGenerationEnabled)
			{
#if UNITY_IOS && !UNITY_EDITOR
				return null;
#else
				var property = component.GetType().GetProperty(propertyName);
				return property == null ? null : property.PropertyType;
#endif
			}

			var componentName = component.GetType().FullName;
			return string.IsNullOrWhiteSpace(componentName)
				? null
				: PropertyType[componentName][propertyName];
		}

		/// <summary>
		/// Gets the value of the property.
		/// </summary>
		/// <param name="component">Component of the property.</param>
		/// <param name="propertyName">Property name.</param>
		/// <returns>The value.</returns>
		public static object GetValue(Object component, string propertyName)
		{
			if (!ContainsProperty(component, propertyName))
			{
				return null;
			}

			if (!IsCodeGenerationEnabled)
			{
#if UNITY_IOS && !UNITY_EDITOR
				return null;
#else
				var property = component.GetType().GetProperty(propertyName);
				return property == null ? null : property.GetValue(component);
#endif
			}

			var componentName = component.GetType().FullName;
			if (string.IsNullOrWhiteSpace(componentName))
			{
				return null;
			}

			return Get[componentName][propertyName](component);
		}

		/// <summary>
		/// Sets the value of the given property of the given component.
		/// </summary>
		/// <param name="component">Component of the property.</param>
		/// <param name="propertyName">Property name.</param>
		/// <param name="value">Value.</param>
		public static void SetValue(Object component, string propertyName, object value)
		{
			if (!ContainsProperty(component, propertyName))
			{
				return;
			}

			if (!IsCodeGenerationEnabled)
			{
#if UNITY_IOS && !UNITY_EDITOR
				return null;
#else
				var property = component.GetType().GetProperty(propertyName);
				if (property == null)
				{
					return;
				}

				try
				{
					property.SetValue(component, value);
				}
				catch (Exception)
				{
					//
				}

				return;
#endif
			}

			var componentName = component.GetType().FullName;
			if (string.IsNullOrWhiteSpace(componentName))
			{
				return;
			}

			Set[componentName][propertyName](component, value);
		}

#if !UNITY_IOS || UNITY_EDITOR
		/// <summary>
		/// Gets the array of the available properties for the given component.
		/// </summary>
		/// <param name="component">Component with properties.</param>
		/// <returns>Array of the available properties</returns>
		public static PropertyInfo[] GetAvailableProperties(Object component)
		{
			return component.GetType()
				.GetProperties(
					BindingFlags.Public | BindingFlags.Instance
				);
		}

		/// <summary>
		/// Whether the property info is supported by PropertyRefs.
		/// </summary>
		/// <param name="propertyInfo">Property info to validate.</param>
		/// <returns><value>TRUE</value>> if the property works with PropertyRefs.</returns>
		public static bool IsSupportedProperty(PropertyInfo propertyInfo)
		{
			return propertyInfo.CanRead
				&& propertyInfo.CanWrite
				&& !propertyInfo.IsDefined(typeof(ObsoleteAttribute), inherit: true);
		}
#endif
	}
}