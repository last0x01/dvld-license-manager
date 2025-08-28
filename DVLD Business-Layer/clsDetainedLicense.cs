using DVLD_DataAccess_Layer;
using System;
using System.Data;

namespace DVLD_Business_Layer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public bool IsReleased { get; set; }
        public int ReleasedByUserID { get; set; }

        public clsUser ReleasedByUserInfo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseApplicationID { get; set; }

        // The reason why I did not use composition here is because it would cause a StackOverflow (infinite loop).
        // public clsLicense LicenseInfo { get; set; }


        public clsDetainedLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleasedByUserID = -1;
            ReleaseDate = DateTime.MinValue; // null equivalent
            ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }

        public clsDetainedLicense(int detainID, int licenseID, DateTime detainDate,
            float fineFees, int createdByUserID, bool isReleased, int releasedByUserID,
            DateTime releaseDate, int releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleasedByUserID = releasedByUserID;
            ReleaseDate = releaseDate;
            ReleaseApplicationID = releaseApplicationID;

            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserID);


            //  LicenseInfo = clsLicense.Find(licenseID);


            Mode = enMode.Update;
        }

        public static clsDetainedLicense FindByDetainID(int detainID)
        {
            int licenseID = -1;
            DateTime detainDate = DateTime.Now;
            float fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            int releasedByUserID = -1;
            DateTime releaseDate = DateTime.MinValue;
            int releaseApplicationID = -1;

            if (clsDetainedLicensesData.GetDetainedLicenseByID(detainID, ref licenseID, ref detainDate,
                ref fineFees, ref createdByUserID, ref isReleased, ref releasedByUserID,
                ref releaseDate, ref releaseApplicationID))
            {
                return new clsDetainedLicense(detainID, licenseID, detainDate, fineFees,
                    createdByUserID, isReleased, releasedByUserID, releaseDate, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicense FindByLicenseID(int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.Now;
            float fineFees = 0;
            int createdByUserID = -1;
            bool isReleased = false;
            int releasedByUserID = -1;
            DateTime releaseDate = DateTime.MinValue;
            int releaseApplicationID = -1;

            if (clsDetainedLicensesData.GetDetainedLicenseByLicenseID(licenseID, ref detainID,
                ref detainDate, ref fineFees, ref createdByUserID, ref isReleased,
                ref releasedByUserID, ref releaseDate, ref releaseApplicationID))
            {
                return new clsDetainedLicense(detainID, licenseID, detainDate, fineFees,
                    createdByUserID, isReleased, releasedByUserID, releaseDate, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesData.UpdateDetainedLicense(DetainID, LicenseID, DetainDate,
                FineFees, CreatedByUserID, IsReleased, ReleasedByUserID, ReleaseDate, ReleaseApplicationID);
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesData.AddNewDetainedLicense(LicenseID, DetainDate,
                FineFees, CreatedByUserID);

            return DetainID != -1;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }
            return false;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }

        public static bool IsDetainedLicense(int licenseID)
        {
            return clsDetainedLicensesData.IsDetainedLicense(licenseID);
        }

        public bool ReleaseDetainedLicense( int ReleasedByUserID,int ReleaseApplicationID)
        {
            return clsDetainedLicensesData.ReleaseDetainedLicense(this.DetainID, DateTime.Now,ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
