using DVLD_DataAccess_Layer;
using System;
using System.Data;

namespace DVLD_Business_Layer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public int RetakeTestApplicationID { get; set; }


        public clsApplication RetakeTestAppInfo { get; set; }

        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            LocalDrivingLicenseApplicationID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentID, int localDrivingLicenseApplicationID, clsTestType.enTestType testTypeID,
                                  DateTime appointmentDate, float paidFees, int createdByUserID, bool isLocked, int RetakeTestAppID)
        {
            TestAppointmentID = testAppointmentID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            TestTypeID = testTypeID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = RetakeTestAppID;
            RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
            Mode = enMode.Update;
        }

        public static clsTestAppointment Find(int testAppointmentID)
        {
            int localDrivingLicenseApplicationID = -1;
            int testTypeID = -1;
            DateTime appointmentDate = DateTime.Now;
            float paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int RetakeTestAppID = -1;

            if (clsTestAppointmentData.GetTestAppointmentByID(testAppointmentID, ref localDrivingLicenseApplicationID,
                ref testTypeID, ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref RetakeTestAppID))
            {
                return new clsTestAppointment(testAppointmentID, localDrivingLicenseApplicationID, (clsTestType.enTestType)testTypeID,
                                              appointmentDate, paidFees, createdByUserID, isLocked, RetakeTestAppID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }

        public bool Delete()
        {
            return clsTestAppointmentData.DeleteTestAppointment(TestAppointmentID);
        }

        public static bool IsTestAppointmentExists(int testAppointmentID)
        {
            return clsTestAppointmentData.isTestAppointmentExists(testAppointmentID);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID, LocalDrivingLicenseApplicationID,
                (int)TestTypeID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment(LocalDrivingLicenseApplicationID,
                (int)TestTypeID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            return (TestAppointmentID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestAppointment();
                default:
                    return false;
            }
        }

        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseAppID, clsTestType.enTestType TestTypeID)
        {
            int testAppointmentID = -1;
            DateTime appointmentDate = DateTime.Now;
            float paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int RetakeTestAppID = -1;

            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseAppID, (int)TestTypeID,ref testAppointmentID
                , ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked, ref RetakeTestAppID))
            {
                return new clsTestAppointment(testAppointmentID, LocalDrivingLicenseAppID, TestTypeID,
                                               appointmentDate, paidFees, createdByUserID, isLocked, RetakeTestAppID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllTestAppointmentsPerTestType(int LocalDrivingLicenseAppID, clsTestType.enTestType TestType)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsPerTestType(LocalDrivingLicenseAppID, (int)TestType);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(this.TestAppointmentID);
        }
    }
}
