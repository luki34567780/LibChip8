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
        public InstructionDecoder()
        {
            var types = typeof(InstructionDecoder).Assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IInstruction)))
                {
                    var instance = (IInstruction)Activator.CreateInstance(type);
                    _instructionInstances.Add(instance);
                    RuntimeHelpers.PrepareMethod(type.GetMethod("Execute").MethodHandle);
                }
            }
        }

        public IInstruction DecodeInstruction(ulong instructionBytes)
        {
            var instr = new Instruction(instructionBytes);

            foreach (var instruction in _instructionInstances)
            {
                if (instruction.InstructionClassByte == instr.InstructionClassByte &&
                    instruction.InstructionIdentByte == instr.InstructionIdentByte)
                {
                    Console.WriteLine($"Executing Instruction: {instruction}");
                    return instruction;
                }
            }

            throw new Exception("Invalid Instruction!");
        }
    }
}
