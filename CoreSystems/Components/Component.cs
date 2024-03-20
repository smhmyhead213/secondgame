global using secondgame.CoreSystems.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace secondgame.CoreSystems.Components
{
    public abstract class Component
    {
        public Entity Owner;
        public abstract void Update();
    }
}
