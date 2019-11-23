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
    public partial class frmLetterTextEffect : Form
    {
        int counter = 0;
        int len = 0;
        string txt;

        public frmLetterTextEffect()
        {
            InitializeComponent();
        }

        private void frmLetterTextEffect_Load(object sender, EventArgs e)
        {
            txt = label1.Text;
            len = txt.Length;
            label1.Text = "";
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;

            if (counter > len)
            {
                counter = 0;
                label1.Text = "";
            }

            else
            {

                label1.Text = txt.Substring(0, counter);

                if (label1.ForeColor == Color.Blue)
                    label1.ForeColor = Color.Orange;
                else
                    label1.ForeColor = Color.Blue;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmLetterTextEffect_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
