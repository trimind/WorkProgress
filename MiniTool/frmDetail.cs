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
    public partial class frmDetail : Form
    {
        public frmDetail()
        {
            InitializeComponent();
        }
        public frmDetail(DataGridViewRow row)
        {
            InitializeComponent();
            txtDate.Text = (row.Cells[4].Value??"").ToString();
            txtVenID.Text = (row.Cells[2].Value??"").ToString();
            txtVenName.Text = (row.Cells[3].Value??"").ToString();

            txtResponseTime.Text =( row.Cells[5].Value??"").ToString();
            txtResponseType.Text = (row.Cells[7].Value??"").ToString();
            txtRequestType.Text = (row.Cells[6].Value??"").ToString();
            txtResponseCode.Text =( row.Cells[8].Value??"").ToString();
            txtResponseDescription.Text = (row.Cells[9].Value??"").ToString();
            txtRequestXML.Text = (row.Cells[10].Value??"").ToString();
            txtResponseXML.Text = (row.Cells[11].Value??"").ToString();


        }
        private void frmDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
