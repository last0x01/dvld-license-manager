using DVLD_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Layer
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
        public clsCountry()
        {
            ID = -1;
            CountryName = "";
        }
        public clsCountry(int countryID, string countryName)
        {
            ID = countryID;
            CountryName = countryName;
        }
        

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountryData.GetCountryByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {

            int ID = -1;

            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }


        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

    }
}
