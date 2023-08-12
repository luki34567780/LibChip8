using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LibChip8
{
    public unsafe class Memory : IList<byte>
    {
        public const int MemorySizeConst = 0x7FFFFFFF;
        public const int DisplaySizeConst = MemorySizeConst - 0x7FFFF82F;

        public Memory()
        {
            var buf = new byte[MemorySizeConst];
            Handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
            var ptr = (byte*)Handle.AddrOfPinnedObject();

            MemPtr = ptr;
            Mem = buf;
            InterpreterInfo = (InterpreterInfo*)ptr;
            StackPtr = MemorySize - DisplaySize;
            Display = Mem.AsMemory(StackPtr, DisplaySize);
            Stack = (StackItem*)MemPtr + MemorySize - DisplaySize - 1;
        }

        public int DisplaySize => DisplaySizeConst;
        public int MemorySize => MemorySizeConst;
        public GCHandle Handle { get; private set; }
        public byte* MemPtr { get; private set; }
        public byte[] Mem { get; private set; }
        public StackItem* Stack { get; private set; }
        public int StackPtr { get; private set; }
        public Memory<byte> Display { get; set; }
        public InterpreterInfo* InterpreterInfo { get; private set; }

        public int Count => ((ICollection<byte>)Mem).Count;

        public bool IsReadOnly => ((ICollection<byte>)Mem).IsReadOnly;

        public void AddToStack(StackItem item)
        {
            StackPtr += 4;
            Stack[-StackPtr] = item;
        }

        public StackItem RemoveFromStack()
        {
            var item = Stack[-StackPtr];
            StackPtr -= 4;

            return item;
        }

        public int IndexOf(byte item) => ((IList<byte>)Mem).IndexOf(item);
        public void Insert(int index, byte item) => ((IList<byte>)Mem).Insert(index, item);
        public void RemoveAt(int index) => ((IList<byte>)Mem).RemoveAt(index);
        public void Add(byte item) => ((ICollection<byte>)Mem).Add(item);
        public void Clear() => ((ICollection<byte>)Mem).Clear();
        public bool Contains(byte item) => ((ICollection<byte>)Mem).Contains(item);
        public void CopyTo(byte[] array, int arrayIndex) => ((ICollection<byte>)Mem).CopyTo(array, arrayIndex);
        public bool Remove(byte item) => ((ICollection<byte>)Mem).Remove(item);
        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)Mem).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Mem.GetEnumerator();

        public Span<byte> AsSpan(int count) => Mem.AsSpan(count);
        public Span<byte> AsSpan() => Mem.AsSpan();
        public byte this[int index]
        {
            get => Mem[index];
            set => Mem[index] = value;
        }

        public byte this[uint index]
        {
            get => Mem[index];
            set => Mem[index] = value;
        }
    }
}
