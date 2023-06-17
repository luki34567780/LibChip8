using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    internal class CPU
    {
        public Registers Regs { get; } = new();
        public Stack[] Stack { get; } = new Stack[16];
        public Screen Screen { get;  } = new();
        public byte[] Memory { get;  } = new byte[4096];

        public void DoInstruction(short instr)
        {
            switch (instr)
            {

            }
        }
    }
}
