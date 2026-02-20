using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ToggleTabTip;

public class MainForm : Form
{
    private Button toggleKeyboardButton;
    
    public MainForm()
    {
        WindowFocusHelper.FocusWindowByName("Terminál Tuzi s.r.o. - Google Chrome"); // Přepneme fokus na Chrome
        
        // Vlastnosti okna
        this.FormBorderStyle = FormBorderStyle.None;
        this.TopMost = true;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 140;
        this.Height = 50;
        this.BackColor = Color.LightBlue;
        this.TransparencyKey = Color.LightBlue;

        // Tlačítko
        toggleKeyboardButton = new Button();
        toggleKeyboardButton.TabStop = false;
        toggleKeyboardButton.BackColor = Color.White;
        toggleKeyboardButton.Size = new Size(100, 50); // nastavíš rozměr tlačítka

        this.StartPosition = FormStartPosition.Manual;
        this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 
            Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        toggleKeyboardButton.Text = "Zobrazit klávesnici";
        // toggleKeyboardButton.Image = Image.FromFile(@"..\..\..\keyboard.png");
        // toggleKeyboardButton.Image = Image.FromFile(@"keyboard.png");


        toggleKeyboardButton.Dock = DockStyle.Fill;
        toggleKeyboardButton.Click += ToggleKeyboardButton_Click;

        this.Controls.Add(toggleKeyboardButton);
    }
    
    // protected override CreateParams CreateParams
    // {
    //     get
    //     {
    //         const int WS_EX_NOACTIVATE = 0x08000000;
    //         CreateParams cp = base.CreateParams;
    //         cp.ExStyle |= WS_EX_NOACTIVATE; // Nedovolí získat fokus
    //         return cp;
    //     }
    // }
    
    // protected override void WndProc(ref Message m)
    // {
    //     const int WM_TOUCH = 0x0240;
    //
    //     if (m.Msg == WM_TOUCH)
    //     {
    //         // Simulujeme kliknutí myší při dotyku
    //         var pos = this.PointToClient(Cursor.Position);
    //         Control control = this.GetChildAtPoint(pos);
    //
    //         if (control != null)
    //         {
    //             StartTabTip();
    //             // var mouseEvent = new MouseEventArgs(MouseButtons.Left, 1, pos.X, pos.Y, 0);
    //             // control.OnMouseClick(mouseEvent);
    //         }
    //
    //         m.Result = IntPtr.Zero;
    //         return;
    //     }
    //
    //     base.WndProc(ref m);
    // }

    private void ToggleKeyboardButton_Click(object sender, EventArgs e)
    {
        StartTabTip();
    }
    
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    
    private void StartTabTip()
    {
        try
        {
            var tabTipPath = @"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe";
            var startInfo = new ProcessStartInfo
            {
                FileName = tabTipPath,
                UseShellExecute = true,
                Verb = "open"
            };

            Process process = Process.Start(startInfo);

            // Po otevření TabTip, ujistíme se, že je na popředí
            if (process != null)
            {
                SetForegroundWindow(process.MainWindowHandle);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Nelze spustit TabTip: " + ex.Message);
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new MainForm());
    }
}