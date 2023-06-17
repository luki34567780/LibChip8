using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    internal class Timers
    {
        public System.Timers.Timer Timer { get; set; }
        // wait timer
        public short WT { get; set; }
        // sound timer
        public short ST { get; set; }

        public Timers()
        {
            // tick rate of 60 hz
            Timer = new System.Timers.Timer(1000 / 60);
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (WT > 0)
                WT--;
            
            if (ST > 0) 
                ST--;
        }
    }
}
