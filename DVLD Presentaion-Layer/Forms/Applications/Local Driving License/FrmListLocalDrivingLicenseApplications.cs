using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services.New_Driving_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Presentaion_Layer.Forms.Manage_Applications.Local_Forms;
using DVLD_Presentaion_Layer.Forms.Applications.Local_Driving_License;
using DVLD_Presentaion_Layer.Forms.Tests;
using DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses;

namespace DVLD_Presentaion_Layer.Forms.Manage_Applications
{
    public partial class FrmListLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtAllLocشlDrivingLicenseApps;

        public FrmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbAddNewLocalDrivingLicense_Click(object sender, EventArgs e)
        {
           FrmAddEditLocalDrivingLicenseApplication frm = new FrmAddEditLocalDrivingLicenseApplication();
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            _ApplayFilter();
        }

        private void _ApplayFilter()
        {
            string FilterWith = "";

            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                   FilterWith = "LocalDrivingLicenseApplicationID";
                    break;

                case "Full Name":
                    FilterWith = "FullName";
                    break;

                case "National No":
                    FilterWith = "NationalNo";
                    break;

                case "Status":
                    FilterWith = "Status";
                    break;

                default:
                    FilterWith = "None";
                    break;
            }

            if(txtFilterValue.Text == "" || FilterWith == "None")

            {
                _dtAllLocشlDrivingLicenseApps.DefaultView.RowFilter = string.Empty;
                lblTotalRecords.Text = "#Records: " + dgvAllLocalDrivingLicenseApplication.RowCount.ToString();
                return;
            }

            if (FilterWith != "LocalDrivingLicenseApplicationID")
            {
                _dtAllLocشlDrivingLicenseApps.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterWith, txtFilterValue.Text.Trim());
                lblTotalRecords.Text = "#Records: " + dgvAllLocalDrivingLicenseApplication.RowCount.ToString();
            }
            else
            {
                _dtAllLocشlDrivingLicenseApps.DefaultView.RowFilter = string.Format("{0} = {1}", FilterWith, txtFilterValue.Text.Trim());
                lblTotalRecords.Text = "#Records: " + dgvAllLocalDrivingLicenseApplication.RowCount.ToString();
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None") ? true : false;

            if(txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocشlDrivingLicenseApps.DefaultView.RowFilter = "";
            lblTotalRecords.Text = "#Records: " + dgvAllLocalDrivingLicenseApplication.RowCount.ToString();
        }


        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Local Driving License Application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LdlAppID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LdlAppID);

            if (LocalDrivingLicenseApplication != null)
            {

                if (LocalDrivingLicenseApplication.Delete())
                {

                    MessageBox.Show("Local Driving License Application deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to delete the Local Driving License Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Local Driving License Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLocalDrivingLicenseApplicationInfo frm = new FrmLocalDrivingLicenseApplicationInfo(Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LicenseID != -1)
            {

                FrmShowLicenseInfo from = new FrmShowLicenseInfo(LicenseID);
                from.ShowDialog();
            }
            else
            {
                MessageBox.Show("License not found or not issued yet.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void _ScheduleTests(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            FrmListTestAppointments frm = new FrmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEditLocalDrivingLicenseApplication frm = new FrmAddEditLocalDrivingLicenseApplication(Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void FrmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
           _dtAllLocشlDrivingLicenseApps = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvAllLocalDrivingLicenseApplication.DataSource = _dtAllLocشlDrivingLicenseApps;
            cbFilterBy.SelectedIndex = 0;
            lblTotalRecords.Text = "#Reords: " + dgvAllLocalDrivingLicenseApplication.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text =="L.D.L.AppID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void CancelApplicationtoolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);



            if (LocalDrivingLicenseApplication != null) {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to cancel the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ScheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTests(clsTestType.enTestType.VisionTest);
        }

        private void ScheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTests(clsTestType.enTestType.WrittenTest);
        }

        private void ScheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTests(clsTestType.enTestType.StreetTest);
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            short TotalPassedTests = Convert.ToInt16(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value);



            issueToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;



            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            ScheduleTestsMenue.Enabled = !LicenseExists;

            CancelApplicationtoolStripMenuItem6.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            editApplicationToolStripMenuItem.Enabled = !LicenseExists && LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            deleteApplicationToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;


            ScheduleTestsMenue.Enabled = (!clsTest.PassedAllTests(LocalDrivingLicenseApplicationID) &&(LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New));

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.StreetTest);

            if(ScheduleTestsMenue.Enabled)
            {
                ScheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                ScheduleWrittenTestToolStripMenuItem.Enabled =  PassedVisionTest && !PassedWrittenTest;

                ScheduleStreetTestToolStripMenuItem.Enabled =  PassedVisionTest && PassedWrittenTest  && !PassedStreetTest ;
            }

        }

        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            FrmIssueDrivingLicenseFirstTime frm = new FrmIssueDrivingLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvAllLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID( LocalDrivingLicenseApplicationID).ApplicantPersonID;
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
            FrmListLocalDrivingLicenseApplications_Load(null, null);
        }
    }
}
