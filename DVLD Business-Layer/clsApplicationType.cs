using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess_Layer;

namespace DVLD_Business_Layer
{
    public class clsApplicationType
    {
        enum enMode { AddNew =0 , Update = 1 };
        private enMode _Mode { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = string.Empty;
            this.Fees = 0.0f;
            _Mode = enMode.AddNew;
        }

        private clsApplicationType(int ApplicationTypeID, string title, float fess) { 
        
            this.ID = ApplicationTypeID;
            Title = title;
            Fees = fess;
            _Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string title = string.Empty;
            float fees = 0.0f;

            if (clsApplicationTypeData.GetApplicationTypeByID(ApplicationTypeID, ref title, ref fees))
            {
                return new clsApplicationType(ApplicationTypeID, title, fees);
            }
            else
                return null;
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);


            return (this.ID != -1);
        }


        private bool _UpdateApplicationType()
        {
            
           return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        _Mode = enMode.Update; // Change mode to Update after successful addition
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplicationType();
            }
            return false; // Default case, should not be reached

        }

        


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

      
    }
}
