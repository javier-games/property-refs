using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monogum.BricksBucket.PropertyRefs
{
    /// <summary>
    /// Reference to a property of a component.
    /// </summary>
    /// <example>
    /// <code>
    /// [SerializeField]
    /// private PropertyRef propertyRef;
    /// </code>
    /// </example>
    [Serializable]
    public class PropertyRef
    {
        [SerializeField]
        private Object component;

        [SerializeField]
        private string property;
        
        
        /// <summary>
        /// Reference to the instance object.
        /// </summary>
        /// <returns>A reference.</returns>
        public Object Component { get => component; private set => component = value; }

        /// <summary>
        /// Name of the property of the component.
        /// </summary>
        /// <returns>Null if has not been assigned.</returns>
        public string Property { get => property; private set => property = value; }

        
        /// <summary>
        /// Set the object reference.
        /// </summary>
        /// <param name="reference">New reference.</param>
        public void SetComponent(Object reference)
        {
            if (Component == reference) return;
            Component = reference;
            Property = string.Empty;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyName">New property.</param>
        public void SetProperty(string propertyName)
        {
            if (Component == null) return;
            Property = propertyName;
        }

        /// <summary>
        /// Gets the Type of the property.
        /// </summary>
        /// <returns>Null if has not been assigned.</returns>
        public Type GetPropertyType()
        {
            return PropertiesRegistry.ContainsProperty(Component, Property)
                ? PropertiesRegistry.GetPropertyType(Component, Property)
                : null;
        }

        /// <summary>
        /// Gets the value of the reference.
        /// </summary>
        /// <returns>Null if has not been assigned.</returns>
        public object GetValue()
        {
            return PropertiesRegistry.ContainsProperty(Component, Property)
                ? PropertiesRegistry.GetValue(Component, Property)
                : null;
        }

        /// <summary>
        /// Sets the value of the property to use as reference.
        /// </summary>
        /// <param name="propertyValue">New value of the reference.</param>
        public void SetValue(object propertyValue)
        {
            if (!PropertiesRegistry.ContainsProperty(Component, Property)) return;
            PropertiesRegistry.SetValue(Component, Property, propertyValue);
        }
    }
}