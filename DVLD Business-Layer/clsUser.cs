using DVLD_DataAccess_Layer;
using System;
using System.Data;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;


namespace DVLD_Business_Layer
{
    public class clsUser
    {

        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode;
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

        public clsUser()
        {

            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            UserName = userName;
            Password = password;
            IsActive = isActive;

            PersonInfo = clsPerson.Find(personID);
            Mode = enMode.Update;
        }


        public static clsUser FindByUserID(int UserID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;

            if (clsUserData.GetUserInofByUserID(UserID, ref personID, ref userName, ref password, ref isActive))
                return new clsUser(UserID, personID, userName, password, isActive);
            else
                return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string userName = "", password = "";
            bool isActive = false;

            if (clsUserData.GetUserByPersonID (PersonID,ref UserID, ref userName, ref password, ref isActive))
                return new clsUser(UserID, UserID, userName, password, isActive);
            else
                return null;
        }


        public static clsUser FindByUsernameAndPassword(string Username, string Password)
        {
            int personID = -1, UserID = -1;

            bool isActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(Username, Password, ref UserID, ref personID, ref isActive))
                return new clsUser(UserID, personID, Username, Password, isActive);
            else
                return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

     

        private bool _AddNewUser()
        {
            UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static bool DeleteUser(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }

        public static bool IsUserExists(string userName)
        {
            return clsUserData.IsUserExists(userName);
        }

        public static bool IsUserExists(int userID)
        {
            return clsUserData.IsUserExists(userID);
        }

        public static bool DoesUserExistForPerson(int personID)
        {
            return clsUserData.IsUserExistForPersonID(personID);
        }

        public static bool ChangePassword(int userID, string newPassword)
        {
            return clsUserData.ChangePassword(userID, newPassword);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateUser();
            }

            return false;

        }


    
}
}
