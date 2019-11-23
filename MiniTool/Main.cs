using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper.PagingSample;
using Dapper.PagingSample.Common;

using Dapper.PagingSample.Logging;
using MiniTool;
using MiniTool.Entity;
using MiniTool.Repository;
using Microsoft.VisualBasic;
using System.IO;
using System.Runtime.InteropServices;

namespace MiniTool
{
    public partial class Main : Form
    {
       
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public Main()
        {
            Logger.InfoFormat("Starting InitializeComponent...");

            InitializeComponent();
         
            this.txtConnectionString.Text = ConfigurationManager.ConnectionStrings["VenDB"]
                .ConnectionString;

            Logger.InfoFormat("InitializeComponent complete.");

        }
        private LogRepository GetRepository()
        {
            return new LogRepository(this.txtConnectionString.Text);
        }

        private LogSearchCriteria GetCritera()
        {
            var newsearch = new LogSearchCriteria();
            FlowLayoutPanel panel = null;
            if (cboTable.SelectedIndex == 0)
            {
                panel = panel1;
             //   newsearch.Type = "Log";
            }
            else if (cboTable.SelectedIndex == 2)
            {
                panel = panel3;
              //  newsearch.Type = "Report";

            }
            if (panel == null) return null;

            var dict = panel.Controls.Cast<GroupBox>().ToDictionary(a=>a.Text,a=> a.Controls[0] is DateTimePicker? ((DateTimePicker) a.Controls[0]).Value.ToString(): a.Controls[0].Text);
         
            int s; decimal d;DateTime dt;
            foreach (var p in dict)
            {
                var k = p.Key.ToLower();
                if (k=="vennum")
                {
                    if ( int.TryParse(p.Value, out s))
                        newsearch.VenNum = p.Value;
                } else if (k == "responsexml")
                {
                    newsearch.ResponseXML = p.Value;
                }
                else if (k == "requestxml")
                {
                    newsearch.RequestXML = p.Value;
                }
                else if (k == "venname")
                {
                    newsearch.VenName = p.Value;
                }
                else if (k == "venid")
                {
                    newsearch.VenID = p.Value;
                }
                else if (k == "from responsetime" && decimal.TryParse(p.Value, out d))
                {
                    newsearch.fromResponseTime = d;
                }
                else if (k == "to responsetime" && decimal.TryParse(p.Value, out d))
                {
                    newsearch.toResponseTime = d;
                }
                else if (k == "responsetime" && decimal.TryParse(p.Value, out d))
                {
                    newsearch.ResponseTime = d;
                }
                else if (k == "date" && DateTime.TryParse(p.Value, out dt))
                {
                    newsearch.Date = dt.Date ;
                }
                else if (k == "from date" && DateTime.TryParse(p.Value, out dt))
                {
                    newsearch.fromDate = dt;
                }
                else if (k == "to date" && DateTime.TryParse(p.Value, out dt))
                {
                    newsearch.toDate = dt;
                }
                else if (k == "responsecode" && int.TryParse(p.Value, out s))
                {
                    newsearch.ResponseCode = s;
                }
                else if (k == "responsetype" )
                {
                    newsearch.ResponseType = p.Value;
                }
                else if (k == "requesttype" )
                {
                    newsearch.RequestType = p.Value;
                }
                else if (k == "responsedescription")
                {
                    newsearch.ResponseDescription = p.Value;
                }

            }

            return newsearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            var repo = GetRepository();
            Tuple<IEnumerable<Log>, int> result;

            var sortings = new List<SortDescriptor>();
            var arr = (SortField ?? "").Split('-').Select(a => a.ToLower()).ToArray();

            if (arr[0] == "responsetime")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1]=="desc"? SortDescriptor.SortingDirection.Descending: SortDescriptor.SortingDirection.Ascending, Field = "responseTime" });
            }
            else if (arr[0] == "date")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "Date" });
            }
            else if (arr[0] == "responsecode")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "ResponseCode" });
            }
            else if (arr[0] == "requesttype")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "requestType" });
            }
            else if (arr[0] == "responsetype")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "responseType" });
            }

            else
                     sortings.Add(new SortDescriptor { Direction =  SortDescriptor.SortingDirection.Descending , Field = "responseTime" });

            //if (!string.IsNullOrWhiteSpace(this.txtSortDesc.Text))
            //    sortings.Add(new SortDescriptor { Direction = SortDescriptor.SortingDirection.Descending, Field = this.txtSortDesc.Text.Trim() });

            var cri = GetCritera();


            if (cri==null)
            {
                MessageBox.Show("Vui lòng nhập số.");

                return;
            }
                result = repo.FindWithOffsetFetch(cri,
                    int.Parse(this.txtPageIndex.Text),
                    int.Parse(this.txtPageSize.Text),
                    sortings);

            dgvData.SuspendLayout();
            this.dgvData.DataSource = result.Item1;
            this.lTotalCountValue.Text = result.Item2.ToString();

            dgvData.ResumeLayout();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int current = int.Parse(this.txtPageIndex.Text);
            var pre = current-1;
            if (pre < 0)
                pre = 0;

            this.txtPageIndex.Text = pre.ToString();

            if (cboTable.SelectedIndex == 0)
                btnSearch.PerformClick();
            else
                btnSearchEvent.PerformClick();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int current = int.Parse(this.txtPageIndex.Text);
            var next = current + 1;
            if (next < 0)
                next = 0;

            this.txtPageIndex.Text = next.ToString();

            if (cboTable.SelectedIndex == 0)
                btnSearch.PerformClick();
            else
                btnSearchEvent.PerformClick();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            cboTable.SelectedIndex = 0;
            //btnSearch_Click(null, null);

           // grpLog.Location = new Point(17, 38);

        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTable.SelectedIndex == 0)
                {
                    grpLog.Location = new Point(17, 38);
                    grpLog.Visible = true;
                    grpEvent.Visible = false;
                    gpReport.Visible = false;
                    grpLog.Text = cboTable.Text ;
                   // ClearAllText(grpEvent);
                    btnSearch_Click(null, null);
                 
                }
                else if (cboTable.SelectedIndex == 1)
                {
                    grpEvent.Location = new Point(17, 38);
                    grpEvent.Visible = true;
                    grpLog.Visible = false;
                    gpReport.Visible = false;
                    grpEvent.Text = cboTable.Text ;
                  //  ClearAllText(grpLog);
                    btnSearchEvent_Click(null, null);
                }
                else if (cboTable.SelectedIndex == 2)
                {
                    gpReport.Location = new Point(17, 38);
                    gpReport.Visible = true;
                    grpEvent.Visible = false;
                    grpLog.Visible = false;
                    gpReport.Text = cboTable.Text;
                    //  ClearAllText(grpLog);
                    btnSearch_Click(null, null);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void btnSearchEvent_Click(object sender, EventArgs e)
        {
            var repo = new EventRepository(this.txtConnectionString.Text);
            Tuple<IEnumerable<Event>, int> result;

            var sortings = new List<SortDescriptor>();
            var arr = (SortField ?? "").Split('-').Select(a=>a.ToLower()).ToArray();
           
            if (arr[0]=="responsetime")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "responseTime" });
            }
            else if (arr[0] == "date")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "Date" });
            }
            else
                sortings.Add(new SortDescriptor { Direction = SortDescriptor.SortingDirection.Descending, Field = "DateCreated" });

            var critera = getEventSearch();

            result = repo.FindWithOffsetFetch(critera,
                int.Parse(this.txtPageIndex.Text),
                int.Parse(this.txtPageSize.Text),
                sortings);

         
            this.dgvData.DataSource = result.Item1;
            this.lTotalCountValue.Text = result.Item2.ToString();
        }

        //private void btnGenerateInfoLog_Click(object sender, EventArgs e)
        //{

        //    var r = new Random();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        Logger.InfoFormat("Randomly generated Info log:{0}", r.Next(1000, 1000000));
        //    }

        //    MessageBox.Show("100 info log entries has been generated.");
        //}

            private EventSearchCriteria getEventSearch()
        {
            var dict = panel2.Controls.Cast<GroupBox>().ToDictionary(a => a.Text, a => a.Controls[0] is DateTimePicker ? ((DateTimePicker)a.Controls[0]).Value.ToString() : a.Controls[0].Text);
            var newsearch = new EventSearchCriteria();
            //int s; decimal d;
            DateTime dt;
            foreach (var p in dict)
            {
                var k = p.Key.ToLower();
                if (k == "idevent")
                {
                   
                        newsearch.EventID = p.Value;
                }
                else if (k == "marketcontext")
                {
                    newsearch.MarketContext = p.Value;
                }
                else if (k == "starttime" && DateTime.TryParse(p.Value, out dt))
                {
                    newsearch.StartTime =dt.Date;
                }
                else if (k == "status" )
                {
                    newsearch.Status = p.Value;
                }
                else if (k == "currentvalue")
                {
                    newsearch.CurrentValue = p.Value;
                }
                else if (k == "currentvalue")
                {
                    newsearch.CurrentValue = p.Value;
                }
                else if (k == "vtncomment")
                {
                    newsearch.VTNComment = p.Value;
                }
                else if (k == "responserequired")
                {
                    newsearch.ResponseRequired = p.Value;
                }

                

            }

            return newsearch;
        }
        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }

        private void btnOpenVenCenter_Click(object sender, EventArgs e)
        {
            new frmVencenter().ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var total = 0;
            var all = Process.GetProcessesByName("oadr2b-ven");
            foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
            {
               // IntPtr handle = window.Key;
                string title = window.Value;
                if (title.StartsWith("VEN")) total++;
                
            }

            lblVensRunning.Text = "vens running: " + total;
            lblProcessRunning.Text = "process running: " + all.Count() ;
        }

        private void btnClearlog_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure?", "Clear table", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var repo = new LogRepository(this.txtConnectionString.Text);
                repo.ClearTable("Log");
                //MessageBox.Show("Finish !.")
                btnSearch.PerformClick();
            }
            
        }

        private void btnClearEvent_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure?", "Clear table", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var repo = new EventRepository(this.txtConnectionString.Text);
                repo.ClearTable();
                //MessageBox.Show("Finish !.")
                btnSearchEvent.PerformClick();
            }
        }


        private string SortField;

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string headerText = dgvData.Columns[e.ColumnIndex].HeaderText;
            var h0 = headerText.Split('-')[0];
            var f = SortField ?? "";
            if (f.Contains(h0))
            {
                var arr = f.Split('-');
                if (arr[1] == "asc") SortField = arr[0] + "-desc";
                else SortField = arr[0] + "-asc";
               

            }
            else SortField = h0 + "-asc";

            dgvData.Columns[e.ColumnIndex].HeaderText = SortField;
            if (cboTable.SelectedIndex == 0)
            {
                btnSearch.PerformClick();
            }
            else if (cboTable.SelectedIndex == 1)
            {
                btnSearchEvent.PerformClick();
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //{
            //    return;
            //}

            //int index = e.RowIndex;
            //dgvData.Rows[index].Selected = true;
        }

        private void dgvData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //var RowsIndex = dgvData.CurrentRow.Index;
            if (cboTable.SelectedIndex == 0)
            {
                new frmDetail(dgvData.CurrentRow).ShowDialog();
            }
            else if (cboTable.SelectedIndex == 1)
            {
                new frmDetailEvent(dgvData.CurrentRow).ShowDialog();
            }

        }

        private void btnListCols_Click(object sender, EventArgs e)
        {
            if (cboTable.SelectedIndex == 0)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else if (cboTable.SelectedIndex == 1)
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else if (cboTable.SelectedIndex == 2)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip1.Items.Count > 0)
                contextMenuStrip1.Items.Clear();


            FlowLayoutPanel panel = null;
            if (cboTable.SelectedIndex == 0)
            {
                panel = panel1;
            }
            else if (cboTable.SelectedIndex == 2)
            {
                panel = panel3;
            }
            if (panel == null) return;

            var lst = panel.Controls.Cast<GroupBox>().Select(a => a.Text).ToList();
            lst.AddRange(new string[] { "Id","TotalCount","Type","DateCreated"});

            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                var name = column.DataPropertyName;
               
                if (!lst.Any(a=>a==name))
                {
                    contextMenuStrip1.Items.Add(name);
                    
                }

               var  name2 = name.ToLower();
                if (name2 == "responsetime")
                {
                    if (!lst.Any(a => a == "from ResponseTime"))
                        contextMenuStrip1.Items.Add("from ResponseTime");

                    if (!lst.Any(a => a == "to ResponseTime"))
                        contextMenuStrip1.Items.Add("to ResponseTime");
                } else if (name2 == "date")
                {
                    if (!lst.Any(a => a == "from Date"))
                        contextMenuStrip1.Items.Add("from Date");

                    if (!lst.Any(a => a == "to Date"))
                        contextMenuStrip1.Items.Add("to Date");
                }
            }
            
            e.Cancel = false;
        }



        private List<Control> lstFilter = new List<Control>();

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            FlowLayoutPanel panel=null; 
            if (cboTable.SelectedIndex == 0)
            {
                panel = panel1;
            }
            else if (cboTable.SelectedIndex == 2)
            {
                panel = panel3;
            }
            if (panel == null) return;

            if (panel.Controls.Count==6)
            {
                MessageBox.Show("Không còn chổ để tạo mới.");
                return;
            }
            var w = new GroupBox();
            panel.Controls.Add(w);
            w.Size = new Size(177, 63);
            w.Location = new Point(7 + (panel.Controls.Count-1) * 177, 2);
            Control t;
            if ( e.ClickedItem.Text=="from Date" || e.ClickedItem.Text == "to Date")
            {
                t = new DateTimePicker();
                var t1 = t as DateTimePicker;
                t1.Format = DateTimePickerFormat.Custom;
                t1.CustomFormat = "dd.MM.yyyy HH:mm";
               
            } else if (e.ClickedItem.Text == "Date")
            {
                t = new DateTimePicker();
                var t1 = t as DateTimePicker;
                t1.Format = DateTimePickerFormat.Custom;
                t1.CustomFormat = "dd.MM.yyyy";
            }
               
            else
            {
                t = new TextBox();
               
            }
            
            var l = new Label();
            w.Controls.Add(t);
            w.Controls.Add(l);
            l.Click += closeBoxSearch;

            t.Size = new Size(151, 22);
            l.Size = new Size(16, 16);
            l.Text = "X";
            t.Location = new Point(17, 25);
            l.Location = new Point(165, 7);
            l.ForeColor = Color.Red;
            t.Visible = true;
            l.Visible = true;

            w.Text = e.ClickedItem.Text;
            w.Visible = true;

        //    MessageBox.Show(panel1.Controls.Count.ToString());

        }

        private void closeBoxSearch(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            if (cboTable.SelectedIndex == 0)
            {
                panel1.Controls.Remove(lbl.Parent);
            }
            else if (cboTable.SelectedIndex == 1)
            {
                panel2.Controls.Remove(lbl.Parent);
            }
           

        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            if (cboTable.SelectedIndex == 0)
            {
                panel1.Controls.Clear();
            }
            else if (cboTable.SelectedIndex == 1)
            {
                panel2.Controls.Clear();
            }
            
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            if (contextMenuStrip2.Items.Count > 0)
                contextMenuStrip2.Items.Clear();

            var lst = panel2.Controls.Cast<GroupBox>().Select(a => a.Text).ToList();
            lst.AddRange(new string[] { "Id", "TotalCount", "Type", "DateCreated","VenNum","VenID","VenName","OptState","SignalType","TestEvent" });

            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                var name = column.DataPropertyName;

                if (!lst.Any(a => a == name))
                {
                    contextMenuStrip2.Items.Add(name);

                }

                //var name2 = name.ToLower();
                //if (name2 == "responsetime")
                //{
                //    if (!lst.Any(a => a == "from ResponseTime"))
                //        contextMenuStrip1.Items.Add("from ResponseTime");

                //    if (!lst.Any(a => a == "to ResponseTime"))
                //        contextMenuStrip1.Items.Add("to ResponseTime");
                //}
                //else if (name2 == "date")
                //{
                //    if (!lst.Any(a => a == "from Date"))
                //        contextMenuStrip1.Items.Add("from Date");

                //    if (!lst.Any(a => a == "to Date"))
                //        contextMenuStrip1.Items.Add("to Date");
                //}
            }

            e.Cancel = false;
        }
        
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (panel2.Controls.Count == 6)
            {
                MessageBox.Show("Không còn chổ để tạo mới.");
                return;
            }
            var w = new GroupBox();
            panel2.Controls.Add(w);
            w.Size = new Size(177, 63);
            w.Location = new Point(7 + (panel2.Controls.Count - 1) * 177, 2);
            Control t;
            if (e.ClickedItem.Text == "StartTime")
            {
                t = new DateTimePicker();
                var t1 = t as DateTimePicker;
                t1.Format = DateTimePickerFormat.Custom;
                t1.CustomFormat = "dd.MM.yyyy";

            }

            else
            {
                t = new TextBox();
               
            }
             
            
            var l = new Label();
            w.Controls.Add(t);
            w.Controls.Add(l);
            l.Click += closeBoxSearch;

            t.Size = new Size(151, 22);
            l.Size = new Size(16, 16);
            l.Text = "X";
            t.Location = new Point(17, 25);
            l.Location = new Point(165, 7);
            l.ForeColor = Color.Red;
            t.Visible = true;
            l.Visible = true;

            w.Text = e.ClickedItem.Text;
            w.Visible = true;
        }

      

        private void btnSearchReport_Click(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
            //btnSearch.PerformClick();
        }

        private void btnClearReport_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure?", "Clear table", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var repo = new LogRepository(this.txtConnectionString.Text);
                repo.ClearTable("Report");
                btnSearchReport.PerformClick();
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {

            string maxrows=null;int m;
            ShowInputDialog(ref maxrows);
            if (string.IsNullOrWhiteSpace(maxrows) || !int.TryParse(maxrows, out m))
            {
                return;
            }
           
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML|*.xml";
            saveFileDialog1.Title = "Export File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                DataTable dT = getDatafromQuery(m);
                dT.TableName = "log";
                DataSet dS = new DataSet();
                dS.Tables.Add(dT);
                var f = System.IO.File.Open(saveFileDialog1.FileName, FileMode.Create);
                dS.WriteXml(f);
                f.Close();
                MessageBox.Show("Finish.");

            }
           
        }



        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
               // if (column.Visible)
               // {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add(column.DataPropertyName);
               // }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }
       
        private DataTable getDatafromQuery(int maxrows=100 )
        {
            var repo = GetRepository();
            Tuple<IEnumerable<Log>, int> result;

            var sortings = new List<SortDescriptor>();
            var arr = (SortField ?? "").Split('-').Select(a => a.ToLower()).ToArray();

            if (arr[0] == "responsetime")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "responseTime" });
            }
            else if (arr[0] == "date")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "Date" });
            }
            else if (arr[0] == "responsecode")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "ResponseCode" });
            }
            else if (arr[0] == "requesttype")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "requestType" });
            }
            else if (arr[0] == "responsetype")
            {
                sortings.Add(new SortDescriptor { Direction = arr[1] == "desc" ? SortDescriptor.SortingDirection.Descending : SortDescriptor.SortingDirection.Ascending, Field = "responseType" });
            }

            else
                sortings.Add(new SortDescriptor { Direction = SortDescriptor.SortingDirection.Descending, Field = "responseTime" });

            //if (!string.IsNullOrWhiteSpace(this.txtSortDesc.Text))
            //    sortings.Add(new SortDescriptor { Direction = SortDescriptor.SortingDirection.Descending, Field = this.txtSortDesc.Text.Trim() });

            var cri = GetCritera();


            if (cri == null)
            {
                MessageBox.Show("Vui lòng nhập số.");

                return null;
            }
            result = repo.FindWithOffsetFetch(cri,
                0,
                maxrows,
                sortings);
           
               
                var dt = ToDataTable(result.Item1.ToList());
                return dt;
            
           
           
         

        }


        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        private  DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(300, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "MaxRows";
            inputBox.StartPosition = FormStartPosition.CenterParent;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ShowInTaskbar = false;


            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmLetterTextEffect().ShowDialog();

        }
    }


    


}
