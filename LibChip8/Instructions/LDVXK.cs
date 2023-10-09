using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions;
internal class LDVXK : IInstruction
{
    // Fx0A - LD Vx, K
    public bool IsInstruction(Instruction instr)
    {
        return instr.CompareValues(0xF, -1, 0x0, 0xA);
    }

    public void Init(CPU cpu)
    {
        if (cpu != null)
            cpu.Keyboard.KeyPressed += (byte key) => pressedKey = key;
    }

    public volatile byte pressedKey = 0xFF;

    public void Execute(CPU cpu, ushort instr)
    {
        var x = (byte)((instr & 0x0F00) >> 8);
        while (pressedKey == 0xFF)
        {
            Thread.Sleep(1);
        }

        var key = pressedKey;
        pressedKey = 0xFF;
        cpu.Regs.V[x] = key;
    }
}
