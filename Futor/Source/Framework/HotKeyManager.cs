using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Futor
{
    public class HotKeyManager : IDisposable
    {
        class MessageWindow : ContainerControl
        {
            const int _kWmHotKey = 0x0312;

            readonly Func<Message, bool> _callback;

            public MessageWindow(Func<Message, bool> callback)
            {
                _callback = callback;
            }

            protected override void WndProc(ref Message m)
            {
                bool handled = false;

                if (m.Msg == _kWmHotKey)
                    handled = _callback(m);

                if (!handled)
                    base.WndProc(ref m);
            }
        }

        [Flags]
        public enum Modifiers
        {
            None = 0,

            Alt = 0x0001,
            Ctrl = 0x0002,
            Shift = 0x0004,
            Win = 0x0008
        }

        public struct HotKey
        {
            const char _kSeparator = '+';
            const string _kWinString = "Win";

            public Modifiers Modifiers { get; private set; }
            public Keys Key { get; private set; }

            public static HotKey? StringToHotKey(string hotKeyString)
            {
                var hotKey = new HotKey();

                var parts = hotKeyString.Split(new[] {_kSeparator}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

                for (int i = parts.Count - 1; i >= 0; i--)
                {
                    var part = parts[i];

                    var hasModifier = false;

                    if (part == Keys.Control.ToString())
                    {
                        hotKey.Modifiers |= Modifiers.Ctrl;
                        hasModifier = true;
                    }
                    else if (part.Trim() == Keys.Alt.ToString())
                    {
                        hotKey.Modifiers |= Modifiers.Alt;
                        hasModifier = true;
                    }
                    else if (part.Trim() == Keys.Shift.ToString())
                    {
                        hotKey.Modifiers |= Modifiers.Shift;
                        hasModifier = true;
                    }
                    else if (part.Trim() == _kWinString)
                    {
                        hotKey.Modifiers |= Modifiers.Win;
                        hasModifier = true;
                    }

                    if (hasModifier)
                        parts.RemoveAt(i);
                }

                if (parts.Count != 1)
                    return null;

                var keyconverter = new KeysConverter();

                try
                {
                    hotKey.Key = (Keys)keyconverter.ConvertFrom(parts.First());
                }
                catch
                {
                    return null;
                }

                return hotKey;
            }

            public override string ToString()
            {
                var parts = new List<string> {Key.ToString()};

                if ((Modifiers & Modifiers.Win) == Modifiers.Win)
                    parts.Insert(0, _kWinString);

                if ((Modifiers & Modifiers.Shift) == Modifiers.Shift)
                    parts.Insert(0, Keys.Shift.ToString());

                if ((Modifiers & Modifiers.Alt) == Modifiers.Alt)
                    parts.Insert(0, Keys.Alt.ToString());

                if ((Modifiers & Modifiers.Ctrl) == Modifiers.Ctrl)
                    parts.Insert(0, Keys.Control.ToString());

                return string.Join($" {_kSeparator} ", parts);
            }
        }

        class HotKeyHandler
        {
            public HotKey HotKey;
            public Action Callback;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, Modifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        readonly MessageWindow _messageWindow;

        public static HotKeyManager Instance { get; } = new HotKeyManager();

        int _id;
        readonly Dictionary<int, HotKeyHandler> _handlers = new Dictionary<int, HotKeyHandler>();

        HotKeyManager()
        {
            _messageWindow = new MessageWindow(ProcessHotKeyMessage);
        }

        IntPtr Handle => _messageWindow.Handle;

        public bool IsHotKeyRegistered(string hotKeyString)
        {
            var hotKey = HotKey.StringToHotKey(hotKeyString);

            return hotKey.HasValue
                   && IsHotKeyRegistered(hotKey.Value);
        }

        public bool IsHotKeyRegistered(HotKey hotKey)
        {
            _id++;
            if (!RegisterHotKey(Handle, _id, hotKey.Modifiers, hotKey.Key))
                return false;

            UnregisterHotKey(Handle, _id);

            _id--;

            return true;
        }

        public bool RegisterHotKey(string hotKeyString, Action callback)
        {
            var hotKey = HotKey.StringToHotKey(hotKeyString);

            return hotKey.HasValue
                   && RegisterHotKey(hotKey.Value, callback);
        }

        public bool RegisterHotKey(HotKey hotKey, Action callback)
        {
            return TryRegisterHotKey(hotKey, callback) == 0;
        }

        uint TryRegisterHotKey(HotKey hotKey, Action callback)
        {
            _id++;
            if (!RegisterHotKey(Handle, _id, hotKey.Modifiers, hotKey.Key))
                return GetLastError();

            _handlers.Add(_id, new HotKeyHandler {HotKey = hotKey, Callback = callback});

            return 0;
        }

        public bool UnregisterHotKey(string hotKeyString)
        {
            var hotKey = HotKey.StringToHotKey(hotKeyString);

            return hotKey.HasValue
                   && UnregisterHotKey(hotKey.Value);
        }

        public bool UnregisterHotKey(HotKey hotKey)
        {
            return TryUnregisterHotKey(hotKey) == 0;
        }

        uint TryUnregisterHotKey(HotKey hotKey)
        {
            var id = _handlers.FirstOrDefault(x => x.Value.HotKey.Equals(hotKey)).Key;
            if (id == 0)
                return 1;

            _handlers.Remove(id);

            return UnregisterHotKey(Handle, id) ? 0 : GetLastError();
        }

        bool ProcessHotKeyMessage(Message message)
        {
            var id = message.WParam.ToInt32();

            if (!_handlers.ContainsKey(id))
                throw new KeyNotFoundException();

            _handlers[id].Callback();

            return true;
        }

        public void Dispose()
        {
            foreach (var handler in _handlers.Values.ToList())
                TryUnregisterHotKey(handler.HotKey);

            _handlers.Clear();
        }
    }
}
