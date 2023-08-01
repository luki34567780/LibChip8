using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    internal readonly struct Instruction
    {
        /// <summary>
        /// Compares if nibble are equal. Set to -1 to ignore a nibble;
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="fourth"></param>
        public bool CompareValues(short first, short second, short third, short fourth)
        {
            bool invertedResult = false;

            invertedResult = (first == Nibble0 || first == -1) || invertedResult;
            invertedResult = (second == Nibble1 || second == -1) || invertedResult;
            invertedResult = (third == Nibble2 || third == -1) || invertedResult;
            invertedResult = (fourth == Nibble3 || fourth == -1) || invertedResult;

            return !invertedResult;
        }

        public ushort Raw { get; }
        public byte Nibble0 => (byte)(Raw & 0x000F);
        public byte Nibble1 => (byte)((Raw & 0x00F0) >> 4);
        public byte Nibble2 => (byte)((Raw & 0x0F00) >> 8);
        public byte Nibble3 => (byte)((Raw & 0xF000) >> 12);

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Nibble0;
                    case 1:
                        return Nibble1;
                    case 2:
                        return Nibble2;
                    case 3:
                        return Nibble3;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public Instruction(ushort raw)
        {
            Raw = raw;
        }

        public static implicit operator Instruction(ushort raw) => new(raw);
    }
}
