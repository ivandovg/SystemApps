
namespace SystemApps9_3
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lsbReg = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edPath = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsbReg
            // 
            this.lsbReg.FormattingEnabled = true;
            this.lsbReg.Location = new System.Drawing.Point(12, 51);
            this.lsbReg.Name = "lsbReg";
            this.lsbReg.Size = new System.Drawing.Size(368, 251);
            this.lsbReg.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path";
            // 
            // edPath
            // 
            this.edPath.Location = new System.Drawing.Point(12, 28);
            this.edPath.Name = "edPath";
            this.edPath.Size = new System.Drawing.Size(287, 20);
            this.edPath.TabIndex = 2;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(305, 26);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 307);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.edPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsbReg);
            this.Name = "MainForm";
            this.Text = "Registry Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edPath;
        private System.Windows.Forms.Button btnRead;
    }
}

