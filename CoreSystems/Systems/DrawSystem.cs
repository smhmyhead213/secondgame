using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using secondgame.Utilities;

namespace secondgame.CoreSystems.Systems
{
    public class DrawSystem : BaseSystem
    {
        public DrawComponent[] DrawComponents;
        public override void Update()
        {
            IEnumerable<DrawComponent> componentsToUpdate = EntityManager.Query<DrawComponent>();

            // sort components in order of layer
            DrawComponents = componentsToUpdate.ToArray<DrawComponent>();

            // sort in order of layer
            for (int i = 0; i < DrawComponents.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    // Swap if the element at j - 1 position is greater than the element at j position
                    if (DrawComponents[j - 1].DrawLayer > DrawComponents[j].DrawLayer)
                    {
                        int temp = (int)DrawComponents[j - 1].DrawLayer;
                        DrawComponents[j - 1] = DrawComponents[j];
                        DrawComponents[j].DrawLayer = (DrawLayer)temp;
                    }
                }
            }
            
            foreach (DrawComponent drawComponent in DrawComponents)
            {
                if (drawComponent.TakePositionFromMovementComponent)
                {
                    drawComponent.Position = drawComponent.Owner.QueryComponent<MovementComponent>().Position;
                }

                // calculate which frame to use this frame

                drawComponent.Timer += DeltaTime;
                drawComponent.FrameCounter = (int)(drawComponent.FramesPerSecond * drawComponent.Timer) % drawComponent.Frames;
            }

            MainCamera.TranslateCamera(new System.Numerics.Vector2(0f, 70f));
            MainCamera.UpdateMatrices();
        }

        public void Draw()
        {
            MainSpriteBatch.Begin(transformMatrix: MainCamera.Matrix);

            // to do: add auto-incrementing of FrameCounter and shader support
            foreach (DrawComponent component in DrawComponents)
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
