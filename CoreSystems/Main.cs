global using static secondgame.CoreSystems.Main;
global using static secondgame.CoreSystems.SoundSystems.SoundSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace secondgame.CoreSystems
{
    public class Main : Game
    {
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public static int Width;
        public static int Height;
        public static float DeltaTime;
        public int time;
        public Vector2 position;
        
        public Main()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Graphics.IsFullScreen = true;
            Graphics.HardwareModeSwitch = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            position = new Vector2(0, Graphics.PreferredBackBufferHeight / 2f);
            
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            SetUpFMOD();            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            FMODStudioSystem.update(); // maybe move this to SoundSystem in future if something else needs updated every frame

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (time % 120 == 0)
            {
                PlaySound("testsound");
            }
            time++;
            position.X = time * 3f;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here
            int frames = 8;
            int frameToUse = time % frames;
            Texture2D texture = Content.Load<Texture2D>("PlayerAssets/maincharacter");
            int frameHeight = texture.Height / frames;
            int startHeight = frameToUse * frameHeight;
            SpriteBatch.Begin();
            SpriteBatch.Draw(texture, position, new Rectangle(0, startHeight, texture.Width, frameHeight), Color.White, 0, new Vector2(texture.Width / 2f, -frameHeight / 2f), new Vector2(0.3f), SpriteEffects.None, 0);
            SpriteBatch.End();


            base.Draw(gameTime);
        }
    }
}