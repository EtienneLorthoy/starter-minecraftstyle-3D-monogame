using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarterMinecraftStyleWorld.Terrains
{
    internal class LinkedBlockMesh
    {
        private static readonly float SpriteSize = 1 / 16f;

        // All faces
        private readonly float offsetX;
        private readonly float offsetY;

        private readonly float topFaceOffsetX;
        private readonly float topFaceOffsetY;

        private readonly float size;
        private Vector3 pos;
        private Vector3 normal = Vector3.Forward;

        public LinkedBlockMesh Up;
        public LinkedBlockMesh Down;
        public LinkedBlockMesh Front;
        public LinkedBlockMesh Back;
        public LinkedBlockMesh Left;
        public LinkedBlockMesh Right;

        public LinkedBlockMesh(Vector3 pos, BlockTextureOffsets offsets, float size = 1f)
        {
            this.size = size;
            this.pos = pos;
            this.offsetX = SpriteSize * offsets.DefaultFaceOffsetX;
            this.offsetY = SpriteSize * offsets.DefaultFaceOffsetY;
            if (offsets.TopFaceOffsetX == -1 || offsets.TopFaceOffsetX == -1)
            {
                this.topFaceOffsetX = SpriteSize * offsets.DefaultFaceOffsetX;
                this.topFaceOffsetY = SpriteSize * offsets.DefaultFaceOffsetY;
            }
            else
            {
                this.topFaceOffsetX = SpriteSize * offsets.TopFaceOffsetX;
                this.topFaceOffsetY = SpriteSize * offsets.TopFaceOffsetY;
            }
        }

        public VertexPositionNormalTexture [] ConstructMesh()
        {
            var vertices = new List<VertexPositionNormalTexture >();

            // Face Up
            if (Up == null)
            {
                var A = new Vector3(0, 1, 0);
                var B = new Vector3(1, 1, 0);
                var C = new Vector3(0, 1, 1);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 0) * size + pos, normal, new Vector2(topFaceOffsetX, topFaceOffsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 0) * size + pos, normal, new Vector2(topFaceOffsetX + SpriteSize, topFaceOffsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 1) * size + pos, normal, new Vector2(topFaceOffsetX, topFaceOffsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 1) * size + pos, normal, new Vector2(topFaceOffsetX, topFaceOffsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 0) * size + pos, normal, new Vector2(topFaceOffsetX + SpriteSize, topFaceOffsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 1) * size + pos, normal, new Vector2(topFaceOffsetX + SpriteSize, topFaceOffsetY)));
            }

            // Face Front
            if (Front == null)
            {
                var A = new Vector3(0, 1, 1);
                var B = new Vector3(1, 1, 1);
                var C = new Vector3(0, 0, 1);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 1) * size + pos, normal, new Vector2(offsetX, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY)));
            }

            // Face Down
            if (Down == null)
            {
                var A = new Vector3(1, 0, 1);
                var B = new Vector3(1, 0, 0);
                var C = new Vector3(0, 0, 1);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY)));
            }

            // Face Left
            if (Left == null)
            {
                var A = new Vector3(0, 1, 0);
                var B = new Vector3(0, 1, 1);
                var C = new Vector3(0, 0, 0);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 0) * size + pos, normal, new Vector2(offsetX, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 0) * size + pos, normal, new Vector2(offsetX, offsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 0) * size + pos, normal, new Vector2(offsetX, offsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 1) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY)));
            }

            // Face Back
            if (Back == null)
            {
                var A = new Vector3(1, 1, 0);
                var B = new Vector3(0, 1, 0);
                var C = new Vector3(1, 0, 0);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 0) * size + pos, normal, new Vector2(offsetX, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 0) * size + pos, normal, new Vector2(offsetX, offsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 0) * size + pos, normal, new Vector2(offsetX, offsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 1, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(0, 0, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY)));
            }

            // Face Right
            if (Right == null)
            {
                var A = new Vector3(1, 1, 1);
                var B = new Vector3(1, 1, 0);
                var C = new Vector3(1, 0, 1);

                var ab = A - B; var cb = C - B;
                ab.Normalize(); cb.Normalize();
                normal = Vector3.Cross(ab, cb);

                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 1) * size + pos, normal, new Vector2(offsetX, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));

                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 1) * size + pos, normal, new Vector2(offsetX, offsetY)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 1, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY + SpriteSize)));
                vertices.Add(new VertexPositionNormalTexture (new Vector3(1, 0, 0) * size + pos, normal, new Vector2(offsetX + SpriteSize, offsetY)));
            }

            return vertices.ToArray();
        }
    }
}
