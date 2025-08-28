namespace DVLD_Presentaion_Layer.Forms.Manage_Applications
{
    partial class FrmListLocalDrivingLicenseApplications
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
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.dgvAllLocalDrivingLicenseApplication = new System.Windows.Forms.DataGridView();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.editApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelApplicationtoolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ScheduleTestsMenue = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScheduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.issueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbAddNewLocalDrivingLicense = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalDrivingLicenseApplication)).BeginInit();
            this.cmsApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewLocalDrivingLicense)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(12, 652);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(106, 20);
            this.lblTotalRecords.TabIndex = 17;
            this.lblTotalRecords.Text = "# Records 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(500, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 36);
            this.label2.TabIndex = 14;
            this.label2.Text = "Local Driving License Application";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DisplayMember = "First Name";
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(87, 207);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(147, 28);
            this.cbFilterBy.TabIndex = 13;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFindBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Find By";
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(250, 208);
            this.txtFilterValue.Multiline = true;
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(215, 26);
            this.txtFilterValue.TabIndex = 11;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFindBy_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // dgvAllLocalDrivingLicenseApplication
            // 
            this.dgvAllLocalDrivingLicenseApplication.AllowUserToAddRows = false;
            this.dgvAllLocalDrivingLicenseApplication.AllowUserToDeleteRows = false;
            this.dgvAllLocalDrivingLicenseApplication.AllowUserToOrderColumns = true;
            this.dgvAllLocalDrivingLicenseApplication.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllLocalDrivingLicenseApplication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllLocalDrivingLicenseApplication.ContextMenuStrip = this.cmsApplications;
            this.dgvAllLocalDrivingLicenseApplication.Location = new System.Drawing.Point(12, 255);
            this.dgvAllLocalDrivingLicenseApplication.Name = "dgvAllLocalDrivingLicenseApplication";
            this.dgvAllLocalDrivingLicenseApplication.ReadOnly = true;
            this.dgvAllLocalDrivingLicenseApplication.RowHeadersWidth = 62;
            this.dgvAllLocalDrivingLicenseApplication.RowTemplate.Height = 28;
            this.dgvAllLocalDrivingLicenseApplication.Size = new System.Drawing.Size(1354, 371);
            this.dgvAllLocalDrivingLicenseApplication.TabIndex = 9;
            // 
            // cmsApplications
            // 
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.editApplicationToolStripMenuItem,
            this.deleteApplicationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.CancelApplicationtoolStripMenuItem6,
            this.toolStripMenuItem7,
            this.ScheduleTestsMenue,
            this.toolStripMenuItem3,
            this.issueToolStripMenuItem,
            this.toolStripMenuItem4,
            this.showLicenseToolStripMenuItem,
            this.toolStripMenuItem5,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.Size = new System.Drawing.Size(347, 329);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.PersonDetails_322;
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.showApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.showApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(343, 6);
            // 
            // editApplicationToolStripMenuItem
            // 
            this.editApplicationToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.edit_322;
            this.editApplicationToolStripMenuItem.Name = "editApplicationToolStripMenuItem";
            this.editApplicationToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.editApplicationToolStripMenuItem.Text = "Edit Application";
            this.editApplicationToolStripMenuItem.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // deleteApplicationToolStripMenuItem
            // 
            this.deleteApplicationToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Delete_32_2;
            this.deleteApplicationToolStripMenuItem.Name = "deleteApplicationToolStripMenuItem";
            this.deleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.deleteApplicationToolStripMenuItem.Text = "Delete Application";
            this.deleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(343, 6);
            // 
            // CancelApplicationtoolStripMenuItem6
            // 
            this.CancelApplicationtoolStripMenuItem6.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Delete_321;
            this.CancelApplicationtoolStripMenuItem6.Name = "CancelApplicationtoolStripMenuItem6";
            this.CancelApplicationtoolStripMenuItem6.Size = new System.Drawing.Size(346, 32);
            this.CancelApplicationtoolStripMenuItem6.Text = "Cancel Application";
            this.CancelApplicationtoolStripMenuItem6.Click += new System.EventHandler(this.CancelApplicationtoolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(343, 6);
            // 
            // ScheduleTestsMenue
            // 
            this.ScheduleTestsMenue.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScheduleVisionTestToolStripMenuItem,
            this.ScheduleWrittenTestToolStripMenuItem,
            this.ScheduleStreetTestToolStripMenuItem});
            this.ScheduleTestsMenue.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Schedule_Test_32;
            this.ScheduleTestsMenue.Name = "ScheduleTestsMenue";
            this.ScheduleTestsMenue.Size = new System.Drawing.Size(346, 32);
            this.ScheduleTestsMenue.Text = "Schedule Tests";
            // 
            // ScheduleVisionTestToolStripMenuItem
            // 
            this.ScheduleVisionTestToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Vision_Test_Schdule;
            this.ScheduleVisionTestToolStripMenuItem.Name = "ScheduleVisionTestToolStripMenuItem";
            this.ScheduleVisionTestToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
            this.ScheduleVisionTestToolStripMenuItem.Text = "Schedule Vision Test";
            this.ScheduleVisionTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleVisionTestToolStripMenuItem_Click);
            // 
            // ScheduleWrittenTestToolStripMenuItem
            // 
            this.ScheduleWrittenTestToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Written_Test_32;
            this.ScheduleWrittenTestToolStripMenuItem.Name = "ScheduleWrittenTestToolStripMenuItem";
            this.ScheduleWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
            this.ScheduleWrittenTestToolStripMenuItem.Text = "Schedule Written Test";
            this.ScheduleWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleWrittenTestToolStripMenuItem_Click);
            // 
            // ScheduleStreetTestToolStripMenuItem
            // 
            this.ScheduleStreetTestToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Street_Test_32;
            this.ScheduleStreetTestToolStripMenuItem.Name = "ScheduleStreetTestToolStripMenuItem";
            this.ScheduleStreetTestToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
            this.ScheduleStreetTestToolStripMenuItem.Text = "Schedule Street Test";
            this.ScheduleStreetTestToolStripMenuItem.Click += new System.EventHandler(this.ScheduleStreetTestToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(343, 6);
            // 
            // issueToolStripMenuItem
            // 
            this.issueToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.IssueDrivingLicense_32;
            this.issueToolStripMenuItem.Name = "issueToolStripMenuItem";
            this.issueToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.issueToolStripMenuItem.Text = "Issue Driving License (First Time)";
            this.issueToolStripMenuItem.Click += new System.EventHandler(this.issueToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(343, 6);
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.License_View_321;
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.showLicenseToolStripMenuItem.Text = "Show License";
            this.showLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(343, 6);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::DVLD_Presentaion_Layer.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(346, 32);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(779, 62);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(52, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Close_32;
            this.btnClose.Location = new System.Drawing.Point(1266, 642);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Presentaion_Layer.Properties.Resources.Applications1;
            this.pictureBox1.Location = new System.Drawing.Point(657, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // pbAddNewLocalDrivingLicense
            // 
            this.pbAddNewLocalDrivingLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAddNewLocalDrivingLicense.Image = global::DVLD_Presentaion_Layer.Properties.Resources.New_Application_641;
            this.pbAddNewLocalDrivingLicense.Location = new System.Drawing.Point(1282, 189);
            this.pbAddNewLocalDrivingLicense.Name = "pbAddNewLocalDrivingLicense";
            this.pbAddNewLocalDrivingLicense.Size = new System.Drawing.Size(84, 46);
            this.pbAddNewLocalDrivingLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAddNewLocalDrivingLicense.TabIndex = 10;
            this.pbAddNewLocalDrivingLicense.TabStop = false;
            this.pbAddNewLocalDrivingLicense.Click += new System.EventHandler(this.pbAddNewLocalDrivingLicense_Click);
            // 
            // FrmListLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 694);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.pbAddNewLocalDrivingLicense);
            this.Controls.Add(this.dgvAllLocalDrivingLicenseApplication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmListLocalDrivingLicenseApplications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLocalDrivingLicenseApplications";
            this.Load += new System.EventHandler(this.FrmListLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalDrivingLicenseApplication)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddNewLocalDrivingLicense)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.PictureBox pbAddNewLocalDrivingLicense;
        private System.Windows.Forms.DataGridView dgvAllLocalDrivingLicenseApplication;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ScheduleTestsMenue;
        private System.Windows.Forms.ToolStripMenuItem ScheduleVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScheduleStreetTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem issueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelApplicationtoolStripMenuItem6;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    }
}