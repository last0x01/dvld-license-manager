using DVLD_DataAccess_Layer;
using System;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;

namespace DVLD_Business_Layer
{
    public class clsApplication
    {

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3};
        public enum enApplicationType { NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceDrivingLicenseForLost = 3, ReplaceDrivingLicenseForDamaged = 4, ReleaseDetainedDrivingLicesne = 5, NewInternationalDrivingLicense = 6, RetakeTest = 7};
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        public string ApplicantFullName
        {
            get
            {
                return PersonInfo.FullName;
            }
        }
        public clsPerson PersonInfo { get; set; }
        public DateTime ApplicationDate
        {
            get;set;
        }
        public int ApplicationTypeID;

        public clsApplicationType ApplicationTypeInfo { get; set; }

        public enApplicationStatus ApplicationStatus;

        public string TextStatus
        {
            get
            {
                switch(ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }

        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser UserInfo { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
           this.ApplicationTypeID = -1;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            ApplicationDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        private clsApplication(int applicationID, int applicantPersonID, DateTime ApplicationDate, int applicationTypeID, enApplicationStatus applicationStatusID, DateTime lastStatusDate, float PaidFees, int createdByUserID)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            PersonInfo = clsPerson.Find(applicantPersonID);
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeInfo = clsApplicationType.Find((int)applicationTypeID);
            ApplicationStatus = applicationStatusID;
            LastStatusDate = lastStatusDate;
            this.PaidFees = PaidFees;
            CreatedByUserID = createdByUserID;
            UserInfo = clsUser.FindByUserID(createdByUserID);
            this.ApplicationDate = ApplicationDate;
            Mode = enMode.Update;
        }

        


        public static clsApplication FindBaseApplication(int applicationID)
        {


            int applicantPersonID = -1;
            DateTime ApplicationDate  = DateTime.Now;
            int applicationTypeID = -1;
            byte applicationStatus = 0;
            DateTime lastStatusDate = DateTime.MinValue;
            float paidfees = 0;
            int CreatedByUserID = -1;


            if (clsApplicationData.GetApplicationByID(applicationID, ref applicantPersonID, ref ApplicationDate,ref applicationTypeID, ref applicationStatus, ref lastStatusDate, ref paidfees, ref CreatedByUserID))
            {
                return new clsApplication(applicationID, applicantPersonID,ApplicationDate, applicationTypeID, (enApplicationStatus)applicationStatus, lastStatusDate, paidfees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate, (short)this.ApplicationTypeID, (short)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return ApplicationID != -1;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplication();

            }


            return false;
        }


        public  bool Delete()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID,this.ApplicationDate, this.ApplicationTypeID, (short)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }


   

        public static int GetApplicationID(int PersonID, int LicenseClassID)
        {
            return clsApplicationData.GetApplicationID(PersonID, LicenseClassID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);

        }

        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }


        public static int GetActiveApplicationID(int PersonID,enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (short)ApplicationTypeID);
        }

        public int GetActiveApplicationID(enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClassID(int PersonID, enApplicationType applicationType,int LicenseClassID )
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClassID(PersonID, (int)applicationType,LicenseClassID );
        }
    }
}
