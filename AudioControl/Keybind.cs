using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioControl
{
    class Keybind : IDisposable
    {

        private bool registered = false;

        //Parameters used for registering the hotkey
        private int id;
        private uint fsModifiers;
        private uint vk;

        public delegate void KeypressHandler();
        private KeypressHandler handler;

        //This is used to register a keybind with windows
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        //This unregisters a keybind with windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public Keybind(int id, uint fsModifiers, uint vk, KeypressHandler handler)
        {
            this.id = id;
            this.fsModifiers = fsModifiers;
            this.vk = vk;
            this.handler = handler;
        }

        public bool Register()
        {
            return (this.registered = RegisterHotKey((IntPtr)null, id, fsModifiers, vk));
        }

        public bool Unregister()
        {
            return UnregisterHotKey((IntPtr) null, id);
        }

        public void Dispose()
        {
            if (registered)
                Unregister();
        }

        public int getID(){
            return id;
        }

        public void handleKeypress(){
            handler();
        }


    }
}
