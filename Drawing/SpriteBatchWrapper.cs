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
    public static class SpriteBatchWrapper
    {
        public static void Draw(Texture2D texture, Vector2 position, RectangleF? sourceRectangle, Microsoft.Xna.Framework.Color colour, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
        {
            MainSpriteBatch.Draw(texture, position, sourceRectangle?.ToRectangleXNA(), colour, rotation, origin, scale, spriteEffects, layerDepth);
        }
    }
}
