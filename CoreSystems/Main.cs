global using static secondgame.CoreSystems.Main;
global using static secondgame.CoreSystems.SoundSystems.SoundSystem;
global using static secondgame.CoreSystems.InputSystem;
global using static secondgame.Utilities.Utilities;
global using static System.MathF;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using secondgame.CoreSystems.GameStates;
using System.Collections.Generic;

namespace secondgame.CoreSystems
{
    public class Main : Game
    {
        public GraphicsDeviceManager Graphics;
        public static SpriteBatch MainSpriteBatch;
        public static Camera MainCamera;
        public static int Width;
        public static int Height;
        public static float DeltaTime;
        public int time;
        public System.Numerics.Vector2 position;

        public static GameState GameState;
        public Main()
        {
            MainInstance = this;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Graphics.IsFullScreen = true;
            Graphics.HardwareModeSwitch = false;
        }
        public static Main MainInstance
        {
            get;
            private set;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            position = System.Numerics.Vector2.Zero;
            GameState = new PlayingState();
            MainCamera = new Camera();
            Entity test = new Entity();
            test.AddComponent(new MovementComponent(position, new System.Numerics.Vector2(0f, 0f), new System.Numerics.Vector2(0f, 0f)));
            test.AddComponent(new DrawComponent("Assets/PlayerAssets/maincharacter", 8, 10, DrawLayer.Player, position, true));
            test.Spawn();
            
        }

        protected override void LoadContent()
        {
            MainSpriteBatch = new SpriteBatch(GraphicsDevice);

            SetUpFMOD();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateInputSystem();

            FMODStudioSystem.update(); // maybe move this to SoundSystem in future if something else needs updated every frame

            GameState.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            GameState.Draw();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}