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
    public partial class FrmDetainedLicenseApplication : Form
    {
        int _DetainID = -1; 
        int _SelectedLicenseID = -1; 
        public FrmDetainedLicenseApplication()
        {
            InitializeComponent();


        }

        private void LinklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseInfo frm = new FrmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();

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





            bool IsDetained = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetainedLicense;

            if (IsDetained)
            {
                MessageBox.Show("This License all ready detained, choose another one. " ,"License Allready Detained", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            txtFineFees.Focus();
            btnDetain.Enabled = true;

        }

        private void FrmDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void LinklblLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            }
            ;


            if (!clsValidation.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            }
            ;

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fix the errors before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm Detention", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            _DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.
                Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Failed to Detain License. Please try again.", "Detention Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text = _DetainID.ToString();

            MessageBox.Show("License Detained Successfully with Detain ID = " + _DetainID, "Detention Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            txtFineFees.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            LinklblNewLicenseInfo.Enabled = true;

        }

        private void FrmDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFoucs();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
