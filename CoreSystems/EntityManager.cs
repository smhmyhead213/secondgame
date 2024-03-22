﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems
{
    public static class EntityManager
    {
        public static List<Entity> ActiveEntities = new List<Entity>();

        // could maybe query from the ActiveComponentRegistry??
        public static IEnumerable<TComponentType> Query<TComponentType>() where TComponentType : Component
        {
            foreach (Entity entity in ActiveEntities)
            {
                foreach (Component component in entity.Components)
                { 
                    if (component.GetType() == typeof(TComponentType))
                    {
                        yield return component;
                    }
                }
            }
        }
    }
}
