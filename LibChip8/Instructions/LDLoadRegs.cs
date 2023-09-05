using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal class LDLoadRegs : IInstruction
    {
        public bool IsInstruction(Instruction instr)
        {
            return instr.CompareValues(0xF, -1, 0x6, 0x5);
        }

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (instr & 0x0F00) >> 8;

            if (cpu.QuirkConfig.LoadStoreBehaviour == LoadStoreBehaviour.Increment)
            {
                for (int i = 0; i <= x; i++)
                {
                    cpu.Regs.V[i] = cpu.Memory[cpu.Regs.I++];
                }
            }
            else if (cpu.QuirkConfig.LoadStoreBehaviour == LoadStoreBehaviour.NoIncrement)
            {
                for (int i = 0; i <= x; i++)
                {
                    cpu.Regs.V[i] = cpu.Memory[cpu.Regs.I + i];
                }
            }
            else
            {
                throw new Exception("Not implemented LoadStoreBehaviour!");
            }
        }
    }
}
