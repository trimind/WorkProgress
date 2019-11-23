namespace Dapper.PagingSample
{
    partial class frmVencenter
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
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumberVen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumLoads = new System.Windows.Forms.TextBox();
            this.chkHidevens = new System.Windows.Forms.CheckBox();
            this.btnCreateVen = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalIDs = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxthread = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(113, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(12, 327);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 99);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stop vens running";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "URL:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(158, 89);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(191, 22);
            this.txtUrl.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Number of process";
            // 
            // txtNumberVen
            // 
            this.txtNumberVen.Location = new System.Drawing.Point(158, 122);
            this.txtNumberVen.Name = "txtNumberVen";
            this.txtNumberVen.Size = new System.Drawing.Size(191, 22);
            this.txtNumberVen.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Num loads per ven";
            // 
            // txtNumLoads
            // 
            this.txtNumLoads.Location = new System.Drawing.Point(158, 178);
            this.txtNumLoads.Name = "txtNumLoads";
            this.txtNumLoads.Size = new System.Drawing.Size(191, 22);
            this.txtNumLoads.TabIndex = 9;
            // 
            // chkHidevens
            // 
            this.chkHidevens.AutoSize = true;
            this.chkHidevens.Location = new System.Drawing.Point(258, 206);
            this.chkHidevens.Name = "chkHidevens";
            this.chkHidevens.Size = new System.Drawing.Size(91, 21);
            this.chkHidevens.TabIndex = 10;
            this.chkHidevens.Text = "hide vens";
            this.chkHidevens.UseVisualStyleBackColor = true;
            this.chkHidevens.Visible = false;
            // 
            // btnCreateVen
            // 
            this.btnCreateVen.Location = new System.Drawing.Point(113, 246);
            this.btnCreateVen.Name = "btnCreateVen";
            this.btnCreateVen.Size = new System.Drawing.Size(150, 44);
            this.btnCreateVen.TabIndex = 4;
            this.btnCreateVen.Text = "Create vens";
            this.btnCreateVen.UseVisualStyleBackColor = true;
            this.btnCreateVen.Click += new System.EventHandler(this.btnCreateVen_ClickAsync);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 33);
            this.button1.TabIndex = 11;
            this.button1.Text = "add list";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaxthread);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblTotalIDs);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnCreateVen);
            this.groupBox1.Controls.Add(this.chkHidevens);
            this.groupBox1.Controls.Add(this.txtNumLoads);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNumberVen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 309);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "create vens";
            // 
            // lblTotalIDs
            // 
            this.lblTotalIDs.AutoSize = true;
            this.lblTotalIDs.Location = new System.Drawing.Point(155, 53);
            this.lblTotalIDs.Name = "lblTotalIDs";
            this.lblTotalIDs.Size = new System.Drawing.Size(16, 17);
            this.lblTotalIDs.TabIndex = 13;
            this.lblTotalIDs.Text = "#";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Create list ID:";
            // 
            // txtMaxthread
            // 
            this.txtMaxthread.Location = new System.Drawing.Point(158, 150);
            this.txtMaxthread.Name = "txtMaxthread";
            this.txtMaxthread.Size = new System.Drawing.Size(191, 22);
            this.txtMaxthread.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-6, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "MaxThread per process";
            // 
            // frmVencenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 436);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVencenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ven Center";
            this.Load += new System.EventHandler(this.frmVencenter_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumberVen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNumLoads;
        private System.Windows.Forms.CheckBox chkHidevens;
        private System.Windows.Forms.Button btnCreateVen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalIDs;
        private System.Windows.Forms.TextBox txtMaxthread;
        private System.Windows.Forms.Label label2;
    }
}