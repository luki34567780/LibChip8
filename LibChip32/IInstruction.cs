using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public interface IInstruction
    {
        public bool IsInstruction(Instruction instr);
        public void Execute(CPU cpu, ulong instr);
    }
}
