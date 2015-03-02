using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl
{
    class AudioControlApplication
    {
        static int Main(string[] args)
        { 
            KeybindWatchThread kwt = new KeybindWatchThread();
            kwt.initKeybind(0x13, 0, dostuff);
            kwt.messageLoopForever();
            return 0;
        }

        public static void dostuff()
        {
            SystemSounds.Beep.Play();
        }
    }
}
