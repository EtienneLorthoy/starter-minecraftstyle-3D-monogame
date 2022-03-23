using System;

namespace StarterMinecraftStyleWorld
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new StarterGame())
                game.Run();
        }
    }
}
