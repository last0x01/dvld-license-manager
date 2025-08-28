using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public  class clsPerson
    {
        public enum enMode {AddNew = 1, Update = 2 }
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        private string _ImagePath;

        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;

        public string FullName
        {
            get
            {
                return $"{FirstName} {SecondName} {ThirdName} {LastName}";
            }
        }

        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = 0;
            Address = "";
            Phone = "";
            Email = "";
            ImagePath = "";
            NationalityCountryID = -1;

            Mode = enMode.AddNew;

        }

        public clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName
            , DateTime dateOfBirth, byte gender,string address, string phone, string email, int NationalityCountryID , string imagepath)
        {
            PersonID = personID;
            NationalNo = nationalNo;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            Phone = phone;
            Email = email;
            ImagePath = imagepath;
            this.NationalityCountryID = NationalityCountryID;

            Mode = enMode.Update;

            CountryInfo = clsCountry.Find(NationalityCountryID);
        }

        public static clsPerson Find(int PersonID)
        {
            int NationalityCountryID = -1 ;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = ""; 
            DateTime DateOfBirth = DateTime.Now; byte Gender = 0;

            if (clsPersonData.GetPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                                        ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                        ref Email, ref NationalityCountryID, ref ImagePath))
                {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }


        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1 ;
            int NationalityCountryID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now; byte Gender = 0;

            if (clsPersonData.GetPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName,
                                        ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone,
                                        ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }



        public static DataTable GetAllPeople()
        {

            return clsPersonData.GetAllPeople();
        }

  

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExists(int PersonID)
        {
            return clsPersonData.IsPersonExists(PersonID);
        }

        public static bool IsPersonExists(string NationalNo)
        {
            return clsPersonData.IsPersonExists(NationalNo);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth,Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);

            return (PersonID != -1);
        }

  
        public bool Save()
        {
            switch (Mode)
            { 
                case enMode.AddNew:
                    if(_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    
                    return _UpdatePerson();


            }
            return false;

        }
    }
}
