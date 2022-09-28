
namespace SystemApps8_3WndStyle
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
            this.btnGetLong = new System.Windows.Forms.Button();
            this.lsbStyles = new System.Windows.Forms.ListBox();
            this.lsbStylesEx = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGetLong
            // 
            this.btnGetLong.Location = new System.Drawing.Point(12, 12);
            this.btnGetLong.Name = "btnGetLong";
            this.btnGetLong.Size = new System.Drawing.Size(75, 23);
            this.btnGetLong.TabIndex = 0;
            this.btnGetLong.Text = "Get styles";
            this.btnGetLong.UseVisualStyleBackColor = true;
            this.btnGetLong.Click += new System.EventHandler(this.btnGetLong_Click);
            // 
            // lsbStyles
            // 
            this.lsbStyles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lsbStyles.FormattingEnabled = true;
            this.lsbStyles.Location = new System.Drawing.Point(12, 41);
            this.lsbStyles.Name = "lsbStyles";
            this.lsbStyles.Size = new System.Drawing.Size(213, 342);
            this.lsbStyles.TabIndex = 1;
            this.lsbStyles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsbStyles_MouseDoubleClick);
            // 
            // lsbStylesEx
            // 
            this.lsbStylesEx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbStylesEx.FormattingEnabled = true;
            this.lsbStylesEx.Location = new System.Drawing.Point(231, 41);
            this.lsbStylesEx.Name = "lsbStylesEx";
            this.lsbStylesEx.Size = new System.Drawing.Size(251, 342);
            this.lsbStylesEx.TabIndex = 2;
            this.lsbStylesEx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsbStylesEx_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Extended Window Styles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Window Class Styles";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(307, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Двойной клик в списке - переключает стиль окна";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 420);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsbStylesEx);
            this.Controls.Add(this.lsbStyles);
            this.Controls.Add(this.btnGetLong);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Test Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetLong;
        private System.Windows.Forms.ListBox lsbStyles;
        private System.Windows.Forms.ListBox lsbStylesEx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

