using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Primitives
{
    internal class VisualVector3 : DrawableGameComponent
    {
        VertexPositionColor[] vertices;
        VertexBuffer buffer;
        BasicEffect effect;

        public VisualVector3(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {

            // Shader
            effect = new BasicEffect(Game.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.World = Matrix.Identity;
            effect.LightingEnabled = false;
            effect.Projection = (Game as StarterGame).Camera.Projection;

            base.LoadContent();
        }

        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            vertices = new VertexPositionColor[6];

            vertices[0] = new VertexPositionColor(position, Color.Yellow);
            vertices[1] = new VertexPositionColor(position + direction, Color.Red);

            // Vertex Buffer
            buffer = new VertexBuffer(this.Game.GraphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            buffer.SetData(vertices);
        }

        public override void Update(GameTime gameTime)
        {
            effect.View = (Game as StarterGame).Camera.View;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.Game.GraphicsDevice.SetVertexBuffer(buffer);
                this.Game.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, vertices.Length / 2);
            }

            base.Draw(gameTime);
        }
    }
}
