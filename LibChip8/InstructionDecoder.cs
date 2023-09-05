using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public class InstructionDecoder
    {
        private List<IInstruction> _instructionInstances = new();
        private Dictionary<IInstruction, int> CallCounts = new();
        public InstructionDecoder(CPU cpu)
        {
            var types = typeof(InstructionDecoder).Assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IInstruction)))
                {
                    var instance = (IInstruction)Activator.CreateInstance(type);
                    instance.Init(cpu);
                    _instructionInstances.Add(instance);
                    RuntimeHelpers.PrepareMethod(type.GetMethod("Execute").MethodHandle);
                }
            }
        }

        private static unsafe T UnsafeCast<T>(object obj) where T : unmanaged
        {
            return *(T*)&obj;
        }

        public IInstruction DecodeInstruction(ushort instructionBytes)
        {
            var instructionStruct = new Instruction(instructionBytes);

            foreach (var instructionInstance in _instructionInstances)
            {
                if (instructionInstance.IsInstruction(instructionStruct))
                {
                    //Console.WriteLine($"Executing Instruction: {instructionInstance}");
                    //CallCounts[instructionInstance]++;
                    return instructionInstance;
                }
            }

            throw new Exception("Invalid Instruction!");
        }
    }
}
