namespace BookAssitant {
    partial class MainForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选择书店ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当当书店ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新华书店ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firefoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择书店ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选择书店ToolStripMenuItem
            // 
            this.选择书店ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当当书店ToolStripMenuItem,
            this.新华书店ToolStripMenuItem,
            this.firefoxToolStripMenuItem});
            this.选择书店ToolStripMenuItem.Name = "选择书店ToolStripMenuItem";
            this.选择书店ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.选择书店ToolStripMenuItem.Text = "选择书店";
            // 
            // 当当书店ToolStripMenuItem
            // 
            this.当当书店ToolStripMenuItem.Name = "当当书店ToolStripMenuItem";
            this.当当书店ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.当当书店ToolStripMenuItem.Text = "当当书店";
            this.当当书店ToolStripMenuItem.Click += new System.EventHandler(this.当当书店ToolStripMenuItem_Click);
            // 
            // 新华书店ToolStripMenuItem
            // 
            this.新华书店ToolStripMenuItem.Name = "新华书店ToolStripMenuItem";
            this.新华书店ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.新华书店ToolStripMenuItem.Text = "新华书店";
            this.新华书店ToolStripMenuItem.Click += new System.EventHandler(this.新华书店ToolStripMenuItem_Click);
            // 
            // firefoxToolStripMenuItem
            // 
            this.firefoxToolStripMenuItem.Name = "firefoxToolStripMenuItem";
            this.firefoxToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.firefoxToolStripMenuItem.Text = "Firefox";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "图书管理系统";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选择书店ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当当书店ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新华书店ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firefoxToolStripMenuItem;
    }
}

