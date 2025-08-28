
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsPersonData
    {


       
        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
                                        ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone,
                                        ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {

            bool IsFound = false;
            string query = @"Select * from People
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
                        NationalNo = reader["NationalNo"].ToString();
                        FirstName = reader["FirstName"].ToString();
                        SecondName = reader["SecondName"].ToString();

                        if (reader["ThirdName"] != DBNull.Value)
                        {
                            ThirdName = reader["ThirdName"].ToString();
                        }
                        else
                            ThirdName = "";

                        LastName = reader["LastName"].ToString();
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        Gender = Convert.ToByte(reader["Gender"]);
                        Address = reader["Address"].ToString();
                        Phone = reader["Phone"].ToString();

                        if (reader["Email"] != DBNull.Value)
                        {
                            Email = reader["Email"].ToString();
                        }
                        else
                            Email = "";


                        NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                        if (reader["ImagePath"] != DBNull.Value)
                        {
                            ImagePath = reader["ImagePath"].ToString();
                        }
                        else
                            ImagePath = "";
                       

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

        public static bool GetPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName,
                                                ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone,
                                        ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;


            string query = @"Select * from People
                            Where NationalNo = @NationalNo";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        PersonID = Convert.ToInt32(reader["PersonID"]);
                        FirstName = reader["FirstName"].ToString();
                        SecondName = reader["SecondName"].ToString();

                        if (reader["ThirdName"] != DBNull.Value)
                        {
                            ThirdName = reader["ThirdName"].ToString();
                        }
                        else
                            ThirdName = "";

                        LastName = reader["LastName"].ToString();
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        Gender = Convert.ToByte(reader["Gender"]);
                        Address = reader["Address"].ToString();
                        Phone = reader["Phone"].ToString();

                        if (reader["Email"] != DBNull.Value)
                        {
                            Email = reader["Email"].ToString();
                        }
                        else
                            Email = "";

                        NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                        if (reader["ImagePath"] != DBNull.Value)
                        {
                            ImagePath = reader["ImagePath"].ToString();
                        }
                        else
                            ImagePath = "";
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


        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
                                         string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone,
                                         string Email, int NationalityCountryID, string ImagePath)
        {
            string query = @"Insert into People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                            Values (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                            Select SCOPE_IDENTITY();";

            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);

                if(ThirdName != null && ThirdName != "")
                {
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                }

                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Gender", Gender);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Address", Address);

                if (Email != null && Email != "")
                {
                    command.Parameters.AddWithValue("@Email", Email);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }

                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                if (ImagePath != null && ImagePath != "")
                {
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                }
                else
                {
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }
            



                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        PersonID = InsertedID;
                    }
                }
                catch (Exception Ex)
                {
                    
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return PersonID;
        }


        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName,
                                         string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone,
                                         string Email, int NationalityCountryID, string ImagePath)
        {
            string query = @"Update  People 
                             Set FirstName = @FirstName, 
                                 SecondName = @SecondName, 
                                 ThirdName = @ThirdName, 
                                 LastName = @LastName, 
                                 DateOfBirth = @DateOfBirth,
                                 Gender = @Gender,
                                 Phone = @Phone,
                                 Address = @Address,
                                 Email = @Email,
                                 NationalityCountryID = @NationalityCountryID,
                                 ImagePath = @ImagePath
                             Where PersonID = @PersonID
                           ";

            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);

                if (ThirdName != null && ThirdName != "")
                {
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                }
                else
                {
                    command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
                }
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Gender", Gender);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Address", Address);

                if (Email != null && Email != "")
                {
                    command.Parameters.AddWithValue("@Email", Email);
                }
                else
                {
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
                }

                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                if (ImagePath != null && ImagePath != "")
                {
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                }
                else
                {
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
                }




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


        public static bool DeletePerson(int PersonID)
        {
            int RowsAffected = 0;

            string query = @"delete from People
                              Where PersonID = @PersonID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand Command = new SqlCommand(query, connection);

                Command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static bool IsPersonExists(string NationalNo)
        {
            bool IsExists = false;
            string query = @"Select Found=1 from People
                            Where NationalNo = @NationalNo";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
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


        public static bool IsPersonExists(int PersonID)
        {
            bool IsExists = false;
            string query = @"Select Found=1 from People
                            Where PersonID = @PersonID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
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

        public static DataTable GetAllPeople()
        {
            DataTable dtPeople = new DataTable() ;
           

            string query = @"
                                Select PersonID, NationalNo, FirstName , SecondName, ThirdName, LastName, 
                                DateOfBirth,  CASE Gender
                                WHEN 0 THEN 'Male'
                                WHEN 1 THEN 'Female'
                                END AS Gender
                                   ,Address,Phone,Email,Countries.CountryName ,ImagePath from People
                                    Inner JOin Countries on NationalityCountryID = Countries.CountryID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                   if(reader.HasRows)
                    {
                        dtPeople.Load(reader);
                    }

                    reader.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }

            return dtPeople;
        }



    }


}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    