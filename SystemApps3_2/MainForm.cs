using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SystemApps3_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            thread = null;
        }
        private Thread thread;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (thread != null)
                return;

            thread = new Thread(ShowTime);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ShowTime()
        {
            Action action = () =>
            {
                Text = DateTime.Now.ToLongTimeString();
            };

            while (true)
            {
                if (this.InvokeRequired)
                    this.Invoke(action);
                else
                    action();

                Thread.Sleep(1000);
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            if (thread == null)
                return;

            thread.Suspend();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (thread == null)
                return;

            thread.Resume();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (thread == null)
                return;

            thread.Abort();
            thread = null;
        }

        private void btnStartSync_Click(object sender, EventArgs e)
        {
            ShowTime();
        }
    }
}
