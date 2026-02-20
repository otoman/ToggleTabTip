using System.Runtime.InteropServices;

namespace ToggleTabTip;

public class WindowFocusHelper
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    // [DllImport("user32.dll")]
    // public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public const int SW_RESTORE = 9; // Obnoví minimalizované okno, nebo obnoví z fullscreen režimu

    public static void FocusWindowByName(string windowName)
    {
        IntPtr hWnd = FindWindow(null, windowName);
        if (hWnd != IntPtr.Zero)
        {
            // Obnovíme okno (pokud je minimalizováno nebo ve fullscreen režimu)
            ShowWindow(hWnd, SW_RESTORE);
            SetForegroundWindow(hWnd); // Až po obnovení okna, přepneme focus
        }
        else
        {
            Console.WriteLine("Okno nebylo nalezeno.");
        }
    }

}