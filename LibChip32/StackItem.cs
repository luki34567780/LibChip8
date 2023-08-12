using System.Runtime.InteropServices;

namespace LibChip8
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StackItem
    {
        public StackItem(uint value) => Value = value;
        public uint Value { get; set; }

        public static implicit operator uint(StackItem item)
        {
            return item.Value;
        }

        public static implicit operator StackItem(uint item)
        {
            return new StackItem(item);
        }

        public static implicit operator int(StackItem item)
        {
            return unchecked((int)item.Value);
        }

        public static implicit operator StackItem(int item)
        {
            return new StackItem(unchecked((uint)item));
        }
    }
}
