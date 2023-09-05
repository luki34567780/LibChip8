using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions;
internal class SKNP : IInstruction
{
    public bool IsInstruction(Instruction instr)
    {
        return instr.CompareValues(0xE, -1, 0xA, 0x1);
    }

    public void Execute(CPU cpu, ushort instr)
    {
        var x = (byte)((instr & 0x0F00) >> 8);
        if (!cpu.Keyboard.Keys[cpu.Regs.V[x]])
        {
            cpu.Regs.PC += 2;
        }
    }
}
