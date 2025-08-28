using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace DVLD_DataAccess_Layer
{
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentByID(int TestAppointmentID, ref int LocalDrivingLicenseApplicationID,
            ref int TestTypeID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = true;

            string query = @"Select * from TestAppointments where TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        LocalDrivingLicenseApplicationID = reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID"));
                        TestTypeID = reader.GetInt32(reader.GetOrdinal("TestTypeID"));
                        AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                        PaidFees  = Convert.ToSingle(reader["PaidFees"]);
                        CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                        IsLocked = (bool)reader["IsLocked"];
                        if (reader["RetakeTestApplicationID"] == DBNull.Value)
                            RetakeTestApplicationID = -1;
                        else
                            RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        public static bool UpdateTestAppointment(int TestAppointmentID, int LocalDrivingLicenseApplicationID,
            int TestTypeID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int rowsAffected = 0;

            string query = @"Update TestAppointments 
                             set LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                 TestTypeID = @TestTypeID, AppointmentDate = @AppointmentDate,
                                 PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID, IsLocked = @IsLocked
                             where TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

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

        public static int AddNewTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            string query = @"Insert into TestAppointments (LocalDrivingLicenseApplicationID, TestTypeID, AppointmentDate,
                                PaidFees, CreatedByUserID, IsLocked) 
                             Values (@LocalDrivingLicenseApplicationID, @TestTypeID, @AppointmentDate,
                                @PaidFees, @CreatedByUserID, @IsLocked)
                             SELECT Scope_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        TestAppointmentID = InsertedID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return TestAppointmentID;
        }

        public static bool isTestAppointmentExists(int TestAppointmentID)
        {
            bool isExists = false;
            string query = @"Select Found=1 from TestAppointments where TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool DeleteTestAppointment(int TestAppointmentID)
        {
            int rowsAffected = 0;
            string query = @"Delete from TestAppointments where TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dtTestAppointments = new DataTable();
            string query = @"Select * from TestAppointments_View order by AppointmentDate desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtTestAppointments.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return dtTestAppointments;
        }

        public static DataTable GetAllTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dtTestAppointments = new DataTable();
            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dtTestAppointments.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }

            }

            return dtTestAppointments;
        }


        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID, ref int TestAppointmentID,
             ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            string query = @"Select Top 1 * from TestAppointments
                            where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID
                            Order by TestAppointmentID desc";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {

                        TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                        AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate"));
                        PaidFees = reader.GetFloat(reader.GetOrdinal("PaidFees"));
                        CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                        IsLocked = reader.GetBoolean(reader.GetOrdinal("IsLocked"));
                        RetakeTestApplicationID = reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"));
                        isFound = true;

                    }
                }

                catch (Exception Ex)
                {
                    // Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return isFound;

        }


        public static int GetTestID(int AppointmentID)
        {
            int TestID = -1;

            string query = @"Select TestID from Tests where TestAppointmentID = @TestAppointmentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", AppointmentID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int ID))
                    {
                        TestID = ID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }

            return TestID;
        }
        
    }
}
