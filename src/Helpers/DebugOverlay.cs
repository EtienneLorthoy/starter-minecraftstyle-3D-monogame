using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StarterMinecraftStyleWorld.Helpers
{
    public class DebugOverlay : DrawableGameComponent, IOverlayDebugOutput
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private Stopwatch drawTimer;
        private Stopwatch updateTimer;

        private long lastDraw;
        private long lastUpdate;
        private long lastFps;
        private long fpszero;

        private int primitiveCount;
        private int lastPrimitiveCount;

        private string outputString;
        private Color outputColor;

        public DebugOverlay(Game game) : base(game)
        {
            drawTimer = new Stopwatch();
            updateTimer = new Stopwatch();
            outputString = String.Empty;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>("Debug");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (lastDraw == 0L && lastUpdate == 0L) fpszero = 1;
            else fpszero = 0;
            lastFps = 1000L / (lastDraw + lastUpdate + fpszero);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw rendering measurements
            lastPrimitiveCount = primitiveCount;
            primitiveCount = 0;

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Update " + lastUpdate + " ms", new Vector2(10, 10), Color.Blue);
            spriteBatch.DrawString(spriteFont, "Draw " + lastDraw + " ms", new Vector2(10, 40), Color.Green);
            spriteBatch.DrawString(spriteFont, lastFps + " Fps", new Vector2(10, 70), Color.Red);
            spriteBatch.DrawString(spriteFont, lastPrimitiveCount + " triangles", new Vector2(10, 100), Color.AliceBlue);
            spriteBatch.DrawString(spriteFont, outputString, new Vector2(10, 130), outputColor);
            spriteBatch.End();

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            outputString = string.Empty;

            base.Draw(gameTime);
        }

        public void AddToTriangleCount(int count)
        {
            primitiveCount += count;
        }

        public void StartDraw()
        {
            lastDraw = drawTimer.ElapsedMilliseconds;
            drawTimer.Restart();
        }

        public void StopDraw()
        {
            drawTimer.Stop();
        }

        public void StartUpdate()
        {
            lastUpdate = updateTimer.ElapsedMilliseconds;
            updateTimer.Restart();
        }

        public void StopUpdate()
        {
            updateTimer.Stop();
        }

        public void WriteLine(string line)
        {
            this.WriteLine(line, Color.Red);
        }

        public void WriteLine(string line, Color color)
        {
            outputString += line + "\n";
            outputColor = color;
        }
    }
}
