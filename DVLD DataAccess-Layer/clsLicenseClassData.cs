using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layer
{
    public class clsLicenseClassData
    {
        public static bool IsLicenseClassExists(int LicenseClassID)
        {
            bool IsExists = false;
            string query = "SELECT Found = 1 FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IsExists = reader.HasRows;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return IsExists;
        }

        public static bool GetLicenseClassByName(string ClassName, ref int LicenseClassID, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassName", ClassName);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                        ClassDescription = reader["ClassDescription"].ToString();
                        MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                        DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                        ClassFees = (float)(decimal)(reader["ClassFees"]);
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return IsFound;
        }

        public static bool GetLicenseClassByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref int MinimumAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ClassName = reader["ClassName"].ToString();
                        ClassDescription = reader["ClassDescription"].ToString();
                        MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                        DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                        ClassFees = (float)(decimal)(reader["ClassFees"]);
                        IsFound = true;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return IsFound;
        }

        public static int AddNewLicenseClass(string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            int NewID = -1;
            string query = @"INSERT INTO LicenseClasses (ClassName, ClassDescription, MinimumAllowedAge, ValidateLength, ClassFees)
                             VALUES (@ClassName, @ClassDescription, @MinimumAllowedAge, @ValidateLength, @ClassFees);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassName", ClassName);
                command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
                command.Parameters.AddWithValue("@ClassFees", ClassFees);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        NewID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return NewID;
        }

        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, int MinimumAllowedAge, int DefaultValidityLength, float ClassFees)
        {
            int RowsAffected = 0;
            string query = @"UPDATE LicenseClasses 
                             SET ClassName = @ClassName, 
                                 ClassDescription = @ClassDescription, 
                                 MinimumAllowedAge = @MinimumAllowedAge, 
                                 ValidateLength = @ValidateLength, 
                                 ClassFees = @ClassFees
                             WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassName", ClassName);
                command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                command.Parameters.AddWithValue("@ValidateLength", DefaultValidityLength);
                command.Parameters.AddWithValue("@ClassFees", ClassFees);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return RowsAffected > 0;
        }

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM LicenseClasses";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }
    }
}
