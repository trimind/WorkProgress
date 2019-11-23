namespace MiniTool
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lConnectionString = new System.Windows.Forms.Label();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.btnExportXML = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearlog = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnListCols = new System.Windows.Forms.Button();
            this.gbPaging = new System.Windows.Forms.GroupBox();
            this.lTotalCountValue = new System.Windows.Forms.Label();
            this.lTotalCount = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lPageSize = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.lPageIndex = new System.Windows.Forms.Label();
            this.txtPageIndex = new System.Windows.Forms.TextBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpEvent = new System.Windows.Forms.GroupBox();
            this.btnClearEvent = new System.Windows.Forms.Button();
            this.btnSearchEvent = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpenVenCenter = new System.Windows.Forms.Button();
            this.lblVensRunning = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gpReport = new System.Windows.Forms.GroupBox();
            this.btnClearReport = new System.Windows.Forms.Button();
            this.btnSearchReport = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblProcessRunning = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gbConnection.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.gbPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.grpEvent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gpReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(132, 36);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(4);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(679, 22);
            this.txtConnectionString.TabIndex = 0;
            // 
            // lConnectionString
            // 
            this.lConnectionString.AutoSize = true;
            this.lConnectionString.Location = new System.Drawing.Point(8, 39);
            this.lConnectionString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lConnectionString.Name = "lConnectionString";
            this.lConnectionString.Size = new System.Drawing.Size(120, 17);
            this.lConnectionString.TabIndex = 2;
            this.lConnectionString.Text = "ConnectionString:";
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.lConnectionString);
            this.gbConnection.Controls.Add(this.txtConnectionString);
            this.gbConnection.Location = new System.Drawing.Point(1305, 81);
            this.gbConnection.Margin = new System.Windows.Forms.Padding(4);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Padding = new System.Windows.Forms.Padding(4);
            this.gbConnection.Size = new System.Drawing.Size(1335, 85);
            this.gbConnection.TabIndex = 4;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Database";
            this.gbConnection.Visible = false;
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.btnExportXML);
            this.grpLog.Controls.Add(this.panel1);
            this.grpLog.Controls.Add(this.btnClearlog);
            this.grpLog.Controls.Add(this.btnSearch);
            this.grpLog.Location = new System.Drawing.Point(15, 71);
            this.grpLog.Margin = new System.Windows.Forms.Padding(4);
            this.grpLog.Name = "grpLog";
            this.grpLog.Padding = new System.Windows.Forms.Padding(4);
            this.grpLog.Size = new System.Drawing.Size(777, 275);
            this.grpLog.TabIndex = 5;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Logs Table";
            // 
            // btnExportXML
            // 
            this.btnExportXML.Location = new System.Drawing.Point(568, 236);
            this.btnExportXML.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportXML.Name = "btnExportXML";
            this.btnExportXML.Size = new System.Drawing.Size(97, 31);
            this.btnExportXML.TabIndex = 15;
            this.btnExportXML.Text = "Export XML";
            this.btnExportXML.UseVisualStyleBackColor = true;
            this.btnExportXML.Click += new System.EventHandler(this.btnExportXML_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(7, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 181);
            this.panel1.TabIndex = 14;
            // 
            // btnClearlog
            // 
            this.btnClearlog.Location = new System.Drawing.Point(673, 236);
            this.btnClearlog.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearlog.Name = "btnClearlog";
            this.btnClearlog.Size = new System.Drawing.Size(97, 31);
            this.btnClearlog.TabIndex = 8;
            this.btnClearlog.Text = "Clear data";
            this.btnClearlog.UseVisualStyleBackColor = true;
            this.btnClearlog.Click += new System.EventHandler(this.btnClearlog_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(463, 236);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 31);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnListCols
            // 
            this.btnListCols.Location = new System.Drawing.Point(85, 38);
            this.btnListCols.Margin = new System.Windows.Forms.Padding(4);
            this.btnListCols.Name = "btnListCols";
            this.btnListCols.Size = new System.Drawing.Size(25, 34);
            this.btnListCols.TabIndex = 9;
            this.btnListCols.Text = "+";
            this.btnListCols.UseVisualStyleBackColor = true;
            this.btnListCols.Click += new System.EventHandler(this.btnListCols_Click);
            // 
            // gbPaging
            // 
            this.gbPaging.Controls.Add(this.lTotalCountValue);
            this.gbPaging.Controls.Add(this.lTotalCount);
            this.gbPaging.Controls.Add(this.btnPrevious);
            this.gbPaging.Controls.Add(this.btnNext);
            this.gbPaging.Controls.Add(this.lPageSize);
            this.gbPaging.Controls.Add(this.txtPageSize);
            this.gbPaging.Controls.Add(this.lPageIndex);
            this.gbPaging.Controls.Add(this.txtPageIndex);
            this.gbPaging.Location = new System.Drawing.Point(801, 183);
            this.gbPaging.Margin = new System.Windows.Forms.Padding(4);
            this.gbPaging.Name = "gbPaging";
            this.gbPaging.Padding = new System.Windows.Forms.Padding(4);
            this.gbPaging.Size = new System.Drawing.Size(431, 138);
            this.gbPaging.TabIndex = 6;
            this.gbPaging.TabStop = false;
            this.gbPaging.Text = "Paging";
            // 
            // lTotalCountValue
            // 
            this.lTotalCountValue.AutoSize = true;
            this.lTotalCountValue.Location = new System.Drawing.Point(104, 72);
            this.lTotalCountValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTotalCountValue.Name = "lTotalCountValue";
            this.lTotalCountValue.Size = new System.Drawing.Size(0, 17);
            this.lTotalCountValue.TabIndex = 15;
            // 
            // lTotalCount
            // 
            this.lTotalCount.AutoSize = true;
            this.lTotalCount.Location = new System.Drawing.Point(12, 71);
            this.lTotalCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTotalCount.Name = "lTotalCount";
            this.lTotalCount.Size = new System.Drawing.Size(85, 17);
            this.lTotalCount.TabIndex = 14;
            this.lTotalCount.Text = "Total Count:";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(189, 97);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(113, 28);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "Previous Page";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(310, 97);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 28);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "Next Page";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lPageSize
            // 
            this.lPageSize.AutoSize = true;
            this.lPageSize.Location = new System.Drawing.Point(156, 32);
            this.lPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPageSize.Name = "lPageSize";
            this.lPageSize.Size = new System.Drawing.Size(76, 17);
            this.lPageSize.TabIndex = 3;
            this.lPageSize.Text = "Page Size:";
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(241, 28);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(48, 22);
            this.txtPageSize.TabIndex = 3;
            this.txtPageSize.Text = "10";
            // 
            // lPageIndex
            // 
            this.lPageIndex.AutoSize = true;
            this.lPageIndex.Location = new System.Drawing.Point(13, 32);
            this.lPageIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPageIndex.Name = "lPageIndex";
            this.lPageIndex.Size = new System.Drawing.Size(82, 17);
            this.lPageIndex.TabIndex = 3;
            this.lPageIndex.Text = "Page Index:";
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(107, 28);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(4);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(40, 22);
            this.txtPageIndex.TabIndex = 3;
            this.txtPageIndex.Text = "0";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(15, 329);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1217, 312);
            this.dgvData.TabIndex = 9;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_ColumnHeaderMouseClick);
            this.dgvData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDoubleClick);
            // 
            // cboTable
            // 
            this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Items.AddRange(new object[] {
            "Log",
            "Event"});
            this.cboTable.Location = new System.Drawing.Point(86, 12);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(183, 24);
            this.cboTable.TabIndex = 10;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Table";
            // 
            // grpEvent
            // 
            this.grpEvent.Controls.Add(this.btnClearEvent);
            this.grpEvent.Controls.Add(this.btnSearchEvent);
            this.grpEvent.Controls.Add(this.panel2);
            this.grpEvent.Location = new System.Drawing.Point(1267, 49);
            this.grpEvent.Margin = new System.Windows.Forms.Padding(4);
            this.grpEvent.Name = "grpEvent";
            this.grpEvent.Padding = new System.Windows.Forms.Padding(4);
            this.grpEvent.Size = new System.Drawing.Size(782, 272);
            this.grpEvent.TabIndex = 8;
            this.grpEvent.TabStop = false;
            this.grpEvent.Text = "Event Table";
            this.grpEvent.Visible = false;
            // 
            // btnClearEvent
            // 
            this.btnClearEvent.Location = new System.Drawing.Point(684, 233);
            this.btnClearEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearEvent.Name = "btnClearEvent";
            this.btnClearEvent.Size = new System.Drawing.Size(97, 31);
            this.btnClearEvent.TabIndex = 9;
            this.btnClearEvent.Text = "Clear data";
            this.btnClearEvent.UseVisualStyleBackColor = true;
            this.btnClearEvent.Click += new System.EventHandler(this.btnClearEvent_Click);
            // 
            // btnSearchEvent
            // 
            this.btnSearchEvent.Location = new System.Drawing.Point(571, 233);
            this.btnSearchEvent.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchEvent.Name = "btnSearchEvent";
            this.btnSearchEvent.Size = new System.Drawing.Size(103, 31);
            this.btnSearchEvent.TabIndex = 7;
            this.btnSearchEvent.Text = "Search";
            this.btnSearchEvent.UseVisualStyleBackColor = true;
            this.btnSearchEvent.Click += new System.EventHandler(this.btnSearchEvent_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(7, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(763, 181);
            this.panel2.TabIndex = 15;
            // 
            // btnOpenVenCenter
            // 
            this.btnOpenVenCenter.Location = new System.Drawing.Point(256, 51);
            this.btnOpenVenCenter.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenVenCenter.Name = "btnOpenVenCenter";
            this.btnOpenVenCenter.Size = new System.Drawing.Size(144, 39);
            this.btnOpenVenCenter.TabIndex = 11;
            this.btnOpenVenCenter.Text = "Manager";
            this.btnOpenVenCenter.UseVisualStyleBackColor = true;
            this.btnOpenVenCenter.Click += new System.EventHandler(this.btnOpenVenCenter_Click);
            // 
            // lblVensRunning
            // 
            this.lblVensRunning.AutoSize = true;
            this.lblVensRunning.BackColor = System.Drawing.Color.Red;
            this.lblVensRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVensRunning.Location = new System.Drawing.Point(26, 45);
            this.lblVensRunning.Name = "lblVensRunning";
            this.lblVensRunning.Size = new System.Drawing.Size(112, 20);
            this.lblVensRunning.TabIndex = 12;
            this.lblVensRunning.Text = "Vens running:";
            this.lblVensRunning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(112, 38);
            this.btnClearSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(25, 34);
            this.btnClearSearch.TabIndex = 13;
            this.btnClearSearch.Text = "X";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblProcessRunning);
            this.groupBox1.Controls.Add(this.btnOpenVenCenter);
            this.groupBox1.Controls.Add(this.lblVensRunning);
            this.groupBox1.Location = new System.Drawing.Point(801, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 128);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VEN Manager";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            this.contextMenuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // gpReport
            // 
            this.gpReport.Controls.Add(this.btnClearReport);
            this.gpReport.Controls.Add(this.btnSearchReport);
            this.gpReport.Controls.Add(this.panel3);
            this.gpReport.Location = new System.Drawing.Point(1255, 356);
            this.gpReport.Margin = new System.Windows.Forms.Padding(4);
            this.gpReport.Name = "gpReport";
            this.gpReport.Padding = new System.Windows.Forms.Padding(4);
            this.gpReport.Size = new System.Drawing.Size(782, 272);
            this.gpReport.TabIndex = 16;
            this.gpReport.TabStop = false;
            this.gpReport.Text = "Report";
            this.gpReport.Visible = false;
            // 
            // btnClearReport
            // 
            this.btnClearReport.Location = new System.Drawing.Point(684, 233);
            this.btnClearReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearReport.Name = "btnClearReport";
            this.btnClearReport.Size = new System.Drawing.Size(97, 31);
            this.btnClearReport.TabIndex = 9;
            this.btnClearReport.Text = "Clear data";
            this.btnClearReport.UseVisualStyleBackColor = true;
            this.btnClearReport.Click += new System.EventHandler(this.btnClearReport_Click);
            // 
            // btnSearchReport
            // 
            this.btnSearchReport.Location = new System.Drawing.Point(571, 233);
            this.btnSearchReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchReport.Name = "btnSearchReport";
            this.btnSearchReport.Size = new System.Drawing.Size(103, 31);
            this.btnSearchReport.TabIndex = 7;
            this.btnSearchReport.Text = "Search";
            this.btnSearchReport.UseVisualStyleBackColor = true;
            this.btnSearchReport.Click += new System.EventHandler(this.btnSearchReport_Click);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(7, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(763, 181);
            this.panel3.TabIndex = 15;
            // 
            // lblProcessRunning
            // 
            this.lblProcessRunning.AutoSize = true;
            this.lblProcessRunning.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lblProcessRunning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessRunning.Location = new System.Drawing.Point(6, 78);
            this.lblProcessRunning.Name = "lblProcessRunning";
            this.lblProcessRunning.Size = new System.Drawing.Size(112, 20);
            this.lblProcessRunning.TabIndex = 13;
            this.lblProcessRunning.Text = "Vens running:";
            this.lblProcessRunning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(1209, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 34);
            this.button1.TabIndex = 17;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 641);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.btnListCols);
            this.Controls.Add(this.grpEvent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.gbPaging);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.gbConnection);
            this.Controls.Add(this.gpReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "MiniTool";
            this.Load += new System.EventHandler(this.Main_Load);
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.gbPaging.ResumeLayout(false);
            this.gbPaging.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.grpEvent.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpReport.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lConnectionString;
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.GroupBox gbPaging;
        private System.Windows.Forms.Label lPageIndex;
        private System.Windows.Forms.Label lPageSize;
        private System.Windows.Forms.TextBox txtPageIndex;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lTotalCount;
        private System.Windows.Forms.Label lTotalCountValue;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpEvent;
        private System.Windows.Forms.Button btnSearchEvent;
        private System.Windows.Forms.Button btnOpenVenCenter;
        private System.Windows.Forms.Label lblVensRunning;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnClearlog;
        private System.Windows.Forms.Button btnClearEvent;
        private System.Windows.Forms.Button btnListCols;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.FlowLayoutPanel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.FlowLayoutPanel panel2;
        private System.Windows.Forms.GroupBox gpReport;
        private System.Windows.Forms.Button btnClearReport;
        private System.Windows.Forms.Button btnSearchReport;
        private System.Windows.Forms.FlowLayoutPanel panel3;
        private System.Windows.Forms.Button btnExportXML;
        private System.Windows.Forms.Label lblProcessRunning;
        private System.Windows.Forms.Button button1;
    }
}

