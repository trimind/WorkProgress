using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniTool
{
    public partial class frmListBoxView : Form
    {

        private ContextMenuStrip collectionRoundMenuStrip;
        private string _selectedMenuItem;

        public string textsaved { get; set; }
        public frmListBoxView(string textitem)
        {
            InitializeComponent();

            var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Delete" };
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;

            collectionRoundMenuStrip = new ContextMenuStrip();
            collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });

            var arr= textitem.Split(new char[] { '=' });
            listBox1.Items.Clear();
            listBox1.Items.AddRange(arr[1].Split('|'));


        }
        public frmListBoxView()
        {
            InitializeComponent();

            var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Delete" };
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;

            collectionRoundMenuStrip = new ContextMenuStrip();
            collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(_selectedMenuItem);


        }
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;
            var index = listBox1.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listBox1.SelectedIndex = index;
                _selectedMenuItem = listBox1.Items[index].ToString();
                collectionRoundMenuStrip.Show(Cursor.Position);
                collectionRoundMenuStrip.Visible = true;
            }
            else
            {
                collectionRoundMenuStrip.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            textsaved = string.Join("|", listBox1.Items.Cast<string>());
            this.Close();

        }
    }
}
