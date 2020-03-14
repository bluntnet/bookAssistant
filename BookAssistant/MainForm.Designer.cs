namespace BookAssistant
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选择书店处理程序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当当网书店ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新华书店ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择书店处理程序ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选择书店处理程序ToolStripMenuItem
            // 
            this.选择书店处理程序ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当当网书店ToolStripMenuItem,
            this.新华书店ToolStripMenuItem});
            this.选择书店处理程序ToolStripMenuItem.Name = "选择书店处理程序ToolStripMenuItem";
            this.选择书店处理程序ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.选择书店处理程序ToolStripMenuItem.Text = "选择书店(&B)";
            // 
            // 当当网书店ToolStripMenuItem
            // 
            this.当当网书店ToolStripMenuItem.Name = "当当网书店ToolStripMenuItem";
            this.当当网书店ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.当当网书店ToolStripMenuItem.Text = "当当网书店";
            this.当当网书店ToolStripMenuItem.Click += new System.EventHandler(this.当当网书店ToolStripMenuItem_Click);
            // 
            // 新华书店ToolStripMenuItem
            // 
            this.新华书店ToolStripMenuItem.Name = "新华书店ToolStripMenuItem";
            this.新华书店ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新华书店ToolStripMenuItem.Text = "新华书店";
            this.新华书店ToolStripMenuItem.Click += new System.EventHandler(this.新华书店ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "图书辅助管理系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选择书店处理程序ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当当网书店ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新华书店ToolStripMenuItem;
    }
}