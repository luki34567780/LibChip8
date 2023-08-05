using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy6 - SHR Vx {, Vy}
    internal struct SHR : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x6);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);

            cpu.Regs.VF = (byte)(cpu.Regs.V[x] & 0x01);
            cpu.Regs.V[x] >>= 1;
        }
    }
}
