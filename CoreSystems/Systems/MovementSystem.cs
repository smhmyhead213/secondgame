using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Systems
{
    public class MovementSystem : BaseSystem
    {
        public override void Update()
        {
            // get an enumberable of all movement components that are active.
            IEnumerable<MovementComponent> componentsToUpdate = EntityManager.Query<MovementComponent>();

            foreach (MovementComponent component in componentsToUpdate)
            {
                component.Velocity += component.Acceleration * DeltaTime * 0.5f;
                component.Position += component.Velocity * DeltaTime;
                component.Velocity += component.Acceleration * DeltaTime * 0.5f;
            }
        }
    }
}
