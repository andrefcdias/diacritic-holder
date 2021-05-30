using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static PInvoke.User32;
using static PInvoke.Kernel32;

namespace DiacriticHolder
{
    class KeyListener : IDisposable
    {
        private SafeHookHandle _keyboardHookHandle;
        private IntPtr _moduleHandleId;
        internal delegate IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam);
        //private WindowsHookDelegate _keyboardHookDelegate;
        private Action<Key> _keyPressedCallback, _keyReleasedCallback;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //internal static extern IntPtr GetModuleHandle(string lpModuleName);

        private int KeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            WindowMessage windowMessage = (WindowMessage)wParam.ToInt32();
            if (nCode >= 0 && (windowMessage == WindowMessage.WM_KEYDOWN || windowMessage == WindowMessage.WM_KEYUP))
            {
                int keyIntValue = Marshal.ReadInt32(lParam);
                bool isValidLetter = Enum.IsDefined(typeof(Key), keyIntValue);

                if(isValidLetter)
                {
                    switch(windowMessage)
                    {
                        case WindowMessage.WM_KEYDOWN:
                            _keyPressedCallback.Invoke((Key)keyIntValue);
                            break;
                        case WindowMessage.WM_KEYUP:
                            _keyReleasedCallback.Invoke((Key)keyIntValue);
                            //return CallNextHookEx(_keyboardHookHandle.DangerousGetHandle(), nCode, wParam, lParam);
                            break;
                    }
                }
            }

            //return CallNextHookEx(_keyboardHookHandle.DangerousGetHandle(), nCode, wParam, lParam);
            return 0;
        }

        public KeyListener()
        {
            _moduleHandleId = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);
        }

        public void RegisterHook(Action<Key> keyPressedCallback, Action<Key> keyReleasedCallback)
        {
            //_keyboardHookDelegate = KeyboardCallback;
            //_keyboardHookHandle = SetWindowsHookEx(WindowsHookType.WH_KEYBOARD_LL, _keyboardHookDelegate, _moduleHandleId, 0);
            _keyPressedCallback = keyPressedCallback;
            _keyReleasedCallback = keyReleasedCallback;
            _keyboardHookHandle = SetWindowsHookEx(WindowsHookType.WH_KEYBOARD_LL, KeyboardCallback, _moduleHandleId, 0);
        }

        public void Dispose()
        {
            if (!_keyboardHookHandle.IsClosed && !_keyboardHookHandle.IsInvalid)
            {
                UnhookWindowsHookEx(_keyboardHookHandle.DangerousGetHandle());
            }
        }
    }
}
