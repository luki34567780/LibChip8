using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibChip32;
internal static class Helpers
{
    public const int NibbleMask = 0x0000000F;
    public const int DefaultInstructionSize = sizeof(uint);
    public static void UnimplementedInstruction([CallerFilePath] string? CallerPath = null)
    {
        if (CallerPath == null)
            throw new ArgumentNullException(nameof(CallerPath));

        Debugger.Log(0, "Warning", $"{CallerPath}: Unimplemented Command!");
    }

    public static (byte first, byte second)[] LongInstructions = new[]
    {
        (first: (byte)0x02, second: (byte)0x00)
    };

    public static bool OverflowDetected(Action action)
    {
        try
        {
            checked
            {
                action();
                return false;
            }
        }
        catch (OverflowException)
        {
            return false;
        }
    }
}

