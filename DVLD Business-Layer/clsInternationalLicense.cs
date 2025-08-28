using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }

        public int IssuedUsingLocalLicenseID { get; set; }
        public bool IsActive { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)enApplicationType.NewInternationalDrivingLicense;

            InternationalLicenseID = -1;
            DriverID = -1;
            ApplicationID = -1;
            IsActive = false;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID, int internationalLicenseID, int issuedUsingLocalLicenseID, int driverID, bool isActive, DateTime issueDate, DateTime expirationDate)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = (int)enApplicationType.NewInternationalDrivingLicense;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;




            InternationalLicenseID = InternationalLicenseID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            DriverID = driverID;
            DriverInfo = clsDriver.FindByDriverID(driverID);
            IsActive = isActive;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;

        }




        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int applicationID = -1;
            int applicantPersonID = -1;
            DateTime applicationDate = DateTime.Now;
            enApplicationStatus applicationStatus = enApplicationStatus.New;
            DateTime lastStatusDate = DateTime.Now;
            float paidFees = 0.0f;
            int createdByUserID = -1;

            int issuedUsingLocalLicenseID = -1;
            int driverID = -1;
            bool isActive = false;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;

            bool isFound = clsInternationalLicenseData.GetLicenseByID(InternationalLicenseID, ref applicationID, ref driverID, ref issuedUsingLocalLicenseID, ref isActive, ref issueDate, ref expirationDate, ref createdByUserID);

            if (isFound)
            {
                clsApplication application = clsApplication.FindBaseApplication(applicationID);

                return new clsInternationalLicense(application.ApplicationID, application.ApplicantPersonID, application.ApplicationDate, application.ApplicationStatus, application.LastStatusDate, application.PaidFees, createdByUserID, InternationalLicenseID, issuedUsingLocalLicenseID, driverID, isActive, issueDate, expirationDate);


            }

            else
                return null;
        }


        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateLicense(InternationalLicenseID, ApplicationID, DriverID,
                IssuedUsingLocalLicenseID, IsActive, IssueDate, ExpirationDate, CreatedByUserID);
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData.AddNewLicense(ApplicationID, DriverID,
                IssuedUsingLocalLicenseID, IsActive, IssueDate, ExpirationDate, CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)this.Mode;

            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateInternationalLicense();

            }

            return false;
        }


        public static DataTable GetInternationalLicenses(int driverID)
        {
            return clsInternationalLicenseData.GetLicensesByDriverID(driverID);
        }

        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllLicenses();
        }

        public static bool IsInternationalLicenseExists(int internationalLicenseID)
        {
            return clsInternationalLicenseData.IsLicenseExists(internationalLicenseID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }
    }
}
