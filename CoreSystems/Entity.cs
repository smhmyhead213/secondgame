using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using secondgame.CoreSystems.Components;

namespace secondgame.CoreSystems
{
    public class Entity
    {
        public List<Component> Components = new List<Component>();
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

        public TComponentType QueryComponent<TComponentType>() where TComponentType : Component
        {
            foreach (TComponentType component in Components)
            {
                if (component.GetType() == typeof(TComponentType))
                {
                    return component;
                }
            }

            return null;
        }
        public void Destroy()
        {
            Components.Clear();
            ActiveComponentRegistry.RemoveComponentsFrom(this);
            EntityManager.ActiveEntities.Remove(this);
        }

        public void SetPosition(Vector2 position)
        {
            MovementComponent movementComponent = QueryComponent<MovementComponent>();
            if (movementComponent != null)
            {
                movementComponent.SetPosition(position);
            }
            else
            {
                throw new Exception("Cannot set the position of an entity without a MovementComponent.");
            }
        }
        public void SetVelocity(Vector2 velocity)
        {
            MovementComponent movementComponent = QueryComponent<MovementComponent>();
            if (movementComponent != null)
            {
                movementComponent.SetVelocity(velocity);
            }
            else
            {
                throw new Exception("Cannot set the velocity of an entity without a MovementComponent.");
            }
        }
        public void SetAccleration(Vector2 acceleration)
        {
            MovementComponent movementComponent = QueryComponent<MovementComponent>();
            if (movementComponent != null)
            {
                movementComponent.SetAcceleration(acceleration);
            }
            else
            {
                throw new Exception("Cannot set the acceleration of an entity without a MovementComponent.");
            }
        }
        /// <summary>
        /// Every entity should override this method and add all of its components here.
        /// </summary>
        public virtual void Spawn()
        {
            EntityManager.ActiveEntities.Add(this);
        }
    }
}
