using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.People;
using DVLD_Presentaion_Layer.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication _Application;
        private int _ApplicationID = -1;

        public int ApplicationID
        {
            get { return _ApplicationID; }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;
            lblApplicationID.Text = "[????]";
            lblApplicationDate.Text = "[????]";
            lblApplicationStatus.Text = "[????]";
            lblApplicationType.Text = "[????]";
            lblPersonName.Text = "[????]";
            lblApplicationFees.Text = "[????]";
            lblApplicationLastStatusDate.Text = "[????]";
            lblCreatedByUserID.Text = "[????]";
        }

        public void LoadBasicApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                MessageBox.Show("Could Not Find This Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetApplicationInfo();
                return;
            }
            else
            {
                _FillApplicationInfo();
            }
        }

        private void _FillApplicationInfo()
        {
            lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicationDate.Text =clsFormat.DateToShort( _Application.ApplicationDate);
            lblApplicationStatus.Text = _Application.ApplicationStatus.ToString();
            lblApplicationType.Text = _Application.ApplicationTypeInfo.Title;
            lblPersonName.Text = _Application.ApplicantFullName;
            lblApplicationFees.Text = _Application.ApplicationTypeInfo.Fees.ToString();
            lblApplicationLastStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedByUserID.Text = _Application.UserInfo.UserName;
            _ApplicationID = _Application.ApplicationID;

        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void LinklblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonDetails Frm = new FrmShowPersonDetails(_Application.ApplicantPersonID);
            Frm.ShowDialog();

            LoadBasicApplicationInfo(_ApplicationID);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
