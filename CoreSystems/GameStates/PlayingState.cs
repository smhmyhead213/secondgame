using secondgame.CoreSystems.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.GameStates
{
    public class PlayingState : GameState
    {
        public LifetimeSystem LifetimeSystem;
        public MovementSystem MovementSystem;
        public DrawSystem DrawSystem;
        public PlayingState() : base()
        {
            LifetimeSystem = new LifetimeSystem();
            MovementSystem = new MovementSystem();
            DrawSystem = new DrawSystem();
        }

        public override void Draw()
        {
            DrawSystem.Draw();
        }
        public override void Update()
        {
            LifetimeSystem.Update();
            MovementSystem.Update();
            DrawSystem.Update();
        }
    }
}
