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
    public partial class frmDetailEvent : Form
    {
        public frmDetailEvent()
        {
            InitializeComponent();
        }
        public frmDetailEvent(DataGridViewRow row)
        {
            InitializeComponent();

            txtEventID.Text = (row.Cells[4].Value ?? "").ToString();
            txtVenID.Text = (row.Cells[1].Value ?? "").ToString();
            txtVenName.Text = (row.Cells[2].Value ?? "").ToString();

            txtStartTime.Text = (row.Cells[5].Value ?? "").ToString();
            txtDuration.Text = (row.Cells[6].Value ?? "").ToString();
            txtStatus.Text = (row.Cells[7].Value ?? "").ToString();
            txtMarKetContext.Text = (row.Cells[9].Value ?? "").ToString();
            txtSignalType.Text = (row.Cells[10].Value ?? "").ToString();
            txtCurrentValue.Text = (row.Cells[11].Value ?? "").ToString();
           


        }
        private void frmDetailEvent_Load(object sender, EventArgs e)
        {

        }
    }
}
