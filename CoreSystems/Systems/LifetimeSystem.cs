using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Systems
{
    public class LifetimeSystem : System
    {
        public override void Update()
        {
            // get an enumberable of all lifetime components that are active.
            IEnumerable<LifetimeComponent> componentsToUpdate = EntityManager.Query<LifetimeComponent>();

            foreach (LifetimeComponent component in componentsToUpdate)
            {
                component.TimeSinceSpawn += DeltaTime;

                if (component.TimeSinceSpawn >= component.Lifetime)
                {
                    component.Owner.Destroy();
                }
            }
        }
    }
}
