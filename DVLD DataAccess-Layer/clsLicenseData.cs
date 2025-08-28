using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layer
{
    public class clsLicenseData
    {
        public static bool GetLicenseByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive,
            ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = true;

            string query = @"Select * from Licenses where LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID"));
                        DriverID = reader.GetInt32(reader.GetOrdinal("DriverID"));
                        LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID"));
                        IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                        ExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate"));
                        if (reader.IsDBNull(reader.GetOrdinal("Notes")))
                            Notes = "";
                        else
                            Notes = reader.GetString(reader.GetOrdinal("Notes"));
                        PaidFees = Convert.ToSingle(reader["PaidFees"]);
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                        IssueReason = reader.GetByte(reader.GetOrdinal("IssueReason"));
                        CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));

                        isFound = true;
                    }

                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return isFound;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive,
            byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;

            string query = @"Update Licenses 
                             set ApplicationID = @ApplicationID, DriverID = @DriverID, LicenseClassID = @LicenseClassID,
                                 IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes,
                                 PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUserID = @CreatedByUserID
                             where LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                if (Notes == "" || Notes == null)
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return (rowsAffected > 0);
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive,
            byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            string query = @"Insert into Licenses (ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes,
                                PaidFees, IsActive, IssueReason, CreatedByUserID) 
                             Values (@ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes,
                                @PaidFees, @IsActive, @IssueReason, @CreatedByUserID)
                             SELECT Scope_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

                if(Notes == "" || Notes == null)
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@Notes", Notes);

                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueReason", IssueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        LicenseID = InsertedID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return LicenseID;
        }

        public static bool isLicenseExists(int LicenseID)
        {
            bool isExists = false;
            string query = @"Select Found=1 from Licenses where LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isExists = reader.HasRows;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return isExists;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClassID = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int GetActiveLicenseIDByPerson(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            string query = @"Select LicenseID from Licenses  Inner join Drivers on Drivers.DriverID = Licenses.DriverID
                                where LicenseClassID = @LicenseClassID And Drivers.PersonID = @PersonID And IsActive=1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    
                    if (result != null && int.TryParse(result.ToString(), out int ActiveLicenseID))
                    {
                        LicenseID = ActiveLicenseID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return LicenseID ;

        }

        public static bool DeleteLicense(int LicenseID)
        {
            int rowsAffected = 0;
            string query = @"Delete from Licenses where LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllLicenses()
        {
            DataTable dtLicenses = new DataTable();
            string query = @"Select * from Licenses";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtLicenses.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return dtLicenses;
        }

        public static bool DeactivateLicense(int LicenseID)
        {
            int rowsAffected = 0;
            string query = @"Update Licenses set IsActive = 0 where LicenseID = @LicenseID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return (rowsAffected > 0);
        }

        public static int IsLicenseExistsByPersonID(int PersonID, int LicenseClassID)
        {
            int  LicenseID = -1;
            string query = @"Select LicenseID from Licenses Inner Join Drivers on Drivers.DriverID = Licenses.DriverID
                                where Drivers.PersonID = @PersonID And LicenseClassID = @LicenseClassID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if(result != null && int.TryParse(result.ToString(), out int InsertedID))
                        {
                        LicenseID= InsertedID;
                    }

                    

                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return LicenseID;
        }

      
    }
}
