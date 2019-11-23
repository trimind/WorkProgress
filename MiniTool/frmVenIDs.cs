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
    public partial class frmVenIDs : Form
    {
        public frmVenIDs()
        {
            InitializeComponent();
            this.ActiveControl = txtVenName;
        }
        public int TotalCount => listBox1.Items.Count;
        private  ContextMenuStrip collectionRoundMenuStrip;
        private string _selectedMenuItem;

        private void frmVenIDs_Load(object sender, EventArgs e)
        {
            var toolStripMenuItem1 = new ToolStripMenuItem { Text = "Delete" };
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
          
            collectionRoundMenuStrip = new ContextMenuStrip();
            collectionRoundMenuStrip.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            listBox1.MouseDown += listBox1_MouseDown;

            var settings = new IniFile("Settings.ini");
            var IDs = settings.Read("VenIDs", "Ven Settings")??"";
            listBox1.Items.AddRange(IDs.Split('-').Where(a=> !string.IsNullOrWhiteSpace(a)).ToArray());

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
    

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(_selectedMenuItem);


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if ( txtVenName.Text.IndexOfAny(":|,-`".ToCharArray()) != -1 || txtvenID.Text.IndexOfAny(":|,-`".ToCharArray()) != -1)
            {
                MessageBox.Show("Name or ID không chứa kí tự | , : `");
                return;

            }
            if (txtVenName.Text=="" || txtvenID.Text=="")
            {
                MessageBox.Show("Name và ID bắt buộc nhập");
                return;

            }
            int s;
            if (txtVenNum.Text == "" || !int.TryParse(txtVenNum.Text, out s))
            {
                MessageBox.Show("VenNum bắt buộc nhập");
                return;

            }

            var items = listBox1.Items.Cast<string>().ToArray();
            listBox1.Items.Clear();
            var isHasItem = false;
            foreach (var w in items)
            {
                var arrtmp = w.Split(new char[] { '=' }) ;
                
                var first = arrtmp[0];
                var last = arrtmp[1];
                if (first.Split(new char[] { ':' })[1] == txtVenNum.Text.Trim())
                {
                    last += "|Name:" + txtVenName.Text + ",ID:" + txtvenID.Text;
                    isHasItem = true;
                }

                listBox1.Items.Add(first + "=" + last);
            }

            if (!isHasItem)
            {
                listBox1.Items.Add("Num:" + txtVenNum.Text + "=Name:" + txtVenName.Text + ",ID:" + txtvenID.Text);
            }

           // 
            txtVenName.Text = "";
            txtvenID.Text = "";
            txtVenName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var settings = new IniFile("Settings.ini");
            var str = string.Join("-", listBox1.Items.Cast<string>().ToList());
            settings.Write("VenIDs", str, "Ven Settings");
            this.Close();

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var textselected = listBox1.SelectedItem.ToString();
                var f = new frmListBoxView(textselected);
                f.ShowDialog();

                if (!string.IsNullOrWhiteSpace(f.textsaved))
                {
                    var index = listBox1.SelectedIndex;
                    var arr = textselected.Split('=');
                    var text = arr[0] + "=" + f.textsaved;
                    listBox1.Items[index] = text;


                }
            }
            catch
            {
            }

           
         
        }
    }
}
