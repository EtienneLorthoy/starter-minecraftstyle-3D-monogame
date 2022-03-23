using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Primitives
{
    internal class BlockMesh : DrawableGameComponent
    {
        private static readonly float SpriteSize = 1 / 16f;
        private static readonly float offsetX = SpriteSize * 2;
        private static readonly float offsetY = SpriteSize * 0;

        private float size;
        private Vector3 pos;

        private VertexPositionTexture[] vertices;
        private Texture2D texture;
        private BasicEffect effect;

        public BlockMesh(Game game, Vector3 pos, float size = 1f) : base(game)
        {
            this.size = size;
            this.pos = pos;
        }

        protected override void LoadContent()
        {
            // Texture and offset 

            // Vertices
            vertices = new VertexPositionTexture[36];

            // Face Up
            vertices[0] = new VertexPositionTexture(new Vector3(0, 1, 0) * size + pos, new Vector2(0, SpriteSize));
            vertices[1] = new VertexPositionTexture(new Vector3(1, 1, 0) * size + pos, new Vector2(SpriteSize, SpriteSize));
            vertices[2] = new VertexPositionTexture(new Vector3(0, 1, 1) * size + pos, new Vector2(0, 0));

            vertices[3] = new VertexPositionTexture(new Vector3(0, 1, 1) * size + pos, new Vector2(0, 0));
            vertices[4] = new VertexPositionTexture(new Vector3(1, 1, 0) * size + pos, new Vector2(SpriteSize, SpriteSize));
            vertices[5] = new VertexPositionTexture(new Vector3(1, 1, 1) * size + pos, new Vector2(SpriteSize, 0));

            // Face Front
            vertices[6] = new VertexPositionTexture(new Vector3(0, 1, 1) * size + pos, new Vector2(offsetX, offsetY + SpriteSize));
            vertices[7] = new VertexPositionTexture(new Vector3(1, 1, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[8] = new VertexPositionTexture(new Vector3(0, 0, 1) * size + pos, new Vector2(offsetX, offsetY));

            vertices[9] =  new VertexPositionTexture(new Vector3(0, 0, 1) * size + pos, new Vector2(offsetX, offsetY));
            vertices[10] = new VertexPositionTexture(new Vector3(1, 1, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[11] = new VertexPositionTexture(new Vector3(1, 0, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY));

            // Face Down
            vertices[12] = new VertexPositionTexture(new Vector3(1, 0, 1) * size + pos, new Vector2(offsetX, offsetY + SpriteSize));
            vertices[13] = new VertexPositionTexture(new Vector3(1, 0, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[14] = new VertexPositionTexture(new Vector3(0, 0, 1) * size + pos, new Vector2(offsetX, offsetY));

            vertices[15] = new VertexPositionTexture(new Vector3(0, 0, 1) * size + pos, new Vector2(offsetX, offsetY));
            vertices[16] = new VertexPositionTexture(new Vector3(1, 0, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[17] = new VertexPositionTexture(new Vector3(0, 0, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY));

            // Face Left
            vertices[18] = new VertexPositionTexture(new Vector3(0, 1, 0) * size + pos, new Vector2(offsetX, offsetY + SpriteSize));
            vertices[19] = new VertexPositionTexture(new Vector3(0, 1, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[20] = new VertexPositionTexture(new Vector3(0, 0, 0) * size + pos, new Vector2(offsetX, offsetY));

            vertices[21] = new VertexPositionTexture(new Vector3(0, 0, 0) * size + pos, new Vector2(offsetX, offsetY));
            vertices[22] = new VertexPositionTexture(new Vector3(0, 1, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[23] = new VertexPositionTexture(new Vector3(0, 0, 1) * size + pos, new Vector2(offsetX + SpriteSize, offsetY));

            // Face Back
            vertices[24] = new VertexPositionTexture(new Vector3(1, 1, 0) * size + pos, new Vector2(offsetX, offsetY + SpriteSize));
            vertices[25] = new VertexPositionTexture(new Vector3(0, 1, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[26] = new VertexPositionTexture(new Vector3(1, 0, 0) * size + pos, new Vector2(offsetX, offsetY));

            vertices[27] = new VertexPositionTexture(new Vector3(1, 0, 0) * size + pos, new Vector2(offsetX, offsetY));
            vertices[28] = new VertexPositionTexture(new Vector3(0, 1, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[29] = new VertexPositionTexture(new Vector3(0, 0, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY));

            // Face Right
            vertices[30] = new VertexPositionTexture(new Vector3(1, 1, 1) * size + pos, new Vector2(offsetX, offsetY + SpriteSize));
            vertices[31] = new VertexPositionTexture(new Vector3(1, 1, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[32] = new VertexPositionTexture(new Vector3(1, 0, 1) * size + pos, new Vector2(offsetX, offsetY));

            vertices[33] = new VertexPositionTexture(new Vector3(1, 0, 1) * size + pos, new Vector2(offsetX, offsetY));
            vertices[34] = new VertexPositionTexture(new Vector3(1, 1, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize));
            vertices[35] = new VertexPositionTexture(new Vector3(1, 0, 0) * size + pos, new Vector2(offsetX + SpriteSize, offsetY));

            // Texture
            this.texture = this.Game.Content.Load<Texture2D>("terrain");

            // Shader
            effect = new BasicEffect(Game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.Projection = (Game as StarterGame).Camera.Projection;
            effect.LightingEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = this.texture;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            effect.View = (this.Game as StarterGame).Camera.View;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.Game.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                vertices, 0, vertices.Length / 3,
                VertexPositionTexture.VertexDeclaration);
            }

            base.Draw(gameTime);
        }
    }
}
