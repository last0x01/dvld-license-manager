using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Applications.Controls;
using DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services;
using DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses;
using DVLD_Presentaion_Layer.Global_Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Applications.International_License
{
    public partial class FrmNewInternationalLicense : Form
    {
        int _InternationalLicenseID = -1;

        public FrmNewInternationalLicense()
        {
            InitializeComponent();
        }

        public FrmNewInternationalLicense(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;

        }



        private void FrmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));//add one year.
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void driverLicenseInfoWithFilter1_OnSelectedLicense(int obj)
        {
            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();

            LinklblLicensesHistory.Enabled = (SelectedLicenseID != -1);

            if(SelectedLicenseID == -1)
            {
                return;
            }


            if (ctrldriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassID != (int)clsLicenseClass.enLicenseClass.OrdinaryDrivingLicense)
            {
                MessageBox.Show("This is not an ordinary driving license, cannot issue an international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrldriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternationalLicenseID !=-1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LinklblLicensesHistory.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;

        }



        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrldriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalDrivingLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.LastStatusDate = DateTime.Now;


            InternationalLicense.IssuedUsingLocalLicenseID = ctrldriverLicenseInfoWithFilter1.LicenseID;
            InternationalLicense.IsActive = true;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.DriverID = ctrldriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.DriverID;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Failed to issue the international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
          
            MessageBox.Show("International License issued successfully with ID = " + InternationalLicense.InternationalLicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ctrldriverLicenseInfoWithFilter1.FilterEnabled = false;
            btnIssueLicense.Enabled = false;
            LinklblNewLicenseInfo.Enabled = true;
        }

        private void LinklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInternationalLicenseInfo frm = new FrmInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();

        }

        private void LinklblLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(ctrldriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }
    }
}
