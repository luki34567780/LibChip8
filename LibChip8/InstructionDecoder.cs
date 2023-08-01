using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    internal class InstructionDecoder
    {
        private List<IInstruction> _instructionInstances = new();

        public InstructionDecoder()
        {
            var types = typeof(InstructionDecoder).Assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IInstruction)))
                {
                    _instructionInstances.Add((IInstruction)Activator.CreateInstance(type));
                }
            }
        }

        private static unsafe T UnsafeCast<T>(object obj) where T : unmanaged
        {
            return *(T*)&obj;
        }

        public IInstruction DecodeInstruction(ushort instructionBytes)
        {
            var instructionStruct = UnsafeCast<Instruction>(instructionBytes);

            foreach (var instructionInstance in _instructionInstances)
            {
                if (instructionInstance.IsInstruction(instructionStruct))
                {
                    return instructionInstance;
                }
            }

            throw new Exception("Invalid Instruction!");
        }
    }
}
