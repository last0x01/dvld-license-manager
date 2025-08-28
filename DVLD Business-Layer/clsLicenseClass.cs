using System;
using System.Data;
using DVLD_DataAccess_Layer;

namespace DVLD_Business_Layer
{
    public class clsLicenseClass
    {
        enum enMode { AddNew =0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public enum enLicenseClass { SmallMotorcycle = 1, HeavyMotorcycle = 2, OrdinaryDrivingLicense = 3, Commercial = 4, Agricultural = 5, SmallAndMediumBus = 6, TruckAndHeavyVehicles = 7 }
        public enLicenseClass LicenseClassType { get; set; }

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidateLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = 18;
            DefaultValidateLength = 0;
            ClassFees = 0.0f;
        }



        private clsLicenseClass(int licenseClassID, string className, string classDescription, int minimumAllowedAge, int validateLength, float classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidateLength = validateLength;
            ClassFees = classFees;
        }

        public static clsLicenseClass Find(int licenseClassID)
        {
            string className = "", classDescription = "";
            int minimumAllowedAge = 0, validateLength = 0;
            float classFees = 0;

         
            

            if (clsLicenseClassData.GetLicenseClassByID(
                licenseClassID,
                ref className,
                ref classDescription,
                ref minimumAllowedAge,
                ref validateLength,
                ref classFees))
            {
                return new clsLicenseClass(licenseClassID, className, classDescription, minimumAllowedAge, validateLength, classFees);
            }

            return null;
        }

        public static clsLicenseClass Find(string ClassName)
        {
            int licenseClassID = -1;
            string classDescription = "";
            int minimumAllowedAge = 0, validateLength = 0;
            float classFees = 0;
            if (clsLicenseClassData.GetLicenseClassByName(
                ClassName,
                ref licenseClassID,
                ref classDescription,
                ref minimumAllowedAge,
                ref validateLength,
                ref classFees))
            {
                return new clsLicenseClass(licenseClassID, ClassName, classDescription, minimumAllowedAge, validateLength, classFees);
            }
            return null;
        }

       private bool _UpdateLicenseClass()
        {
            return clsLicenseClassData.UpdateLicenseClass(
                LicenseClassID,
                ClassName,
                ClassDescription,
                MinimumAllowedAge,
                DefaultValidateLength,
                ClassFees
            );
        }

        private bool _AddNewLicenseClass()
        {
            LicenseClassID = clsLicenseClassData.AddNewLicenseClass(
                ClassName,
                ClassDescription,
                MinimumAllowedAge,
                DefaultValidateLength,
                ClassFees
            );

            return LicenseClassID != -1;
        }

        public bool Save()
        {
           switch(_Mode)
            {
                case enMode.AddNew:
                   if(_AddNewLicenseClass())
                    {
                        _Mode = enMode.Update; 
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLicenseClass();

            }
            return false;
        }




      

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}
