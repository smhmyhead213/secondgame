using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.Components
{
    public class MovementComponent : Component
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public MovementComponent()
        {
            Position = new Vector2();
            Velocity = new Vector2();
            Acceleration = new Vector2();
        }
    }
}
