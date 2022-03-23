using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace StarterMinecraftStyleWorld.Terrains
{
    public struct BlockTextureOffsets
    {
        public float DefaultFaceOffsetX { get; set; }
        public float DefaultFaceOffsetY { get; set; }
        public float TopFaceOffsetX { get; set; }
        public float TopFaceOffsetY { get; set; }

        public BlockTextureOffsets(float defaultFaceOffsetX, float defaultFaceOffsetY, float topFaceOffsetX = -1, float topFaceOffsetY = -1)
        {
            DefaultFaceOffsetX = defaultFaceOffsetX;
            DefaultFaceOffsetY = defaultFaceOffsetY;
            TopFaceOffsetX = topFaceOffsetX;
            TopFaceOffsetY = topFaceOffsetY;
        }

        public static BlockTextureOffsets GetTextureOffset(BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.Air:
                    return new BlockTextureOffsets(0, 0);
                case BlockType.Grass:
                    return new BlockTextureOffsets(2, 0, 0, 0);
                case BlockType.Dirt:
                    return new BlockTextureOffsets(2, 0);
                case BlockType.Stone:
                    return new BlockTextureOffsets(1, 0);
                case BlockType.Bedrock:
                    return new BlockTextureOffsets(5, 2);
                case BlockType.Water:
                    return new BlockTextureOffsets(14, 0);
                case BlockType.WhiteSpot:
                    return new BlockTextureOffsets(15, 4);
                default:
                    return new BlockTextureOffsets(0, 0);
            }
        }
    }
}