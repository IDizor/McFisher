using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace McFisher.Misc;

internal static class Native
{
    public const int WM_KEYDOWN = 0x100;
    public const int WM_KEYUP = 0x101;
    public const int WM_COMMAND = 0x111;
    public const int WM_LBUTTONDOWN = 0x201;
    public const int WM_LBUTTONUP = 0x202;
    public const int WM_LBUTTONDBLCLK = 0x203;
    public const int WM_RBUTTONDOWN = 0x204;
    public const int WM_RBUTTONUP = 0x205;
    public const int WM_RBUTTONDBLCLK = 0x206;

    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr windowHandle, int hotkeyId, uint modifierKeys, uint virtualKey);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr windowHandle, int hotkeyId);

    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ClientToScreen(IntPtr hWnd, out POINT lpPoint);

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
    public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;        // x position of upper-left corner  
        public int Top;         // y position of upper-left corner  
        public int Right;       // x position of lower-right corner  
        public int Bottom;      // y position of lower-right corner  
    }

    public static IntPtr CreateLParam(int LoWord, int HiWord)
    {
        return (IntPtr)(HiWord << 16 | LoWord & 0xffff);
    }

    public static string? GetWindowTitle(IntPtr hwnd)
    {
        const int nChars = 256;
        StringBuilder Buff = new(nChars);

        if (GetWindowText(hwnd, Buff, nChars) > 0)
        {
            return Buff.ToString();
        }

        return null;
    }

    public static Rect? GetWindowRect(IntPtr hWnd)
    {
        if (GetWindowRect(hWnd, out RECT rect))
        {
            return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        return null;
    }

    public static Rect? GetClientRect(IntPtr hWnd)
    {
        if (ClientToScreen(hWnd, out POINT point) && GetClientRect(hWnd, out RECT rect))
        {
            return new Rect(point.X, point.Y, rect.Right, rect.Bottom);
        }

        return null;
    }

    public static Point GetCursorPosition()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);

        return lpPoint;
    }
}