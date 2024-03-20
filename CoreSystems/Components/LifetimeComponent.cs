using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Components
{
    public class LifetimeComponent : Component
    {
        public float Lifetime;
        public float TimeSinceSpawn;
        // maybe add capability for pausing in the future?
        public override void Update()
        {
            
        }
    }
}
