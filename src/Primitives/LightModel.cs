using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterMinecraftStyleWorld.Primitives
{
    internal class LightModel : DrawableGameComponent
    {
        private static BasicEffect effect;
        private Model model;

        public LightModel(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("cat");

            // Shader
            if (effect == null)
            {
                effect = new BasicEffect(Game.GraphicsDevice);
                effect.World = Matrix.CreateScale(0.001f, 0.001f, 0.001f) * Matrix.CreateTranslation(0, 15, 2);
                effect.Projection = (Game as StarterGame).Camera.Projection;

                effect.VertexColorEnabled = false;

                effect.LightingEnabled = true;
                effect.PreferPerPixelLighting = false;

                effect.DirectionalLight0.Enabled = true;
                effect.DirectionalLight0.DiffuseColor = Color.LightGray.ToVector3();
                effect.DirectionalLight0.Direction = new Vector3(1, -1, 1);

                effect.DirectionalLight1.Enabled = true;
                effect.DirectionalLight1.DiffuseColor = Color.DarkGray.ToVector3();
                effect.DirectionalLight1.Direction = new Vector3(-1, -0.5f, -1);

                effect.DirectionalLight2.Enabled = false;
            }

                base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Actual drawing
            foreach (var mesh in model.Meshes)
            {
                for (int i = 0; i < mesh.MeshParts.Count; i++)
                {
                    var part = mesh.MeshParts[i];

                    if (part.PrimitiveCount > 0)
                    {
                        this.GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                        this.GraphicsDevice.Indices = part.IndexBuffer;

                        for (int j = 0; j < effect.CurrentTechnique.Passes.Count; j++)
                        {
                            effect.CurrentTechnique.Passes[j].Apply();
                            GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, part.StartIndex, part.PrimitiveCount);
                        }
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
