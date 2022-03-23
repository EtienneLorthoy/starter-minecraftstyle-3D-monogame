using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarterMinecraftStyleWorld.Terrains.Testers;

namespace StarterMinecraftStyleWorld.Terrains
{
    public class Chunk : DrawableGameComponent
    {
        private readonly static float frequency = 1f;
        private readonly static float amplitude = 128.0f;
        private readonly static float baseHeight = 9.0f;

        // Map data
        private readonly Vector3 position;
        private readonly BlockType[,,] map;
        private readonly int[,] heightMap;

        // Rendering
        private LinkedBlockMesh[,,] blockMeshes;
        private static Texture2D texture;
        private static BasicEffect effect;
        private VertexPositionNormalTexture[] meshVertices;

        public static readonly int Size = 64;
        public static readonly int Height = 16;
        public static Vector3 offset0, offset1, offset2;

        public Chunk(Game game, Vector3 position) : base(game)
        {
            this.position = position;
            this.map = new BlockType[Size, Height, Size];
            this.heightMap = new int[Size, Size];

            GenerateTerrain();
            BuildBlocks();
        }

        protected override void LoadContent()
        {
            // Texture
            if (texture == null) texture = this.Game.Content.Load<Texture2D>("terrain");

            // Shader
            if (effect == null)
            {
                effect = new BasicEffect(Game.GraphicsDevice);
                effect.World = Matrix.Identity;
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

                effect.TextureEnabled = true;
                effect.Texture = texture;           
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            effect.View = (this.Game as StarterGame).Camera.View;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var debug = (this.Game as StarterGame).DebugOverlay;

            // Just in time mesh creation
            if (meshVertices == null)
            {
                var meshVerticesList = new List<VertexPositionNormalTexture>();
                foreach (var blockMesh in blockMeshes)
                {
                    if (blockMesh != null) meshVerticesList.AddRange(blockMesh.ConstructMesh());
                }
                meshVertices = meshVerticesList.ToArray();
            }

            // Actual drawing
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                meshVertices, 0, meshVertices.Length / 3,
                VertexPositionNormalTexture.VertexDeclaration);

                debug.AddToTriangleCount(meshVertices.Length / 3);
            }

            base.Draw(gameTime);
        }

        private void GenerateTerrain()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int z = 0; z < Size; z++)
                {
                    var value = CalculateHeightMapValue(new Vector3(x, 0, z) + position);
                    //if (value < )
                        heightMap[x, z] = value;
                }
            }

            ChunkModder.ModHeightMapForWalls(Size, heightMap);
            ChunkModder.ModHeightMapForWater(Size, heightMap);
            ChunkModder.ModHeightMapForSafe(Size, heightMap);

            for (int x = 0; x < Size; x++)
            {
                for (int z = 0; z < Size; z++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        BlockType id = CalculateBlockType(new Vector3(x, y, z));
                        map[x, y, z] = id;
                    }
                }
            }

            ChunkModder.ModMapForWalls(Size, map);
            ChunkModder.ModMapForWater(Size, map, heightMap);
            ChunkModder.ModMapForSafe(Size, map, heightMap);
        }

        private void BuildBlocks()
        {
            blockMeshes = new LinkedBlockMesh[Height,Size,Size];

            // Creating all the linkedblock
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    for (int z = 0; z < Size; z++)
                    {
                        BlockType id = map[x, y, z];
                        if (id != BlockType.Air)
                        {
                            LinkedBlockMesh result = null;
                            var textureOffsets = BlockTextureOffsets.GetTextureOffset(id);
                            if (id != BlockType.Air) result = new LinkedBlockMesh(new Vector3(x, y, z) + position, textureOffsets);
                            
                            if (result != null) blockMeshes[y, x, z] = result;
                        }
                    }
                }
            }

            // Link mesh between them to spare the draw from those which are hidden
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    for (int z = 0; z < Size; z++)
                    {
                        if (blockMeshes[y, x, z] != null)
                        {
                            // Down
                            if (y > 0) blockMeshes[y, x, z].Down = blockMeshes[y - 1, x, z];
                            // Up
                            if (y < Height - 1) blockMeshes[y, x, z].Up = blockMeshes[y + 1, x, z];
                            // Back
                            if (z > 0) blockMeshes[y, x, z].Back = blockMeshes[y, x, z - 1];
                            // Front
                            if (z < Size - 1) blockMeshes[y, x, z].Front = blockMeshes[y, x, z + 1];
                            // Left
                            if (x > 0) blockMeshes[y, x, z].Left = blockMeshes[y, x - 1, z];
                            // Back
                            if (x < Size - 1) blockMeshes[y, x, z].Right = blockMeshes[y, x + 1, z];
                        }
                    }
                }
            }
        }

        private BlockType CalculateBlockType(Vector3 currentPosition)
        {
            if (currentPosition.Y < 0 || currentPosition.Y > Height - 1)
            {
                return 0;
            }

            var heightMapValue = heightMap[(int)currentPosition.X, (int)currentPosition.Z];

            if (currentPosition.Y < 3)
            {
                return BlockType.Bedrock;
            }
            else if (currentPosition.Y == heightMapValue)
            {
                return BlockType.Grass;
            }
            else if (currentPosition.Y < heightMapValue && currentPosition.Y > heightMapValue - 5)
            {
                return BlockType.Dirt;
            }
            else if (currentPosition.Y > heightMapValue)
            {
                return BlockType.Air;
            }
            else
            {
                return BlockType.Stone;
            }
        }

        private static int CalculateHeightMapValue(Vector3 position)
        {
            FastNoiseLite noiser = new FastNoiseLite();

            position.X = Math.Abs(position.X);
            position.Z = Math.Abs(position.Z);
            float x0 = (position.X + offset0.X) * frequency;
            float z0 = (position.Z + offset0.Z) * frequency;

            float x1 = (position.X + offset1.X) * frequency * 2f;
            float z1 = (position.Z + offset1.Z) * frequency * 2f;

            float x2 = (position.X + offset2.X) * frequency * 4f;
            float z2 = (position.Z + offset2.Z) * frequency * 4f;

            float noise0 = noiser.GetNoise(x0, z0) * amplitude / 16;
            float noise1 = noiser.GetNoise(x1, z1) * amplitude / 32f;
            float noise2 = noiser.GetNoise(x2, z2) * amplitude / 64f;

            float noise = noise0 + noise1 + noise2;

            var result = (int)Math.Floor(noise + baseHeight);

            if (result < 3) result = 3;
            if (result >= Height) result = Height-1;

            return result;
        }

        internal static void AddToTerrainCollisionTester(Chunk chunk, TerrainCollisionTester terrainCollisionTester)
        {
            terrainCollisionTester.AddHeightMap(chunk.heightMap, (int)-chunk.position.X, (int)-chunk.position.Z, (int)-chunk.position.Y);
        }

        internal static void AddToTerrainOnRessourceTester(Chunk chunk, TerrainOnRessourceTester terrainOnRessourceTester)
        {
            terrainOnRessourceTester.AddMapChunk(chunk.map, (int)-chunk.position.X, (int)-chunk.position.Z, (int)-chunk.position.Y);
        }
    }
}