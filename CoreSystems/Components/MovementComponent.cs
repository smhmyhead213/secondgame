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
        public MovementComponent(Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }
        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void SetAcceleration(Vector2 acceleration)
        {
            Acceleration = acceleration;
        }
    }
}
