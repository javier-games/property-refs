using System.Collections.Generic;
using UnityEngine;

namespace Monogum.BricksBucket.PropertyRefs.Samples.PropertyRefsSourceGenerator
{
    /// <summary>
    /// Assign this script to a game object and assign a GameObject or component to it.
    /// </summary>
    public class PrefabProperties : MonoBehaviour
    {
        /// <summary>
        /// Single property reference.
        /// </summary>
        [SerializeField]
        private PropertyRef propertyRef;

        /// <summary>
        /// List of references.
        /// </summary>
        [SerializeField]
        private List<PropertyRef> properties;
    }
}