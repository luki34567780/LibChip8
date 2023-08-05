using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public readonly struct Instruction
    {
        /// <summary>
        /// Compares if nibble are equal. Set to -1 to ignore a nibble;
        /// </summary>
        /// <param name="bytes">bytes to evaluate against</param>
        public bool CompareValues(sbyte[] bytes)
        {
            for (int i = 0; i < sizeof(ulong) * 2; i++)
            {
                if (bytes[i] == -1)
                    continue;

                byte nibble;
                var b = this[i / 2];
                
                if (i % 2 == 0)
                {
                    nibble = (byte)(b >> 4);
                }
                else
                {
                    nibble = (byte)(b & 0x0F);
                }

                if (nibble != bytes[i])
                    return false;

            }
            return true;
        }

        private readonly ulong _raw;

        public ulong Raw => _raw;

        public unsafe byte this[int index]
        {
            get
            {
                fixed (ulong* ptr = &_raw)
                {
                    return ((byte*)ptr)[index];
                }
            }
        }

        public Instruction(ulong raw)
        {
            _raw = raw;
        }

        public static implicit operator Instruction(ulong raw) => new(raw);
    }
}
