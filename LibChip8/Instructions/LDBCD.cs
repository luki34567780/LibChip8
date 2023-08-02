using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal class LDBCD : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xF, -1, 0x3, 0x3);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            int value = cpu.Regs.V[(instr & 0x0F00) >> 8];

            int ones = value % 10;
            int tens = (value / 10) % 10;
            int hundreds = (value / 100) % 10;

            cpu.Memory[cpu.Regs.I] = (byte)hundreds;
            cpu.Memory[cpu.Regs.I + 1] = (byte)tens;
            cpu.Memory[cpu.Regs.I + 2] = (byte)ones;
        }
    }
}
