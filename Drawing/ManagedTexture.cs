using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace secondgame.Drawing
{
    public class ManagedTexture
    {
        private Texture2D asset;
        public float TimeSinceLastUse;
        public float Rotation;
        public Texture2D Asset
        {
            get
            {
                return asset;
            }
            set
            {
                asset = value;
                TimeSinceLastUse = 0;
            }
        }

        public ManagedTexture(Texture2D asset)
        {
            this.asset = asset;
            TimeSinceLastUse = 0;
            Rotation = 0;
        }
    }
}
