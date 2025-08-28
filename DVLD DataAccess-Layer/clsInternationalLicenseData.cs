using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layer
{
    public class clsInternationalLicenseData
    {
        public static bool GetLicenseByID(int InternationalLicenseID,
                                          ref int ApplicationID,
                                          ref int DriverID,
                                          ref int IssuedUsingLocalLicenseID,
                                          ref bool IsActive,
                                          ref DateTime IssueDate,
                                          ref DateTime ExpirationDate,
                                          ref int CreatedByUserID)
        {
            bool IsFound = false;

            string query = @"SELECT * FROM InternationalLicenses 
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                        DriverID = Convert.ToInt32(reader["DriverID"]);
                        IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                        IsActive = Convert.ToBoolean(reader["IsActive"]);
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                        ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                        CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

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

        public static int AddNewLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                                        bool IsActive, DateTime IssueDate, DateTime ExpirationDate,
                                        int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            string query = @"
                            update InternationalLicenses
                            set IsActive = 0
                             where DriverID= @DriverID;

                            INSERT INTO InternationalLicenses 
                            (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID) 
                            VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate,@IsActive, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        InternationalLicenseID = InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return InternationalLicenseID;
        }

        public static bool UpdateLicense(int InternationalLicenseID, int ApplicationID, int DriverID,
                                         int IssuedUsingLocalLicenseID, bool IsActive,
                                         DateTime IssueDate, DateTime ExpirationDate,
                                         int CreatedByUserID)
        {
            int RowsAffected = 0;

            string query = @"UPDATE InternationalLicenses 
                             SET ApplicationID = @ApplicationID,
                                 DriverID = @DriverID, 
                                 IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID, 
                                 IsActive = @IsActive, 
                                 IssueDate = @IssueDate, 
                                 ExpirationDate = @ExpirationDate,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@DriverID", DriverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool DeleteLicense(int InternationalLicenseID)
        {
            int RowsAffected = 0;

            string query = @"DELETE FROM InternationalLicenses 
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT InternationalLicenseID, ApplicationID, DriverID, 
                                    IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive
                             FROM InternationalLicenses";

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

        public static DataTable GetLicensesByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();

            string query = @" SELECT    InternationalLicenseID, ApplicationID,
		                IssuedUsingLocalLicenseID , IssueDate, 
                        ExpirationDate, IsActive
		    from InternationalLicenses where DriverID=@DriverID
                order by ExpirationDate desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
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
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }

        public static bool IsLicenseExists(int InternationalLicenseID)
        {
            bool IsExists = false;

            string query = @"SELECT Found=1 FROM InternationalLicenses 
                             WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IsExists = reader.HasRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return IsExists;
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }
    }
}
