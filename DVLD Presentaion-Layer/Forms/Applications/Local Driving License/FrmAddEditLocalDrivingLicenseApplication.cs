using DVLD_Presentaion_Layer.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Global_Classes;

namespace DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services.New_Driving_License
{
    public partial class FrmAddEditLocalDrivingLicenseApplication : Form


    {
        enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        private int _SelectedPsersonID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        public FrmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }


        public FrmAddEditLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.Update;
        }

        private void _FillAllLicenseClassesInComboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }


        private void _ResetDefualtValues()
        {
            _FillAllLicenseClassesInComboBox();

            if(_Mode == enMode.AddNew)
            {
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                cbLicenseClasses.SelectedIndex = 2;
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                ctrlPersonCardWithFilter1.FilterFocus();

                lblTitle.Text = "New Local Driving License Application";
                                this.Text = "New Local Driving License Application";

                tpApplicationInfo.Enabled = false;
            }

            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }

          
      
        }
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("The Application With "+_LocalDrivingLicenseApplicationID + "does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            lblCreatedByUser.Text = _LocalDrivingLicenseApplication.CreatedByUserID.ToString();
            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            cbLicenseClasses.SelectedIndex = _LocalDrivingLicenseApplication.LicenseClassID -1;

       

        }


      
   
        
       

        private void btnNext_Click(object sender, EventArgs e)
        {

            if(_Mode == enMode.Update)
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                TC_ApplicationInfo.SelectedTab = TC_ApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }
           
            
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
                TC_ApplicationInfo.SelectedTab = TC_ApplicationInfo.TabPages["tpApplicationInfo"];
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           

          int LicenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClassID(_SelectedPsersonID, clsApplication.enApplicationType.NewDrivingLicense,LicenseClassID);


            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("The selected person already have an active application for the selected class with id = "+ _SelectedPsersonID, "ApplicationID ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }

            if(clsLicense.IsLicenseExistByPersonID(ctrlPersonCardWithFilter1.PersonID ,LicenseClassID ))
            {
                 MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!clsLocalDrivingLicenseApplication._DoesPersonAgeAllowedForThisLicense(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person age is not allowed for this license class, Please choose another license class.", "Age Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


                _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;

            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
          
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Failed to save the Data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void FrmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void FrmAddEditLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnSelectedPerson(int obj)
        {
            _SelectedPsersonID = obj;
        }
    }
}
