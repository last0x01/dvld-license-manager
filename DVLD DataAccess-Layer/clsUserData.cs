using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsUserData
    {

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetUserInofByUserID(int UserID, ref int PersonID, ref string Username, ref string Password, ref bool IsActive)
        {

            bool IsFound = false;
            string query = @"Select * from Users
                            Where UserID = @UserID";



            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        Username = reader.GetString(reader.GetOrdinal("Username"));
                        Password = reader.GetString(reader.GetOrdinal("Password"));
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }



            }
            return IsFound;
        }

        public static bool GetUserByPersonID(int PersonID,ref int UserID,   ref string Username, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            string query = @"Select * from Users
                            Where PersonID = @PersonID";



            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        PersonID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        Username = reader.GetString(reader.GetOrdinal("Username"));
                        Password = reader.GetString(reader.GetOrdinal("Password"));
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }



            }
            return IsFound;
        }



        public static bool GetUserInfoByUsernameAndPassword(string Username,  string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {

            bool IsFound = false;
            string query = @"Select * from Users
                            Where Username = @Username And Password = @Password";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }



            }
            return IsFound;
        }

        public static int AddNewUser(int PersonID,  string Username,  string Password,  bool IsActive)
        {
            string query = @"Insert into Users (PersonID, Username, Password, IsActive)
                             Values (@PersonID, @Username, @Password, @IsActive);
                            Select SCOPE_IDENTITY();";

            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IsActive", IsActive);



                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        UserID = InsertedID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return UserID;
        }


        public static bool UpdateUser(int UserID, int PersonID, string Username, string Password, bool IsActive)
        {
            string query = @"Update  Users 
                             Set PersonID = @PersonID, 
                                 Username = @Username, 
                                 Password = @Password, 
                                IsActive = @IsActive
                             Where UserID = @UserID
                           ";

            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    connection.Open();

                    RowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return RowsAffected > 0;
        }


        public static bool DeleteUser(int UserID)
        {
            int RowsAffected = 0;

            string query = @"delete from Users
                              Where UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand Command = new SqlCommand(query, connection);

                Command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    connection.Open();

                    RowsAffected = Command.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }


            }

            return RowsAffected > 0;
        }


        public static bool IsUserExists(string Username)
        {
            bool IsExists = false;
            string query = @"Select Found=1 from Users
                            Where Username = @Username";
           
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", Username);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IsExists = reader.HasRows;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return IsExists;
        }


        public static bool IsUserExists(int UserID)
        {
            bool IsExists = false;
            string query = @"Select Found=1 from Users
                            Where UserID = @UserID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", UserID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IsExists = reader.HasRows;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return IsExists;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dtUsers = new DataTable();


            string query = @"

                    Select UserID, Users.PersonID, CONCAT(FirstName ,' ', SecondName, ' ', ThirdName,  ' ', LastName ) as FullName,Username,
		                    IsActive
		                    from Users
		                    Inner Join People on People.PersonID = Users.PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();



                    if (reader.HasRows)
                    {
                        dtUsers.Load(reader);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }

            return dtUsers;
        }


        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


    }
}
