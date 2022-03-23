using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace StarterMinecraftStyleWorld.Terrains.Testers
{
    public class TerrainOnRessourceTester
    {
        private List<TerrainOnRessourceChunk> chunks = new List<TerrainOnRessourceChunk>();

        public void AddMapChunk(BlockType[,,] map, int shiftX, int shiftZ, int shiftY)
        {
            chunks.Add(new TerrainOnRessourceChunk(map, shiftX, shiftZ, shiftY));
        }

        public BlockType GetBelowBlockType(Vector3 position)
        {
            var value = BlockType.Air;
            foreach (var chunk in chunks)
            {
                var test = chunk.GetBelowBlockType(position);
                if (test != BlockType.Void) value = test;
            }
            return value;
        }

        private class TerrainOnRessourceChunk
        {
            private BlockType[,,] map;
            private int shiftX;
            private int shiftZ;
            private int shiftY;

            public TerrainOnRessourceChunk(BlockType[,,] map, int shiftX, int shiftZ, int shiftY)
            {
                this.map = map;
                this.shiftX = shiftX;
                this.shiftZ = shiftZ;
                this.shiftY = shiftY;
            }

            public BlockType GetBelowBlockType(Vector3 position)
            {
                // Off map X
                var indexX = (int)position.X + shiftX;
                if (indexX < 0 || indexX >= map.GetLength(0)) return BlockType.Void;

                // Off map Y
                var indexY = (int)position.Y + shiftY - 1; // -1 so below it
                if (indexY < 0 || indexY >= map.GetLength(1)) return BlockType.Void;

                // Off map Z
                var indexZ = (int)position.Z + shiftZ;
                if (indexZ < 0 || indexZ >= map.GetLength(2)) return BlockType.Void;

                // On map assert below
                return  map[indexX, indexY, indexZ];
            }
        }
    }
}
