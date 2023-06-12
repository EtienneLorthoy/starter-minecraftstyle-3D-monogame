using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarterMinecraftStyleWorld;

namespace StarterMinecraftStyleWorld.Primitives
{
    public class SkyBox : DrawableGameComponent
    {
        private Texture2D texture;
        private VertexBuffer cubeVertexBuffer;
        private List<VertexPositionTexture> vertices = new List<VertexPositionTexture>();
        private static BasicEffect effect;
        private Matrix partialWorldMatrix;

        public SkyBox(Game game) : base(game)
        {
            // Create the cube's vertical faces
            BuildFace(
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 1),
                new Vector2(0, 0.25f)); // west face
            BuildFace(
                new Vector3(0, 0, 1),
                new Vector3(1, 1, 1),
                new Vector2(0.75f, 0.25f)); // south face
            BuildFace(
                new Vector3(1, 0, 1),
                new Vector3(1, 1, 0),
                new Vector2(0.5f, 0.25f)); // east face
            BuildFace(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector2(0.25f, 0.25f)); // North face

            // Top face
            vertices.Add(new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0.25f, 0.25f)));
            vertices.Add(new VertexPositionTexture(new Vector3(0, 1, 1), new Vector2(0.25f, 0f)));
            vertices.Add(new VertexPositionTexture(new Vector3(1, 1, 1), new Vector2(0.5f, 0f)));

            vertices.Add(new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(0.25f, 0.25f)));
            vertices.Add(new VertexPositionTexture(new Vector3(1, 1, 1), new Vector2(0.5f, 0)));
            vertices.Add(new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(0.5f, 0.25f)));

            // Bottom face
            vertices.Add(new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0.25f, 0.75f)));
            vertices.Add(new VertexPositionTexture(new Vector3(1, 0, 1), new Vector2(0.25f, 0.5f)));
            vertices.Add(new VertexPositionTexture(new Vector3(0, 0, 1), new Vector2(0.5f, 5f)));

            vertices.Add(new VertexPositionTexture(new Vector3(1, 0, 0), new Vector2(0.25f, 0.25f)));
            vertices.Add(new VertexPositionTexture(new Vector3(1, 0, 1), new Vector2(0.5f, 0)));
            vertices.Add(new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0.5f, 0.25f)));

            cubeVertexBuffer = new VertexBuffer(
                game.GraphicsDevice,
                VertexPositionTexture.VertexDeclaration,
                vertices.Count,
                BufferUsage.WriteOnly);

            cubeVertexBuffer.SetData(
                vertices.ToArray());
        }

        protected override void LoadContent()
        {
            if (texture == null) texture = Game.Content.Load<Texture2D>("skybox");

            // Shader
            if (effect == null)
            {
                effect = new BasicEffect(Game.GraphicsDevice);

                Matrix center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
                Matrix scale = Matrix.CreateScale(1000f);
                partialWorldMatrix = center * scale;
                Matrix translate = Matrix.CreateTranslation((Game as StarterGame).Camera.Position);

                effect.World = partialWorldMatrix * translate;
                effect.Projection = (Game as StarterGame).Camera.Projection;

                effect.VertexColorEnabled = false;
                effect.TextureEnabled = true;
                effect.LightingEnabled = false;
                effect.Texture = texture;
            }

            base.LoadContent();
        }

        override public void Update(GameTime gameTime)
        {
            var camera = (Game as StarterGame).Camera;

            // effect.World = Matrix.Identity * Matrix.CreateScale(10f);
            effect.World = partialWorldMatrix * Matrix.CreateTranslation(camera.Position);
            effect.View = camera.View;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Game.GraphicsDevice.SetVertexBuffer(cubeVertexBuffer);
                Game.GraphicsDevice.DrawPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    cubeVertexBuffer.VertexCount / 3);
            }
        }

        private void BuildFace(Vector3 p1, Vector3 p2, Vector2 txCoord)
        {
            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, txCoord.X + 0.25f, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, txCoord.X, txCoord.Y));
            vertices.Add(BuildVertex(p1.X, p2.Y, p1.Z, txCoord.X + 0.25f, txCoord.Y));

            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, txCoord.X + 0.25f, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p2.X, p1.Y, p2.Z, txCoord.X, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, txCoord.X, txCoord.Y));
        }

        private void BuildFaceHorizontal(Vector3 p1, Vector3 p2, Vector2 txCoord)
        {
            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, txCoord.X, txCoord.Y));
            vertices.Add(BuildVertex(p2.X, p1.Y, p1.Z, txCoord.X, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, txCoord.X + 0.25f, txCoord.Y));

            vertices.Add(BuildVertex(p1.X, p1.Y, p2.Z, txCoord.X, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, txCoord.X + 0.25f, txCoord.Y + 0.25f));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, txCoord.X + 0.25f, txCoord.Y));
        }

        private VertexPositionTexture BuildVertex(
            float x,
            float y,
            float z,
            float u,
            float v)
        {
            return new VertexPositionTexture(
            new Vector3(x, y, z),
            new Vector2(u, v));
        }
    }
}
