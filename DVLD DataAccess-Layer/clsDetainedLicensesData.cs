using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layer
{
    public class clsDetainedLicensesData
    {
        public static bool GetDetainedLicenseByID(int DetainID,
                                                  ref int LicenseID,
                                                  ref DateTime DetainDate,
                                                  ref float FineFees,
                                                  ref int CreatedByUserID,
                                                  ref bool IsReleased,
                                                  ref int ReleasedByUserID,
                                                  ref DateTime ReleaseDate,
                                                  ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            string query = @"SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LicenseID = Convert.ToInt32(reader["LicenseID"]);
                        DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                        FineFees = Convert.ToSingle(reader["FineFees"]);
                        CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                        IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                        ReleasedByUserID = reader["ReleasedByUserID"] != DBNull.Value ? Convert.ToInt32(reader["ReleasedByUserID"]) : -1;
                        ReleaseDate = reader["ReleaseDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReleaseDate"]) : DateTime.MinValue;
                        ReleaseApplicationID = reader["ReleaseApplicationID"] != DBNull.Value ? Convert.ToInt32(reader["ReleaseApplicationID"]) : -1;

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


        public static bool GetDetainedLicenseByLicenseID(
    int licenseID,
    ref int detainID,
    ref DateTime detainDate,
    ref float fineFees,
    ref int createdByUserID,
    ref bool isReleased,
    ref int releasedByUserID,
    ref DateTime releaseDate,
    ref int releaseApplicationID)
        {
            bool isFound = false;

            string query = @"SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", licenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    {
                        isFound = true;

                        detainID = (int)reader["DetainID"];
                        detainDate = (DateTime)reader["DetainDate"];
                        fineFees = Convert.ToSingle(reader["FineFees"]);
                        createdByUserID = (int)reader["CreatedByUserID"];
                        isReleased = (bool)reader["IsReleased"];


                        if (reader["ReleasedByUserID"] != DBNull.Value)
                            releasedByUserID = (int)reader["ReleasedByUserID"];
                        else
                            releasedByUserID = -1;

                        if (reader["ReleaseDate"] != DBNull.Value)
                            releaseDate = (DateTime)reader["ReleaseDate"];
                        else
                            releaseDate = DateTime.MinValue;


                        if (reader["ReleaseApplicationID"] != DBNull.Value)
                            releaseApplicationID = (int)reader["ReleaseApplicationID"];
                        else
                            releaseApplicationID = -1;
                    }


                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }   
            }

            return isFound;
        }


        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            string query = @"INSERT INTO DetainedLicenses 
                             (LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased) 
                             VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID, 0);
                             SELECT SCOPE_IDENTITY();";

            int DetainID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                        DetainID = InsertedID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
                                                 float FineFees, int CreatedByUserID,
                                                 bool IsReleased, int ReleasedByUserID,
                                                 DateTime ReleaseDate, int ReleaseApplicationID)
        {
            string query = @"UPDATE DetainedLicenses
                             SET LicenseID = @LicenseID,
                                 DetainDate = @DetainDate,
                                 FineFees = @FineFees,
                                 CreatedByUserID = @CreatedByUserID,
                                 IsReleased = @IsReleased,
                                 ReleasedByUserID = @ReleasedByUserID,
                                 ReleaseDate = @ReleaseDate,
                                 ReleaseApplicationID = @ReleaseApplicationID
                             WHERE DetainID = @DetainID";

            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsReleased", IsReleased);

                if (ReleasedByUserID != -1)
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
                else
                    command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

                if (ReleaseDate != DateTime.MinValue)
                    command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
                else
                    command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

                if (ReleaseApplicationID != -1)
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

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

        public static bool ReleaseDetainedLicense(int DetainID,DateTime ReleaseDate,
                  int ReleasedByUserID, int ReleaseApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleasedByUserID =@ReleasedByUserID,
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
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


        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * FROM DetainedLicenses_View order by IsReleased, DetainID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        dt.Load(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return dt;
        }

        public static bool IsDetainedLicense(int LicenseID)
        {
            bool IsDetained = false;
            string query = @"SELECT 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IsDetained = reader.HasRows;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return IsDetained;
        }

    }
}
