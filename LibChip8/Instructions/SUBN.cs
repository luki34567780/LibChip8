using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy7 - SUBN Vx, Vy
    internal struct SUBN : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x7);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            var res = (byte)(cpu.Regs.V[y] - cpu.Regs.V[x]);
            var borrowValue = (byte)(cpu.Regs.V[y] > cpu.Regs.V[x] ? 1 : 0);

            cpu.Regs.V[x] = res;
            cpu.Regs.VF = borrowValue;
        }
    }
}
