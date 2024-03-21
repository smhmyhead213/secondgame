using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace secondgame.CoreSystems.Components
{
    public class DrawComponent : Component
    {
        public Texture2D SpriteSheet;
        public int Frames;
        public float AnimationSpeed;
        public int FrameCounter;
        public DrawLayer DrawLayer;
        public Vector2 Position;
    }

    public enum DrawLayer
    {
        Projectiles,
        Player,
        NPCs,
        Background
    }
}
