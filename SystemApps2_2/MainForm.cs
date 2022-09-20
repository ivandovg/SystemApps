using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace SystemApps2_2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dgvMain.MultiSelect = false;
            dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnProcessList_Click(object sender, EventArgs e)
        {
            dgvMain.DataSource = Process.GetProcesses()
                .Select(p => new { p.ProcessName, p.Id, ThreadsCount = p.Threads.Count, Process = p })
                .OrderBy(p => p.ProcessName).ToList();
        }

        private void btnKillProcess_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 0)
                return;

            Process process = dgvMain.SelectedRows[0].Cells["Process"].Value as Process;
            if (process != null)
            {
                process.Kill();
            }
        }

        private void btnModules_Click(object sender, EventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 0)
                return;

            Process process = dgvMain.SelectedRows[0].Cells["Process"].Value as Process;
            dgvMain.DataSource = null;
            if (process != null)
            {
                List<ProcessModule> modules = new List<ProcessModule>();
                foreach (ProcessModule m in process.Modules)
                {
                    modules.Add(m);
                }
                dgvMain.DataSource = modules.OrderBy(m => m.ModuleName).ToList();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Application|*.exe|All files|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            };

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            ProcessStartInfo info = new ProcessStartInfo(dlg.FileName);
            Process process = Process.Start(info);
            Thread thread = new Thread(Process_Wait);
            thread.IsBackground = true;
            thread.Start(process);

            //process.Exited += Process_Exited;
            //process.WaitForExit();
            //MessageBox.Show("Process exit");
            //try
            //{
            //    process.Kill();
            //} 
            //catch { }
        }

        private void Process_Wait(object sender)
        {
            Process process = sender as Process;
            if (process == null)
                return;

            string name = process.ProcessName;
            process.WaitForExit();
            MessageBox.Show($"Process '{name}' exited");
        }
    }
}
