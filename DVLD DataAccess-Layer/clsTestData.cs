using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess_Layer
{
    public class clsTestData
    {
        public static bool  GetTestInfo(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = true;

            string query = @"Select * from Tests where TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                        TestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                        if (reader["Notes"] == DBNull.Value)

                            Notes = "";
                        else
                            Notes = (string)reader["Notes"];
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

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            string query = @"Update Tests 
                             set TestAppointmentID = @TestAppointmentID, TestResult = @TestResult, 
                                 Notes = @Notes, CreatedByUserID = @CreatedByUserID
                             where TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);

                if(Notes != null && Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
                else
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
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

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            string query = @"Insert into Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) 
                             Values (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);

                                Update TestAppointments
                                    Set IsLocked = 1
                                    Where TestAppointmentID = @TestAppointmentID
                             SELECT Scope_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", TestResult);
                if (Notes != null && Notes != "")
                    command.Parameters.AddWithValue("@Notes", Notes);
                else
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        TestID = InsertedID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return TestID;
        }

        public static bool isTestExists(int TestID)
        {
            bool isExists = false;
            string query = @"Select Found=1 from Tests where TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;
            string query = @"Delete from Tests where TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
          (int PersonID, int LicenseClassID, byte TestTypeID, ref int TestID,
            ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

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



        public static DataTable GetAllTests()
        {
            DataTable dtTests = new DataTable();
            string query = @"Select * from Tests";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtTests.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return dtTests;
        }


        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte passedTestsCount = 0;
            string query = @"			 SELECT Count(*)
                        FROM   Tests INNER JOIN
             TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
			 where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestResult = 1;"; // Assuming 1 means passed
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                try
                {
                    connection.Open();
                    
                    object result = command.ExecuteScalar();

                    if (result != null && byte.TryParse(result.ToString(), out byte count))
                    {
                        passedTestsCount = count;
                    }
                   
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return passedTestsCount;
        }
      
    }
}
