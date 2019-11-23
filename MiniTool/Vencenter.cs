using MiniTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dapper.PagingSample
{
    public partial class frmVencenter : Form
    {
        public frmVencenter()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void frmVencenter_Load(object sender, EventArgs e)
        {
            //lấy các thiết đặt tư file txt
            var settings = new IniFile("Settings.ini");
           // var venname = settings.Read("VEN Name", "Ven Settings");
          //  var venID = settings.Read("VEN ID", "Ven Settings");
            var url = settings.Read("URL", "Ven Settings");
            var maxvens = settings.Read("MaxVen", "Ven Settings");
            var maxloads = settings.Read("MaxLoads", "Ven Settings");
            var maxThread = settings.Read("MaxThread", "Ven Settings");
            var hideVen = settings.Read("HideVen", "Ven Settings");

            var IDstext = settings.Read("VenIDs", "Ven Settings") ?? "";
            var arrIDs = IDstext.Split('-').Where(a => !string.IsNullOrWhiteSpace(a)).ToArray();

            txtUrl.Text = url;
            txtMaxthread.Text = maxThread;

            //txtvenID.Text = venID;
            //txtVenName.Text = venname;
            this.lblTotalIDs.Text = "Total:" + arrIDs.Length.ToString();

            txtNumberVen.Text = maxvens;
            txtNumLoads.Text = maxloads;
            if ((hideVen??"").ToLower() == "true")
            {
                chkHidevens.Checked = true;
            }
            else chkHidevens.Checked = false;

        }

        private  void btnCreateVen_ClickAsync(object sender, EventArgs e)
        {
            if (txtNumberVen.Text=="" || txtNumLoads.Text=="")
            {
                MessageBox.Show("Data input is not valid !");
                return;

            }


            var settings = new IniFile("Settings.ini");
           // settings.Write("VEN Name", txtvenID.Text, "Ven Settings");
         //   settings.Write("VEN ID", txtVenName.Text, "Ven Settings");
            settings.Write("URL", txtUrl.Text, "Ven Settings");

            settings.Write("MaxVen", txtNumberVen.Text, "Ven Settings");
            settings.Write("MaxLoads", txtNumLoads.Text, "Ven Settings");
            settings.Write("HideVen", chkHidevens.Checked.ToString(), "Ven Settings");
            settings.Write("MaxThread", txtMaxthread.Text, "Ven Settings");


            var Path = AppDomain.CurrentDomain.BaseDirectory + "/VenCenter.exe";

            var res =  ProcessAsyncHelper.RunProcessAsync(Path, "start -n " + txtNumberVen.Text + " -l " + txtNumLoads.Text  +" -t " + txtMaxthread.Text + (chkHidevens.Checked ? "":" -s"), 3*60*1000).ConfigureAwait(false);


            this.Close();

        }

        private  void button2_Click(object sender, EventArgs e)
        {
            var Path = AppDomain.CurrentDomain.BaseDirectory + "/VenCenter.exe";
            var res = ProcessAsyncHelper.RunProcessAsync(Path, "stop -all ", 3 * 60 * 1000).ConfigureAwait(false);


            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new frmVenIDs())
            {
                var result = form.ShowDialog();
               
                    string val = form.TotalCount.ToString();           

                this.lblTotalIDs.Text = "Total:" + val;



            }
          
        }
    }
}
