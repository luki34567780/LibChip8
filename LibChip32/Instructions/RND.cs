using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // Cxkk - RND Vx, byte
    internal struct RND : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xC, -1, -1, -1);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;
            var kk = (byte)(instr & 0x00FF);
            cpu.Regs.V[x] = (byte)(Random.Shared.Next(0, 256) & kk);
        }
    }
}
