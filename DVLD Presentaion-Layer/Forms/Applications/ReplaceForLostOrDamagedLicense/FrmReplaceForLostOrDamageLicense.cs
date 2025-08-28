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

namespace DVLD_Presentaion_Layer.Forms.Applications.ReplaceForLostOrDamagedLicense
{
    public partial class FrmReplaceForLostOrDamageLicense : Form
    {
        int _NewLicenseID = -1;

        clsLicense.enIssueReason _IssueReason = clsLicense.enIssueReason.DamagedReplacement;
        public FrmReplaceForLostOrDamageLicense()
        {
            InitializeComponent();
        }

        private clsLicense.enIssueReason _GetIssueReason()
        {
                       if (rbDamagedLicense.Checked)
                return clsLicense.enIssueReason.DamagedReplacement;
            else
                return clsLicense.enIssueReason.LostReplacement;
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDrivingLicenseForDamaged;
            else
                return (int)clsApplication.enApplicationType.ReplaceDrivingLicenseForLost;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnSelectedLicense(int obj)
        {

            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            LinklblLicensesHistory.Enabled = (SelectedLicenseID != -1);


            if (SelectedLicenseID == -1)
            {
                return;
            }

            if(!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active, you can not replace it.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplacementLicense.Enabled = false;
                return;
            }

            LinklblLicensesHistory.Enabled = (SelectedLicenseID != -1);

            btnReplacementLicense.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReplaceForLostOrDamageLicense_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFoucs();
            rbDamagedLicense.Checked = true;
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReplaceDrivingLicenseForDamaged).Fees.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now); 
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        private void LinklblLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          //  FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory();
            

        }

        private void LinklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmShowLicenseInfo frm = new FrmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();

        }

        private void btnReplacementLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Replace the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

          


            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Failed to replace the license, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();

            _NewLicenseID = NewLicense.LicenseID;

            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplacementLicense.Enabled = false;
            LinklblNewLicenseInfo.Enabled = true;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
                _IssueReason = clsLicense.enIssueReason.DamagedReplacement;
                
                lblTitle.Text = "Replacement For Damaged License";
                this.Text = lblTitle.Text;
                lblTitle.Location = new Point(149, 9);
                lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
           
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _IssueReason = clsLicense.enIssueReason.LostReplacement;
            lblTitle.Text = "Replacement For Lost License";
            this.Text = lblTitle.Text;
            lblTitle.Location = new Point(181, 9);
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        
    }
    }
}
