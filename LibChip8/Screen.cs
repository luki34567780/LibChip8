using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public class Screen
    {
        public const int Width = 64;
        public const int Height = 32;
        public byte[,] Pixels = new byte[Width, Height];
    }
}
