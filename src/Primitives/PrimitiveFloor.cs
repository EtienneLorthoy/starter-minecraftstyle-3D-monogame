using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Primitives
{
    internal class PrimitiveFloor : DrawableGameComponent
    {
        VertexPositionColor[] vertices;

        public PrimitiveFloor(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            var size = 512;
            vertices = new VertexPositionColor[size*4];

            for (int i = 0; i < size*4; i+=4)
            {
                // X
                vertices[i] = new VertexPositionColor(new Vector3(size/2, 0, size/2 - i/4), Color.LightSalmon);
                vertices[i + 1] = new VertexPositionColor(new Vector3(-size/2, 0, size/2 - i/4), Color.LightSalmon);

                // Z
                vertices[i + 2] = new VertexPositionColor(new Vector3(size/2 - i/4, 0, size/2), Color.LightBlue);
                vertices[i + 3] = new VertexPositionColor(new Vector3(size/2 - i/4, 0, -size/2), Color.LightBlue);
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            (this.Game as StarterGame).Camera.DefaultCameraEffectPass.Apply();
            this.Game.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, vertices.Length/2);
            
            base.Draw(gameTime);
        }
    }
}
