using Microsoft.Xna.Framework;
using StarterMinecraftStyleWorld.Primitives;
using System.Collections.Generic;

namespace StarterMinecraftStyleWorld.Terrains.Testers
{
    public class TerrainCollisionTester
    {
        private List<TerrainCollisionChunk> chunks = new List<TerrainCollisionChunk>();

        public void AddHeightMap(int[,] heightMap, int shiftX, int shiftZ, int shiftY)
        {
            chunks.Add(new TerrainCollisionChunk(heightMap, shiftX, shiftZ, shiftY));
        }

        public float GetHeightToSurface(Vector3 position)
        {
            var value = float.MaxValue;
            foreach (var chunk in chunks)
            {
                var test = chunk.GetHeightToSurface(position);
                if (test != float.MaxValue) value = test;
            }
            return value;
        }

        private class TerrainCollisionChunk
        {
            private int[,] heightMap;
            private int shiftX;
            private int shiftY;
            private int shiftZ;

            public TerrainCollisionChunk(int[,] heightMap, int shiftX, int shiftZ, int shiftY)
            {
                this.heightMap = heightMap;
                this.shiftX = shiftX;
                this.shiftY = shiftY;
                this.shiftZ = shiftZ;
            }

            public float GetHeightToSurface(Vector3 position)
            {
                // Off map X
                var indexX = (int)position.X + shiftX;
                if (indexX < 0 || indexX >= heightMap.GetLength(0)) return float.MaxValue;

                // Off map Z
                var indexZ = (int)position.Z + shiftZ;
                if (indexZ < 0 || indexZ >= heightMap.GetLength(1)) return float.MaxValue;

                // On map assert below
                return position.Y + shiftY - 1 - heightMap[indexX, indexZ];
            }

            #region Debug renderer cubes
            private List<Cube> debugCubes;
            public void AddDebugCube(Game game)
            {
                debugCubes = new List<Cube>();
                for (int x = 0; x < heightMap.GetLength(0); x++)
                {
                    for (int z = 0; z < heightMap.GetLength(1); z++)
                    {
                        var YPos = heightMap[x, z];
                        var cube = new Cube(game, new Vector3(x - shiftX, YPos - shiftY + 1, z - shiftZ), 0.5f);
                        game.Components.Add(cube);
                        debugCubes.Add(cube);
                    }
                }
            }
            #endregion
        }
    }
}
