using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LibChip8
{
    public class Timers
    {
        public System.Timers.Timer Timer { get; set; }
        // wait timer
        public byte DT { get; set; }
        // sound timer
        public byte ST { get; set; }

        public const double TimerFrequency = 1000.0 / 60.0;
        public const double SixtyHz = 1000.0 / 60.0;

        public Timers()
        {
            // tick rate of 60 hz
            Timer = new System.Timers.Timer(TimerFrequency);
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            byte decrementAmount = (byte)Math.Round(TimerFrequency / SixtyHz);

            if (DT > decrementAmount)
            {
                DT -= decrementAmount;
            }
            // if one or two cycles still remain we just ignore them
            else if (DT > 0)
            {
                DT = 0;
            }

            if (ST > decrementAmount)
            {
                ST -= decrementAmount;
                Console.Beep(1000, (int)TimerFrequency + 1);
            }
            // if one or two cycles still remain we just ignore them
            else if (ST > 0)
            {
                ST = 0;
            }
        }
    }
}
