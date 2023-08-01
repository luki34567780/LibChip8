using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xyE - SHL Vx {, Vy}
    internal struct SHL : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            instr.CompareValues(0x8, -1, -1, 0xE);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;
            cpu.Regs.V[0xF] = (byte)(cpu.Regs.V[x] >> 7);
            cpu.Regs.V[x] <<= 1;
        }
    }
}
