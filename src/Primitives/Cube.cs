using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Primitives
{
    internal class Cube : DrawableGameComponent
    {
        // Triangle indices
        static int[] indices = new int[]{
                0,1,2,
                0,2,3,
                0,3,1,
                2,6,5,
                1,6,2,
                4,5,6,
                4,7,5,
                4,6,7,
                5,3,2,
                5,7,3,
                1,7,6,
                1,3,7,
            };

        VertexPositionColor[] vertices;
        float size;
        Vector3 pos;
        Color color;

        public Cube(Game game, Vector3 pos, float size = 1f) : this(game, pos, Color.LightGray, size) {}

        public Cube(Game game, Vector3 pos, Color color, float size = 1f) : base(game) 
        {
            this.size = size;
            this.pos = pos;
            this.color = color;

            vertices = new VertexPositionColor[8];
        }

        public void UpdateMesh(Vector3 position, Color color)
        {
            this.pos = position;
            this.color = color;

            // Vertices
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0) * size + pos, Color.Gray);
            vertices[1] = new VertexPositionColor(new Vector3(1, 0, 0) * size + pos, color);
            vertices[2] = new VertexPositionColor(new Vector3(0, 1, 0) * size + pos, color);
            vertices[3] = new VertexPositionColor(new Vector3(0, 0, 1) * size + pos, color);

            vertices[4] = new VertexPositionColor(new Vector3(1, 1, 1) * size + pos, Color.Gray);
            vertices[5] = new VertexPositionColor(new Vector3(0, 1, 1) * size + pos, color);
            vertices[6] = new VertexPositionColor(new Vector3(1, 1, 0) * size + pos, color);
            vertices[7] = new VertexPositionColor(new Vector3(1, 0, 1) * size + pos, color);
        }

        protected override void LoadContent()
        {
            UpdateMesh(pos, color);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            (this.Game as StarterGame).Camera.DefaultCameraEffectPass.Apply();

            this.Game.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, 
                vertices, 0, vertices.Length, 
                indices, 0, indices.Length / 3, 
                VertexPositionColor.VertexDeclaration);

            base.Draw(gameTime);
        }
    }
}
