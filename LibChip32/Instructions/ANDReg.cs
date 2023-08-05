using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct ANDReg : IInstruction
    {
        public bool IsInstruction(Instruction instruction)
        {
            return instruction[0] == 0x8 && instruction[3] == 0x2;
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            cpu.Regs.V[x] &= cpu.Regs.V[y];
        }
    }
}
