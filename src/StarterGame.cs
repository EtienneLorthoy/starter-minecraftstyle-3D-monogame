using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarterMinecraftStyleWorld.Helpers;
using StarterMinecraftStyleWorld.Primitives;
using StarterMinecraftStyleWorld.Terrains;
using System;

namespace StarterMinecraftStyleWorld
{
    public class StarterGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private Model model;

        public BasicCamera Camera;
        public DebugOverlay DebugOverlay;

        public StarterGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            this.Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            _graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;
            e.GraphicsDeviceInformation.PresentationParameters.DepthStencilFormat = DepthFormat.Depth24;
        }

        protected override void Initialize()
        {
            this._graphics.PreferredBackBufferWidth = 1920;
            this._graphics.PreferredBackBufferHeight = 1080;
            this._graphics.SynchronizeWithVerticalRetrace = false;
            this._graphics.ApplyChanges();

            // Texture filtering
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            // Settings
            this.IsFixedTimeStep = false;

            // Camera
            Camera = new BasicCamera(this, new Vector3(0,20,0), new Vector3(20,0,20), Vector3.Up);
            this.Components.Add(Camera);

            // World Entities
            this.Components.Add(new PrimitiveFloor(this));
            this.Components.Add(new Gizmo(this));
            this.Components.Add(new SkyBox(this));
            this.Components.Add(new World(this));

            this.Components.Add(new LightModel(this));

            this.Components.Add(new Cube(this, new Vector3(-10, 3, -10), 0.9f));
            this.Components.Add(new BlockMesh(this, new Vector3(-10, 0, -10)));

            DebugOverlay = new DebugOverlay(this);
            DebugOverlay.DrawOrder = int.MaxValue;
            this.Components.Add(DebugOverlay);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            model = Content.Load<Model>("Shipb");
        }

        protected override void Update(GameTime gameTime)
        {
            DebugOverlay.StartUpdate();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            DebugOverlay.StopUpdate();
        }

        protected override void Draw(GameTime gameTime)
        {
            DebugOverlay.StartDraw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            model.Draw(Matrix.Identity, Camera.View, Camera.Projection);

            base.Draw(gameTime);

            DebugOverlay.StopDraw();
        }
    }
}
