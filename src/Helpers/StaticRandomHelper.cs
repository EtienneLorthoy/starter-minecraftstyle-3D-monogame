using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterMinecraftStyleWorld.Helpers
{
    public static class StaticRandomHelper
    {
        public static Random Random = new Random(DateTime.Now.Millisecond);
    }
}
