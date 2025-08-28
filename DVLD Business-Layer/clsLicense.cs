using DVLD_DataAccess_Layer;
using System;
using System.Data;

namespace DVLD_Business_Layer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {
            get { return GetIssueReasonText(); }
        }
        public int CreatedByUserID { get; set; }

        public bool IsDetainedLicense
        {
            get { return clsDetainedLicensesData.IsDetainedLicense(this.LicenseID); }
        }

        public clsDetainedLicense DetainedInfo { get; set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = "";
            PaidFees = 0f;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID,
            DateTime issueDate, DateTime expirationDate, string notes, float paidFees,
            bool isActive, enIssueReason IssueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            this.IssueReason = IssueReason;
            CreatedByUserID = createdByUserID;

            DriverInfo = clsDriver.FindByDriverID(DriverID);
            LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
            DetainedInfo = clsDetainedLicense.FindByLicenseID(LicenseID);

            Mode = enMode.Update;
        }

        public static clsLicense Find(int licenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int licenseClassID = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = "";
            float paidFees = 0f;
            bool isActive = false;
            byte IssueReason = 0;
            int createdByUserID = -1;

            if (clsLicenseData.GetLicenseByID(licenseID, ref applicationID, ref driverID, ref licenseClassID,
                ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref IssueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID, licenseClassID,
                    issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason)IssueReason, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }

        public bool Delete()
        {
            return clsLicenseData.DeleteLicense(LicenseID);
        }

        public static bool IsLicenseExists(int licenseID)
        {
            return clsLicenseData.isLicenseExists(licenseID);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClassID,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
        }

        private bool _AddNewLicense()
        {
            LicenseID = clsLicenseData.AddNewLicense(ApplicationID, DriverID, LicenseClassID,
                IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
            return (LicenseID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateLicense();

                default:
                    return false;
            }
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public bool DeactiveCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }

        public static int GetActiveLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPerson(PersonID, LicenseClassID);
        }

        public string GetIssueReasonText()
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";

                case enIssueReason.Renew:
                    return "Renew";

                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";

                case enIssueReason.LostReplacement:
                    return "Lost Replacement";

                default:
                    return "Frirst Time";
            }

        }


        public static bool IsLicenseExistsByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.IsLicenseExistsByPersonID(PersonID, LicenseClassID) != -1;



        }

        public Boolean IsLicenseExpierd()
        {
            if (ExpirationDate < DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //public bool ReleaseDetainedLicense()

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            
            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.DetainDate = DateTime.Now;
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.CreatedByUserID = CreatedByUserID;

            if (DetainedLicense.Save())
            {
                return DetainedLicense.DetainID;
            }
            else
            {
                return -1; 
            }
        }


        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int Application)
        {
            clsApplication application = new clsApplication();

            application.ApplicantPersonID = this.DriverInfo.PersonID;
            application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicesne;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.CreatedByUserID = ReleasedByUserID;
            application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicesne).Fees;

            if (!application.Save())
            {
                Application = -1;
                return false;
            }



            Application = application.ApplicationID;

            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application);

        }

      
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {


            clsApplication Application = new clsApplication();

            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationDate = DateTime.Now;
            Application.CreatedByUserID = CreatedByUserID;
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.LastStatusDate = DateTime.Now;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();


            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidateLength = this.LicenseClassInfo.DefaultValidateLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidateLength);
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.Notes = Notes;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.IssueReason = enIssueReason.Renew;


            if (!NewLicense.Save())
            {
                return null;
            }

        
            DeactiveCurrentLicense();

            return NewLicense;


        }


        public clsLicense Replace(enIssueReason IssueReason , int CreatedByUserID)
        {
            clsApplication application = new clsApplication();

            application.ApplicantPersonID = this.DriverInfo.PersonID;

            if(IssueReason == enIssueReason.LostReplacement)
            
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDrivingLicenseForLost;
            
            else
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReplaceDrivingLicenseForDamaged;

            application.CreatedByUserID = CreatedByUserID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;

            
                application.PaidFees = clsApplicationType.Find(application.ApplicationTypeID).Fees;
          
            application.LastStatusDate = DateTime.Now;
            
            if (!application.Save())
            {
                return null;
            }


            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.Notes = this.Notes;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.IssueReason = IssueReason;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.DriverID = this.DriverID;
            
            if (!NewLicense.Save())
            {
                return null;
            }

            // Deactivate the current license before replacing it with the new one.
            DeactiveCurrentLicense();

            return NewLicense;
        }
    }
}
