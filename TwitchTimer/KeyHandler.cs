using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TwitchTimer
{
    public enum KeyType
    {
        Pause,
        Resume,
        Reset,
        None
    }
    public class KeyHandler
    {

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;
        private KeyType type = KeyType.None;
        public KeyHandler(Keys key, Form form, KeyType type = KeyType.None)
        {
            this.key = (int)key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
            this.type = type;
        }
        public KeyHandler(int key, Form form, KeyType type = KeyType.None)
        {
            this.key = key;
            this.hWnd = form.Handle;
            id = this.GetHashCode();
            this.type = type;
        }

        public KeyType GetKeyType()
        {
            return type;
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public int GetKeyValue()
        {
            return key;
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
}
