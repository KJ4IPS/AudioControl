using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace AudioControl
{
    class KeybindWatchThread
    {

        [DllImport("user32.dll")]
        public static extern bool GetMessage(ref MSG message, IntPtr windowHandle, uint filterMin, uint filterMax);

        private static uint WM_HOTKEY = 0x0312;
        private static uint WM_QUIT = 0x0012;

        private List<Keybind> keybinds;

        public void messageLoopForever()
        {
            bool messageReturn;
            MSG msg;
            while (messageReturn = GetMessage(ref msg, (IntPtr) null, WM_HOTKEY, WM_HOTKEY))
            {//WM_Quit can be given here, even though it was filtered out. If that happens, we should DIE!!!
                if (msg.message == WM_QUIT)
                {
                    return; //We are done
                }
                else if (msg.message == WM_HOTKEY)
                {
                    //Handle the keybinds
                }
                else
                {
                    throw new InvalidOperationException(String.Format("Bad stuff happened, got a message we were not expecting {}", msg.message));
                }
            }
        }

        public int registerKeybind(Keybind keybind)
        {
            int index = keybinds.Count;
            this.keybinds.Add(keybind);
            if (!keybind.Register())
            {
                throw new InvalidOperationException("Something bad happend while registering a keybind!");
            }
            return index;
        }

    }
}
