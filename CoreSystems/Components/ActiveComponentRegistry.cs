using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Components
{
    public static class ActiveComponentRegistry
    {
        /// <summary>
        /// A dictionary of all active components across every entity.
        /// This dictionary stores a HashSet of components for each component type, so that the set of all of one type of component can be easily retrieved.
        /// </summary>
        public static Dictionary<Type, HashSet<Component>> ActiveComponents = new Dictionary<Type, HashSet<Component>>();

        /// <summary>
        /// Adds a component to the registry. Typically called when a component is added to an entity.
        /// </summary>
        /// <param name="component">The component to be added.</param>
        public static void AddComponent(Component component)
        {
            // retrieve the hash set of all components of the addee's type, and add the addee to the hash set.
            GetSetOfComponentsOfType(typeof(Component)).Add(component);
        }

        /// <summary>
        /// Removes a component from the registry. Typically called when a component is removed from an entity.
        /// </summary>
        /// <param name="component">The component to be removed.</param>
        public static void RemoveComponent(Component component)
        {
            // retrieve the hash set of all components of the addee's type, and remove the addee from the hash set.
            GetSetOfComponentsOfType(typeof(Component)).Remove(component);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public static void RemoveComponentsFrom(Entity entity)
        {
            // iterate through every component the entity has.
            foreach (Component component in entity.Components)
            {
                // remove every component from the entity.
                RemoveComponent(component);
            }
        }

        public static HashSet<Component> GetSetOfComponentsOfType(Type componentType)
        {
            // check if the dictionary of active components contains a hash set of the given type.
            if (!ActiveComponents.ContainsKey(componentType))
            {
                // if not, first create a hash set of that type.
                ActiveComponents[componentType] = new HashSet<Component>();
            }
            // return the hash set of the needed type from the dictionary, which is safe now.
            return ActiveComponents[componentType];
        }
    }
}
