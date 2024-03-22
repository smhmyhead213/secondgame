﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace secondgame.CoreSystems.Components
{
    public class DrawComponent : Component
    {
        public Texture2D SpriteSheet;
        public int Frames; // number of frames in spritesheet
        public int FramesPerSecond;
        public int FrameCounter;
        public DrawLayer DrawLayer;
        public Vector2 Position;
        public float Timer;

        public DrawComponent(string spriteSheet, int frames, int fps, DrawLayer drawLayer, Vector2 position)
        { 
            SpriteSheet = MainInstance.Content.Load<Texture2D>(spriteSheet);
            Frames = frames;
            FramesPerSecond = fps;
            DrawLayer = drawLayer;
            Position = position;
        }
    }

    public enum DrawLayer
    {
        Projectiles,
        Player,
        NPCs,
        Background
    }
}
