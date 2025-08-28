using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses;
using DVLD_Presentaion_Layer.Forms.Manage_Applications.Local_Forms;
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

namespace DVLD_Presentaion_Layer.Forms.Applications.Release_Detained_License
{
    public partial class FrmReleaseDetainedLicenseApplication : Form
    {
       
        int _SelectedLicenseID = -1;
        public FrmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public FrmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();

            _SelectedLicenseID = LicenseID;

            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(LicenseID);

            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this license?",
                "Release", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            bool IsReleased = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo
                .ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            if (!IsReleased)
            {
                MessageBox.Show("Failed to release the license.");
                return;
            }
             lblApplicationID.Text = ApplicationID.ToString();

            MessageBox.Show("License released successfully.");
            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            LinklblNewLicenseInfo.Enabled = true;
        }

        private void LinklblLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void LinklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseInfo frm = new FrmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();

        }

        private void FrmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
           

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnSelectedLicense(int obj)
        {
             _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            LinklblLicensesHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
            {
                return;
            }



          

            bool IsDetainedLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetainedLicense;
            if (!IsDetainedLicense)
            {
                MessageBox.Show("The license does not detained with LicenseID= " + _SelectedLicenseID, "Error Validate", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainID.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicesne).Fees.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text =( ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            

            btnRelease.Enabled = true;

        }

       
    }
}
