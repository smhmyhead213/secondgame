using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.GameStates
{
    public abstract class GameState
    {
        public abstract void Update();
        public abstract void Draw();
    }
}
