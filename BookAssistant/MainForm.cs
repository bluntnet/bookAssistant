using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BookAssistant
{
    public partial class MainForm : Form
    {
        Form1 form1 = null;
        XHForm xhform = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void 当当网书店ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form1 == null || form1.IsDisposed)
            {
                form1 = new Form1();
            }
            
            form1.MdiParent = this;
            form1.Show();
            form1.Activate();
        }

        private void 新华书店ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (xhform == null || xhform.IsDisposed)
            {
                xhform = new XHForm();
            }
            xhform.MdiParent = this;
            xhform.Show();
            xhform.Activate();
        }
    }
}