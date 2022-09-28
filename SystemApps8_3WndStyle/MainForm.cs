using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SystemApps8_3WndStyle
{
    public partial class MainForm : Form
    {
        string[] vStyles;
        string[] vStylesEx;
        public MainForm()
        {
            InitializeComponent();
            FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsShell.UnregisterHotKey(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Keys keys = Keys.A | Keys.Control | Keys.Alt;
            WindowsShell.RegisterHotKey(this, keys);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WindowsShell.WM_HOTKEY)
            {
                this.Visible = !this.Visible;
            }
        }

        private void GetStylesWnd(bool style, bool styleEx)
        {
            IntPtr result;
            int v, r;
            // Window Class Styles
            // https://learn.microsoft.com/en-us/windows/win32/winmsg/window-class-styles
            if (style)
            {
                lsbStyles.Items.Clear();
                result = WindowsShell.GetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_STYLE);
                vStyles = Enum.GetNames(typeof(WindowStyle));
                r = (int)result;
                for (int i = 0; i < vStyles.Length; i++)
                {
                    v = (int)Enum.Parse(typeof(WindowStyle), vStyles[i]);
                    if ((v & r) == v)
                        lsbStyles.Items.Add(vStyles[i] + " = On");
                    else
                        lsbStyles.Items.Add(vStyles[i] + " = Off");
                }
            }

            // Extended Window Styles
            // https://learn.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles
            if (styleEx)
            {
                result = WindowsShell.GetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_EXSTYLE);
                lsbStylesEx.Items.Clear();
                vStylesEx = Enum.GetNames(typeof(WindowStyleEx));
                r = (int)result;
                for (int i = 0; i < vStylesEx.Length; i++)
                {
                    v = (int)Enum.Parse(typeof(WindowStyleEx), vStylesEx[i]);
                    if ((v & r) == v)
                        lsbStylesEx.Items.Add(vStylesEx[i] + " = On");
                    else
                        lsbStylesEx.Items.Add(vStylesEx[i] + " = Off");
                }
            }
        }
        private void lsbStyles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsbStyles.SelectedIndex == ListBox.NoMatches)
                return;

            WindowStyle item = (WindowStyle)Enum.Parse(typeof(WindowStyle), vStyles[lsbStyles.SelectedIndex]);

            IntPtr result = WindowsShell.GetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_STYLE);
            uint code = (uint)result;
            uint newCode = code ^ (uint)item;
            IntPtr newResult = WindowsShell.SetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_STYLE, (IntPtr)newCode);

            GetStylesWnd(true, false);
        }

        private void lsbStylesEx_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsbStylesEx.SelectedIndex == ListBox.NoMatches)
                return;

            WindowStyleEx item = (WindowStyleEx)Enum.Parse(typeof(WindowStyleEx), vStylesEx[lsbStylesEx.SelectedIndex]);

            IntPtr result = WindowsShell.GetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_EXSTYLE);
            uint code = (uint)result;
            uint newCode = code ^ (uint)item;
            IntPtr newResult = WindowsShell.SetWindowLongPtr(Handle, (int)WindowLongFlags.GWL_EXSTYLE, (IntPtr)newCode);

            GetStylesWnd(false, true);
        }

        private void btnGetLong_Click(object sender, EventArgs e)
        {
            GetStylesWnd(true, true);
        }
    }
}
