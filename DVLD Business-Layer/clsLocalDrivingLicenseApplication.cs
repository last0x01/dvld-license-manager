using DVLD_DataAccess_Layer;
using System;
using System.Data;
using System.Net.Http.Headers;
using System.Xml.Linq;
using static DVLD_Business_Layer.clsApplication;

namespace DVLD_Business_Layer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }
        public string PersonFullName { get { return clsPerson.Find(ApplicantPersonID).FullName; } }


        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            Mode = enMode.AddNew;

        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int applicationID, int applicantPersonID, DateTime ApplicationDate, int applicationTypeID, clsApplication.enApplicationStatus applicationStatusID, DateTime lastStatusDate, float PaidFees, int createdByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = LicenseClassID;
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            this.ApplicantPersonID = applicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatusID;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID
            (int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;



            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(
                 LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID
            );

            if (isFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, (clsApplication.enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }

            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID
          (int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;


            bool isFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationByID(
                ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID
            );

            if (isFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, (clsApplication.enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }

            return null;
        }


        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(this.LicenseClassID, this.ApplicationID);

            return (LocalDrivingLicenseApplicationID != -1);
        }


        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }



        public bool Delete()
        {
            bool isLocalDrivingLicenseApplicationDeleted = false;

            bool isApplicationDeleted = false;

            isLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!isLocalDrivingLicenseApplicationDeleted)
            {
                return false;
            }

            isApplicationDeleted = base.Delete(); // Delete the base application

            return isApplicationDeleted;


        }


        public bool Save()
        {

            base.Mode = (clsApplication.enMode)Mode;

            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:


                    return _UpdateLocalDrivingLicenseApplication();
            }


            return false;
        }

        public static bool _DoesPersonAgeAllowedForThisLicense(int PersonID, int LicenseClassID)
        {
            DateTime BirthDate = clsPerson.Find(PersonID).DateOfBirth;
            int MinimumAllowedAge = clsLicenseClass.Find(LicenseClassID).MinimumAllowedAge;

            int PersonAge = DateTime.Now.Year - BirthDate.Year;

            if (BirthDate > DateTime.Now.AddYears(-PersonAge))
            {
                PersonAge--;
            }



            return (PersonAge >= MinimumAllowedAge);
        }


        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType testType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (byte)testType);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (byte)TestType);
        }


        public int TotalTrailsPerTest(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrailsPerTest(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType testTypeID)
        {


            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);

        }


        public bool DoesPassedTestType(clsTestType.enTestType testTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassedTestType(this.LocalDrivingLicenseApplicationID, (byte)testTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }


        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int IssueLicenseForTheFirstTime(string Notes, int CreatedUserByID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedDate = DateTime.Now;
                Driver.CreatedByUserID = CreatedUserByID;

                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1; // Failed to create driver
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidateLength);
            License.PaidFees = this.PaidFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedUserByID;
            License.Notes = Notes;
            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID; // Successfully issued license
            }

            return -1; // Failed to issue license

        }



    }
}