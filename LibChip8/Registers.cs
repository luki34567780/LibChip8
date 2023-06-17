namespace LibChip8
{
    public class Registers
    {
        public short V0 { get; set; }
        public short V1 { get; set; }
        public short V2 { get; set; }
        public short V3 { get; set; }
        public short V4 { get; set; }
        public short V5 { get; set; }
        public short V6 { get; set; }
        public short V7 { get; set; }
        public short V8 { get; set; }
        public short V9 { get; set; }
        public short VA { get; set; }
        public short VB { get; set; }
        public short VC { get; set; }
        public short VD { get; set; }
        public short VE { get; set; }
        public short VF { get; set; }
        public short I { get; set; }
        public short PC { get; set; }
        public byte SP { get; set; }

        public short GetRegByNum(int num)
        {
            switch (num)
            {
                case 0: return V0;
                case 1: return V1;
                case 2: return V2;
                case 3: return V3;
                case 4: return V4;
                case 5: return V5;
                case 6: return V6;
                case 7: return V7;
                case 8: return V8;
                case 9: return V9;
                case 10: return VA;
                case 11: return VB;
                case 12: return VC;
                case 13: return VD;
                case 14: return VE;
                case 15: return VF;
                default: throw new Exception("Out of Range!");
            }
        }

    }
}