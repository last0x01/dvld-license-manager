using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Applications.Local_Driving_License
{
    public partial class ctrlLocalDrivingLicenseApplicationInfo : UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
          
        }

        public ctrlLocalDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfoByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Could Not Find This Local Driving License Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
            else
            {
                _FillLocalDrivingLicenseApplicationInfo();
            }

        }


        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Could Not Find This Local Driving License Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDefaultValues();
                return;
            }
            else
            {
                _FillLocalDrivingLicenseApplicationInfo();
            }

        }

        private void _ResetDefaultValues()
        {
          
            _LocalDrivingLicenseApplicationID = -1;
            lblLicenseClassName.Text = "[????]";
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblPassedTests.Text = "[????]";
            ctrlApplicationBasicInfo1.ResetApplicationInfo();



        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            lblLicenseClassName.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = clsTest.GetPassedTestCount(_LocalDrivingLicenseApplicationID).ToString() + "/3";
            ctrlApplicationBasicInfo1.LoadBasicApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

     

        private void ctrlLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
