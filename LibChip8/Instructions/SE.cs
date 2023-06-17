﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct SE_Vx_Byte : IInstruction
    {
        public short Mask => 0x3000;

        public void Execute(CPU cpu, short instr)
        {
            byte registerIndex = (byte)((instr & 0x0F00) >> 8);
            byte value = (byte)(instr & 0x00FF);

            if (cpu.Regs.GetRegByNum(registerIndex) == value)
            {
                // Skip the next instruction
                cpu.Regs.PC += 2;
            }
        }
    }
}