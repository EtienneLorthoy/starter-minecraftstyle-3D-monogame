using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterMinecraftStyleWorld.Helpers
{
    public interface IOverlayDebugOutput
    {
        void WriteLine(string line);
        void WriteLine(string line, Color color);
    }
}
