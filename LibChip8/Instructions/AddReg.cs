using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct AddReg : IInstruction
    {
        public ushort Mask => 0x8004;

        public void Execute(CPU cpu, ushort instr)
        {
            var x = (byte)((instr & 0x0F00) >> 8);
            var y = (byte)((instr & 0x00F0) >> 4);

            var sum = cpu.Regs.V[x] + cpu.Regs.V[y];
            cpu.Regs.VF = (byte)(sum > 0xFF ? 1 : 0);
            cpu.Regs.V[x] = (byte)(sum & 0xFF);
        }
    }
}
