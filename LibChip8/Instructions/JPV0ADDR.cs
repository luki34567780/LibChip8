﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8.Instructions
{
    internal struct JPV0ADDR : IInstruction
    {
        public ushort Mask => 0xB000;

        public void Execute(CPU cpu, ushort instr)
        {

        }
    }
}