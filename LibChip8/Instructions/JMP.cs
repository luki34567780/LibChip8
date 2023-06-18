﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct JMP : IInstruction
    {
        public ushort Mask => 0x1000;

        public void Execute(CPU cpu, ushort instr)
        {
            var targetAddress = (short)(instr & 0x0FFF);
            cpu.Regs.PC = targetAddress;
        }
    }
}
