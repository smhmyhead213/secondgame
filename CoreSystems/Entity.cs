using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using secondgame.CoreSystems.Components;

namespace secondgame.CoreSystems
{
    public class Entity
    {
        public List<Component> Components;
        public void AddComponent(Component component)
        {
            component.Owner = this;
            Components.Add(component);
            ActiveComponentRegistry.AddComponent(component);
        }
        public void RemoveComponent(Component component)
        {
            Components.Remove(component);
            ActiveComponentRegistry.RemoveComponent(component);
        }

        public void Destroy()
        {
            Components.Clear();
            ActiveComponentRegistry.RemoveComponentsFrom(this);
            EntityManager.ActiveEntities.Remove(this);
        }
    }
}
