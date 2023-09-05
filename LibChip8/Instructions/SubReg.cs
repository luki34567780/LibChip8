using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    // 8xy5 - SUB Vx, Vy
    internal struct SubReg : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0x8, -1, -1, 0x5);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            var res = (byte)(cpu.Regs.V[x] - cpu.Regs.V[y]);
            var borrowValue = (byte)(cpu.Regs.V[x] > cpu.Regs.V[y] ? 1 : 0);

            cpu.Regs.V[x] = res;
            cpu.Regs.VF = borrowValue;
        }
    }
}
