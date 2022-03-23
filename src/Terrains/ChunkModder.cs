using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterMinecraftStyleWorld.Terrains
{
    public static class ChunkModder
    {
        private static readonly int wallHeight = 4;

        public static void ModHeightMapForWalls(int size, int[,] heightMap)
        {
            for (int x = size / 3; x <= size / 3; x++)
            {
                for (int z = size / 4; z < 3 * size / 4; z++)
                {
                    heightMap[x, z] = wallHeight;
                }
            }

            for (int z = size / 3; z <= size / 3; z++)
            {
                for (int x = size / 4; x < 3 * size / 4; x++)
                {
                    heightMap[x, z] = wallHeight;
                }
            }
        }
        public static void ModMapForWalls(int size, BlockType[,,] map)
        {
            for (int x = size / 3; x <= size / 3; x++)
                for (int z = size / 4; z < 3 * size / 4; z++)
                    for (int y = wallHeight - 3; y <= wallHeight; y++)
                    {
                        map[x, y, z] = BlockType.Stone;
                    }

            for (int z = size / 3; z <= size / 3; z++)
                for (int x = size / 4; x < 3 * size / 4; x++)
                    for (int y = wallHeight - 3; y <= wallHeight; y++)
                    {
                        map[x, y, z] = BlockType.Stone;
                    }
        }

        public static void ModHeightMapForWater(int size, int[,] heightMap)
        {
            for (int x = size / 8 * 1; x <= size / 8 * 2; x++)
                for (int z = size / 8 * 1; z <= size / 8 * 2; z++)
                {
                    heightMap[x, z] = 3;
                }
        }
        public static void ModMapForWater(int size, BlockType[,,] map, int[,] heightMap)
        {
            for (int x = size / 8 * 1; x <= size / 8 * 2; x++)
                for (int z = size / 8 * 1; z <= size / 8 * 2; z++)
                    for (int y = heightMap[x, z]; y <= heightMap[x, z]; y++)
                    {
                        map[x, y, z] = BlockType.Water;
                    }
        }

        public static void ModHeightMapForSafe(int size, int[,] heightMap)
        {
            for (int x = size / 8 * 6; x <= size / 8 * 7; x++)
                for (int z = size / 8 * 6; z <= size / 8 * 7; z++)
                {
                    heightMap[x, z] = 3;
                }
        }
        public static void ModMapForSafe(int size, BlockType[,,] map, int[,] heightMap)
        {
            for (int x = size / 8 * 6; x <= size / 8 * 7; x++)
                for (int z = size / 8 * 6; z <= size / 8 * 7; z++)
                    for (int y = heightMap[x, z]; y <= heightMap[x, z]; y++)
                    {
                        map[x, y, z] = BlockType.WhiteSpot;
                    }
        }
    }
}
