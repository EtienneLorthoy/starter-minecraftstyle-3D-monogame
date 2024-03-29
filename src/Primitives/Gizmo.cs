﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Primitives
{
    public class Gizmo : DrawableGameComponent
    {
        VertexPositionColor[] vertices;
        VertexBuffer buffer;
        BasicEffect effect;

        public Gizmo(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionColor[6];

            // X
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1000, 0, 0), Color.Red);

            // Y 
            vertices[2] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Green);
            vertices[3] = new VertexPositionColor(new Vector3(0, 1000, 0), Color.Green);

            // Z
            vertices[4] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Blue);
            vertices[5] = new VertexPositionColor(new Vector3(0, 0, 1000), Color.Blue);

            // Vertex Buffer
            buffer = new VertexBuffer(this.Game.GraphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            buffer.SetData(vertices);

            // Shader
            effect = new BasicEffect(Game.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.World = Matrix.Identity;
            effect.LightingEnabled = false;
            effect.Projection = (Game as StarterGame).Camera.Projection;

            base.LoadContent();
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
