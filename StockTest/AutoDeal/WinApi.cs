using System;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoDeal
{
    public static class WinAPI
    {

        //设置文本内容的消息

        private const int WM_SETTEXT = 0x000C;

        //鼠标点击消息
        const int BM_CLICK = 0x00F5;

        //关闭信号
        public static int WM_CLOSE = 0x0010;

        //自定义消息
        public const int WM_MSG = 0x0400 + 200;

        const int WM_GETTEXT = 0x000D;
        const int WM_GETTEXTLENGTH = 0x000E;

        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
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
            public Rect rectCaret;
        }


        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }


        public delegate bool CallBack(IntPtr h, int l);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("user32.dll")]
        static extern bool GetGUIThreadInfo(uint idThread, ref GUITHREADINFO lpgui);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern long ShowWindow(IntPtr hWndChild, int count);

        // <summary>
        /// 获取窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名</param>
        /// <returns>返回句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindows);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, StringBuilder lParam);


        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern Int32 SendMessage2(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, Byte[] lParam);


        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );


        [DllImport("user32.dll")]
        public static extern bool SetFocus(IntPtr hWnd); //置顶窗体

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd); //置顶窗体


        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("User32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);


        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder sb, int length);


        public static GUITHREADINFO? GetGuiThreadInfo(IntPtr hwnd)
        {
            if (hwnd != IntPtr.Zero)
            {
                uint threadId = GetWindowThreadProcessId(hwnd, IntPtr.Zero);
                GUITHREADINFO guiThreadInfo = new GUITHREADINFO();
                guiThreadInfo.cbSize = Marshal.SizeOf(guiThreadInfo);
                if (GetGUIThreadInfo(threadId, ref guiThreadInfo) == false)
                    return null;
                return guiThreadInfo;
            }
            return null;
        }


        /// <summary>
        /// 查找窗体上控件句柄
        /// </summary>
        /// <param name="hwnd">父窗体句柄</param>
        /// <param name="lpszWindow">控件标题(Text)</param>
        /// <param name="bChild">设定是否在子窗体中查找</param>
        /// <returns>控件句柄，没找到返回IntPtr.Zero</returns>
        public static IntPtr FindWindowExMy(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = FindWindowEx(hwnd, IntPtr.Zero, null, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero)
                return iResult;

            // 如果设定了不在子窗体中查找
            if (!bChild)
                return iResult;

            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = FindWindowEx(h, IntPtr.Zero, null, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    StringBuilder title = new StringBuilder(200);
                    int len;
                    len = GetWindowText(hwnd, title, 200);

                    iResult = f1;
                    return false;
                }
            },
            0);
            // 返回查找结果
            return iResult;
        }

        /// <summary>
        /// 模糊匹配控件标题
        /// </summary>
        /// <param name="hwnd">父窗体句柄</param>
        /// <param name="dimStr">模糊匹配字符串</param>
        /// <returns></returns>
        public static IntPtr FindWindowExByDimStrIntoChildWindow(IntPtr hwnd, string dimStr)
        {

            IntPtr iResult = IntPtr.Zero;

            string controlTitle = ""; //控件完全标题

            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                //这里h为父窗口句柄

                IntPtr HwndStatue = FindWindowEx(h, IntPtr.Zero, "Static", null);   //文本框句柄 
                StringBuilder title1 = new StringBuilder(200);
                int len1;
                len1 = GetWindowText(h, title1, 200);

                if (HwndStatue != IntPtr.Zero)
                {
                    int TextLen;
                    TextLen = SendMessage2(HwndStatue, WM_GETTEXTLENGTH, 0, 0); //获取内容长度
                    Byte[] byt = new Byte[TextLen];
                    SendMessage((int)HwndStatue, WM_GETTEXT, TextLen + 1, byt); //获取内容
                    string str = Encoding.Default.GetString(byt);
                    // Log.WriteLog(string.Format("Edit控件内容{0}\n", str));
                    if (str.ToString().Contains(dimStr))
                    {
                        iResult = HwndStatue;  //返回当前窗口句柄
                        controlTitle = str.ToString();
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;

            },
            0);

            // 返回查找结果
            return iResult;
        }



        public static void SendText(string text, IntPtr handle)
        {
            //IntPtr hwnd = WinAPI.GetForegroundWindow();

            // 发送WM_SETTEXT 消息： "Hello World!"
            SendMessage(handle, WM_SETTEXT, IntPtr.Zero, new StringBuilder("Hello World!"));

            //if (String.IsNullOrEmpty(text))
            //    return;
            //GUITHREADINFO? guiInfo = GetGuiThreadInfo(hwnd);
            //if (guiInfo != null)
            //{
            //    for (int i = 0; i < text.Length; i++)
            //    {
            //        SendMessage(guiInfo.Value.hwndFocus, 0x0102, (IntPtr)(int)text[i], IntPtr.Zero);
            //    }
            //}
        }


        public static void TestNotepad()
        {
            // 返回写字板主窗口句柄
            IntPtr hWnd = FindWindow(null, "无标题 - 记事本");
            if (!hWnd.Equals(IntPtr.Zero))
            {
                //返回写字板编辑窗口句柄
                IntPtr edithWnd = FindWindowEx(hWnd, IntPtr.Zero, "RichEditD2DPT", null);
                if (!edithWnd.Equals(IntPtr.Zero))
                    // 发送WM_SETTEXT 消息： "Hello World!"
                    SendMessage(edithWnd, WM_SETTEXT, IntPtr.Zero, new StringBuilder("Hello World!"));
            }
        }
    }
}
