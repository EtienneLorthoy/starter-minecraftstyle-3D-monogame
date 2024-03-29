﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarterMinecraftStyleWorld
{
    public class BasicCamera : GameComponent
    {
        public Vector3 Position => cameraPosition;
        public ref Matrix View => ref view;
        public Matrix Projection { get; protected set; }
        public EffectPass DefaultCameraEffectPass => effect.CurrentTechnique.Passes[0];

        private Matrix view;

        private Vector3 cameraPosition;
        private Vector3 cameraDirection;
        private Vector3 cameraTarget;
        private Vector3 cameraUp;

        private int captivePositionX;
        private int captivePositionY;
        private MouseState lastMouseState;
        private bool mouseCaptive;

        private BasicEffect effect;

        //defines speed of camera movement
        private float speed = 0.03f;

        public BasicCamera(Game game, Vector3 pos, Vector3 target, Vector3 up)
            : base(game)
        {
            // Build camera view matrix
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, 1, 1024);
        }

        public override void Initialize()
        {
            // Set mouse position
            captivePositionX = Game.Window.ClientBounds.Width / 2;
            captivePositionY = Game.Window.ClientBounds.Height / 2;
            Mouse.SetPosition(captivePositionX, captivePositionY);

            // Shader
            effect = new BasicEffect(Game.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.World = Matrix.Identity;
            effect.Projection = (Game as StarterGame).Camera.Projection;
            effect.LightingEnabled = false;

            // Mouse
            lastMouseState = Mouse.GetState();
            mouseCaptive = true;
            this.Game.IsMouseVisible = false;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Speed Shift 
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) speed = 0.3f;
            else speed = 0.03f;

            // Move forward and backward
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) cameraPosition += cameraDirection * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) cameraPosition -= cameraDirection * speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Q)) cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D)) cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;

            // Mouse logic captive/free
            if (Mouse.GetState().LeftButton == ButtonState.Pressed 
                && lastMouseState.LeftButton == ButtonState.Released)
            {
                Mouse.SetPosition(captivePositionX, captivePositionY);
                this.mouseCaptive = !this.mouseCaptive;
                this.Game.IsMouseVisible = !this.Game.IsMouseVisible;
            }
            lastMouseState = Mouse.GetState();

            // Update Camera
            if (this.Game.IsActive && this.mouseCaptive)
            {
                // Rotation in the world
                cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - captivePositionX)));
                cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - captivePositionY)));

                Mouse.SetPosition(captivePositionX, captivePositionY);

                cameraTarget = cameraPosition + cameraDirection;
                Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUp, out view);
                effect.View = view;
            }

            base.Update(gameTime);
        }
    }
}