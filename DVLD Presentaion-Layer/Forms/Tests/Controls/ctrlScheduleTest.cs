using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Global_Classes;
using DVLD_Presentaion_Layer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
           
        }
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }

        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestppointmentID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;



        public void LoadInfo(int LocalDrivingLicenseAppID, int TestAppointmentID = -1)
        {

            if (TestAppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }


            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseAppID;
            _TestppointmentID = TestAppointmentID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("The Local Driving License Application does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;
            }


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();

            }
            else
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "0";
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrailsPerTest(_TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                dtpTestDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                _TestAppointment = new clsTestAppointment();
                lblRetakeTestAppID.Text = "N/A";
            }

            else
            {
                if (!_LoadTestAppointmentData())
                {
                    return;
                }

            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
            {
                return;
            }

            if(!_HandleLockedAppointmentConstraint())
            {
                btnSave.Enabled = false;
                return;
            }   

            if (!_HandlePreviousTestAppointmentConstraint())
            {
                return;
            }

        }

        private bool _HandleRetakeTestConstraint()
        {
            if (_Mode == enMode.AddNew&& _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication application = new clsApplication();

                application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStatusDate = DateTime.Now;
                application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                

                if(!application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Failed to save the Retake Test Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               

                _TestAppointment.RetakeTestApplicationID = application.ApplicationID;


            }
            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                lblUserMessage.Text = "There is already an active scheduled test for this test type.";
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            return true;
        }

        public bool _HandlePreviousTestAppointmentConstraint()
        {

            switch (_TestTypeID)
            { 
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;

                    return true ;

                    case clsTestType.enTestType.WrittenTest:

                    if(!_LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest))
                    {   
                        lblUserMessage.Visible = true;
                        lblUserMessage.Text = "You must pass the Vision Test before scheduling the Written Test.";
                        dtpTestDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;

                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        dtpTestDate.Enabled = true;
                        btnSave.Enabled = true;
                        
                    }

                    return true;

                    case clsTestType.enTestType.StreetTest:

                    if (!_LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Visible = true;
                        lblUserMessage.Text = "You must pass the Written Test before scheduling the Street Test.";
                        dtpTestDate.Enabled = false;

                        btnSave.Enabled = false;
                        return false;

                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        dtpTestDate.Enabled = true;
                        btnSave.Enabled = true;

                    }

                    return true;

            }

            return true;
        }

        public bool _HandleLockedAppointmentConstraint()
        {

                if (_TestAppointment.IsLocked)
                {
                lblUserMessage.Visible = true;
                   
                lblUserMessage.Text = "The Test Appointment is locked and cannot be modified.";
                    dtpTestDate.Enabled = false;
                    btnSave.Enabled = false;
                    return false;
               
                }
            else
                lblUserMessage.Visible = false;

            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("The Test Appointment does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

          
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }

            dtpTestDate.Value = _TestAppointment.AppointmentDate;
            

           

            if (_TestAppointment.RetakeTestApplicationID != -1)
            {
                lblTitle.Text = "Retake Schedule Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
            }
            else
            {
                lblRetakeTestAppID.Text = "N/A";
                lblRetakeAppFees.Text = "0";
        
            }

                return true;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeTestConstraint())
            {
                return;
            }

            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                MessageBox.Show("The Test Appointment has been saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Failed to save the Test Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          

        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
