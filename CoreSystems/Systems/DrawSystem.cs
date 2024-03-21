using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using secondgame.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace secondgame.CoreSystems.Systems
{
    public class DrawSystem : System
    {
        public override void Update()
        {
            IEnumerable<DrawComponent> componentsToUpdate = EntityManager.Query<DrawComponent>();

            // sort components in order of layer
            DrawComponent[] drawComponents = componentsToUpdate.ToArray<DrawComponent>();

            // sort in order of layer
            for (int i = 0; i < drawComponents.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    // Swap if the element at j - 1 position is greater than the element at j position
                    if (drawComponents[j - 1].DrawLayer > drawComponents[j].DrawLayer)
                    {
                        int temp = (int)drawComponents[j - 1].DrawLayer;
                        drawComponents[j - 1] = drawComponents[j];
                        drawComponents[j].DrawLayer = (DrawLayer)temp;
                    }
                }
            }

            MainSpriteBatch.Begin();

            // to do: add auto-incrementing of FrameCounter and shader support
            foreach (DrawComponent component in drawComponents)
            {               
                Texture2D spriteSheet = component.SpriteSheet;
                int frameHeight = component.SpriteSheet.Height / component.Frames;
                int startHeight = component.FrameCounter * frameHeight;
                MainSpriteBatch.Draw(component.SpriteSheet, component.Position, new Rectangle(0, startHeight, spriteSheet.Width, frameHeight), Color.White, 0, new Vector2(spriteSheet.Width / 2f, -frameHeight / 2f), new Vector2(0.3f), SpriteEffects.None, 0);                
            }

            MainSpriteBatch.End();
        }
    }
}
