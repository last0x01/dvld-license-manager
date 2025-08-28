using DVLD.Tests;
using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Applications.International_License;
using DVLD_Presentaion_Layer.Forms.Applications.Release_Detained_License;
using DVLD_Presentaion_Layer.Forms.Applications.Renew_Local_License;
using DVLD_Presentaion_Layer.Forms.Applications.ReplaceForLostOrDamagedLicense;
using DVLD_Presentaion_Layer.Forms.ApplicationsType;
using DVLD_Presentaion_Layer.Forms.Drivers;
using DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services.New_Driving_License;
using DVLD_Presentaion_Layer.Forms.Manage_Applications;
using DVLD_Presentaion_Layer.Forms.Users;
using DVLD_Presentaion_Layer.Global_Classes;
using System;

using System.Windows.Forms;

namespace DVLD_Presentaion_Layer
{
    public partial class MainMenu : Form
    { 
        FrmLoginScreen _FrmLogin;

        
        public MainMenu(FrmLoginScreen frmLogin)
        {
            InitializeComponent();

            _FrmLogin = frmLogin;
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListUsers frm = new FrmListUsers();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListPeople frm = new FrmListPeople();
            frm.ShowDialog();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangePassword frmChangePassword = new FrmChangePassword(clsGlobal.CurrentUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void currrentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowUserInfo frmShowUserDetails = new FrmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frmShowUserDetails.ShowDialog();
        }

        private void manageApplicationsTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListApplicationTypes frm = new FrmListApplicationTypes();

            frm.ShowDialog();

        }

        private void listApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;

            _FrmLogin.Show();
            this.Close();

        }

      

        private void LocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEditLocalDrivingLicenseApplication frmLocalLicense = new FrmAddEditLocalDrivingLicenseApplication();
            frmLocalLicense.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNewInternationalLicense frmInternationalLicense = new FrmNewInternationalLicense();
            frmInternationalLicense.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListLocalDrivingLicenseApplications frm = new FrmListLocalDrivingLicenseApplications();
            frm.ShowDialog();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListDrivers frm = new FrmListDrivers();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListLocalDrivingLicenseApplications frm = new FrmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void ManageInternationalLicenses_Click(object sender, EventArgs e)
        {
            FrmListInternationalLicenses frm = new FrmListInternationalLicenses();
            frm.ShowDialog();
        }

        private void rnewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRenewLocalDrivingLicenseApplication frm = new FrmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog(); 
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReplaceForLostOrDamageLicense frm = new FrmReplaceForLostOrDamageLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDetainedLicenseApplication frmDetainedLicense = new FrmDetainedLicenseApplication();
            frmDetainedLicense.ShowDialog();
        }

        private void releasedDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainedLicenseApplication frm = new FrmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListDetainedLicenses frm = new FrmListDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
