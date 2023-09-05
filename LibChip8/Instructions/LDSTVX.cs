using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions;
internal class LDSTVX : IInstruction
{
    public bool IsInstruction(Instruction instr)
    {
        return instr.CompareValues(0xF, -1, 0x1, 0x8);
    }

    public void Execute(CPU cpu, ushort instr)
    {
        var x = (instr & 0x0F00) >> 8;
        cpu.Timers.ST = cpu.Regs.V[x];
    }
}
