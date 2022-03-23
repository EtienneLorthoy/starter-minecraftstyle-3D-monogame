using Microsoft.Xna.Framework;
using StarterMinecraftStyleWorld.Terrains.Testers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarterMinecraftStyleWorld.Terrains
{
    public class World : GameComponent
    {
        public TerrainCollisionTester TerrainCollisionTester { get; private set; }
        public TerrainOnRessourceTester TerrainOnRessourceTester { get; private set; }

        private static int Size = 4;

        // Fix this to generate the same world every time
        private int seed = DateTime.UtcNow.Second;

        private Random random;

        public World(Game game) : base(game) 
        {
            this.TerrainCollisionTester = new TerrainCollisionTester();
            this.TerrainOnRessourceTester = new TerrainOnRessourceTester();
        }

        public override void Initialize()
        {
            Debug.WriteLine("Generating world");

            random = new Random(seed);
            Chunk.offset0 = new Vector3(random.Next(1000) * 1f, 0, random.Next(1000) * 1f);
            Chunk.offset1 = new Vector3(random.Next(1000) * 1f, 0, random.Next(1000) * 1f);
            Chunk.offset2 = new Vector3(random.Next(1000) * 1f, 0, random.Next(1000) * 1f);

            for (int x = - (World.Size * Chunk.Size / 2); x < World.Size * Chunk.Size / 2; x+= Chunk.Size)
            {
                for (int z = -(World.Size * Chunk.Size / 2); z < World.Size * Chunk.Size / 2; z += Chunk.Size)
                {
                    var chunkPosition = new Vector3(x, 0f, z);
                    chunkPosition.Y = -(Chunk.Height / 4);

                    var chunk = new Chunk(this.Game, chunkPosition);
                    Chunk.AddToTerrainCollisionTester(chunk, TerrainCollisionTester);
                    Chunk.AddToTerrainOnRessourceTester(chunk, TerrainOnRessourceTester);
                    this.Game.Components.Add(chunk);
                }
            }

            Debug.WriteLine("World generated");

            base.Initialize();
        }
    }
}