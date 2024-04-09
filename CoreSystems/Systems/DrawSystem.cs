using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using secondgame.Utilities;
using SharpDX.WIC;
using SharpDX.MediaFoundation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace secondgame.CoreSystems.Systems
{
    public class DrawSystem : BaseSystem
    {
        public DrawComponent[] DrawComponents;
        public float scale;

        public DrawSystem()
        {
            scale = 1f;
        }

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

            CameraTest();
            MainCamera.UpdateMatrices();
        }
        public void Draw()
        {
            MainSpriteBatch.Begin(transformMatrix: MainCamera.Matrix);

            Texture2D background = MainInstance.Content.Load<Texture2D>("Assets/testbackground");
            Vector2 offsetington = new Vector2(background.Width / 2f, background.Height / 2f);
            MainSpriteBatch.Draw(background, System.Numerics.Vector2.Zero, null, Color.White, 0, offsetington, Vector2.One * 10f, SpriteEffects.None, 0);

            // to do: add auto-incrementing of FrameCounter and shader support
            foreach (DrawComponent component in DrawComponents)
            {
                Texture2D spriteSheet = component.SpriteSheet.Asset;
                int frameHeight = spriteSheet.Height / component.Frames;
                int startHeight = component.FrameCounter * frameHeight;
                Vector2 offset = new Vector2(spriteSheet.Width / 2f, frameHeight / 2f);
                //offset = Vector2.Zero;
                MainSpriteBatch.Draw(component.SpriteSheet.Asset, component.Position, new Rectangle(0, startHeight, spriteSheet.Width, frameHeight), Color.White, 0, offset, Vector2.One * 0.3f, SpriteEffects.None, 0);
            }

            MainSpriteBatch.End();
        }

        public void CameraTest()
        {
            #region Camera Test
            float cameraSpeed = 2f;
            bool zooming = false;

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.W))
            {
                MainCamera.MoveCameraBy(new System.Numerics.Vector2(0f, cameraSpeed));
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.S))
            {
                MainCamera.MoveCameraBy(new System.Numerics.Vector2(0f, -cameraSpeed));
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                MainCamera.MoveCameraBy(new System.Numerics.Vector2(cameraSpeed, 0f));
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
            {
                MainCamera.MoveCameraBy(new System.Numerics.Vector2(-cameraSpeed, 0f));
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                MainCamera.ResetMatrices();
                scale = 1f;
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.O))
            {
                scale = scale + 0.01f;
                zooming = true;
            }

            if (IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
            {
                scale = scale - 0.01f;
                zooming = true;
            }

            if (scale <= 0f)
            {
                scale = 0.01f;
            }

            if (zooming)
                MainCamera.SetZoom(scale, ScreenCentre());
            else
            {
                MainCamera.ResetZoom();
                MainCamera.Origin = System.Numerics.Vector2.Zero;
            }
            #endregion
        }
    }
}
