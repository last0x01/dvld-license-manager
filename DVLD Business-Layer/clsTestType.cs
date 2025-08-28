using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess_Layer;

namespace DVLD_Business_Layer
{
    public class clsTestType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public clsTestType.enTestType ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        private clsTestType(clsTestType.enTestType ID, string Title, string Description, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
            this.Description = Description;
            Mode = enMode.Update;
        }

        public clsTestType()
        {
            this.ID = enTestType.VisionTest;
            this.Title = "";
            this.Fees = 0;
            this.Description = "";
            Mode = enMode.AddNew;
        }

        public static clsTestType Find(clsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = "";
            float Fees = 0;

            if (clsTestTypesData.GetTestTypeByID((int)TestTypeID, ref Title, ref Description, ref Fees))
            {
                return new clsTestType(TestTypeID, Title, Description, Fees);
            }
            else
            {
                return null;
            }

        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }

        private bool _AddNewTestType()
        {
            this.ID = (clsTestType.enTestType)clsTestTypesData.AddNewTestType(this.Title, this.Description, this.Fees);

            return this.Title != "";
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }
    }
    }
