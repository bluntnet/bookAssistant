using BookAssistant;
using System;
using System.Windows.Forms;

namespace BookAssitant {
    public partial class MainForm : Form {
        private readonly string xulrunnerPath = Application.StartupPath + "/xulrunner";
        DanDanForm dandanForm = null;
        XHForm xhform = null;
        public MainForm() {
            InitializeComponent();
        }

        private void 新华书店ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (xhform == null || xhform.IsDisposed) {
                xhform = new XHForm();
            }
            xhform.MdiParent = this;
            xhform.Show();
            xhform.Activate();
        }

        private void 当当书店ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (dandanForm == null || dandanForm.IsDisposed) {
                dandanForm = new DanDanForm();
            }
            dandanForm.MdiParent = this;
            dandanForm.Show();
            dandanForm.Activate();
        }
    }
}
