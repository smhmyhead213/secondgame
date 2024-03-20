using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Components
{
    public static class ActiveComponentRegistry
    {
        public static Dictionary<Type, HashSet<Component>> ActiveComponents = new Dictionary<Type, HashSet<Component>>();

        public static void AddComponent(Component component)
        {
            // retrieve the hash set of all components of the addee's type, and add the addee to the hash set.
            GetSetOfComponentsOfType(typeof(Component)).Add(component);
        }

        public static void RemoveComponent(Component component)
        {
            // retrieve the hash set of all components of the addee's type, and remove the addee from the hash set.
            GetSetOfComponentsOfType(typeof(Component)).Remove(component);
        }

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
