using PInvoke;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DiacriticHolder.Types
{
    public static class Win32
    {
        public enum NOTIFYICONMESSAGE : int
        {
            NIM_ADD = 0x00000000,
            NIM_MODIFY = 0x00000001,
            NIM_DELETE = 0x00000002,
            NIM_SETFOCUS = 0x00000003,
            NIM_SETVERSION = 0x00000004,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GUITHREADINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hwndActive;
            public IntPtr hwndFocus;
            public IntPtr hwndCapture;
            public IntPtr hwndMenuOwner;
            public IntPtr hwndMoveSize;
            public IntPtr hwndCaret;
            public Rectangle rcCaret;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NOTIFYICONDATA
        {
            public int cbSize; // DWORD
            public IntPtr hWnd; // HWND
            public int uID; // UINT
            public NOTIFYICONMESSAGE uFlags; // UINT
            public int uCallbackMessage; // UINT
            public IntPtr hIcon; // HICON
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szTip; // char[128]
            public int dwState; // DWORD
            public int dwStateMask; // DWORD
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szInfo; // char[256]
            public int uTimeoutOrVersion; // UINT
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szInfoTitle; // char[64]
            public int dwInfoFlags; // DWORD
            //GUID guidItem; > IE 6
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCaretPos([In] ref POINT lpPoint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGUIThreadInfo(uint idThread, [In] ref GUITHREADINFO lpgui);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool Shell_NotifyIcon(uint dwMessage, [In] ref NOTIFYICONDATA pnid);
    }
}
