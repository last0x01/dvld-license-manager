using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Layer
{
    public class clsApplicationData
    {
        public static bool GetApplicationByID(int ApplicationID, ref int  applicantPersonID, ref DateTime ApplicationDate, ref int  applicationTypeID, ref byte applicationStatus, ref DateTime lastStatusDate, ref float PaidFees, ref int createdByUserID)
        {
            bool isFound = true;

            string query = @"Select * from Applications where ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        applicantPersonID = reader.GetInt32(reader.GetOrdinal("ApplicantPersonID"));
                        applicationTypeID = reader.GetInt32(reader.GetOrdinal("ApplicationTypeID"));
                        applicationStatus = reader.GetByte(reader.GetOrdinal("ApplicationStatus"));
                        lastStatusDate = reader.GetDateTime(reader.GetOrdinal("LastStatusDate"));
                        PaidFees = Convert.ToSingle(reader["PaidFees"]);
                        createdByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));

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

        public static bool UpdateApplication(int ApplicationID, int applicantPersonID, DateTime ApplicationDate, int applicationTypeID, short applicationStatus, DateTime lastStatusDate, float PaidFees, int createdByUserID)
        {

            int rowsAffected = 0;

            string query = @"Update Applications set ApplicantPersonID = @ApplicantPersonID, ApplicationTypeID = @ApplicationTypeID, ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @createdByUserID where ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
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

        public static int AddNewApplication(int applicantPersonID,DateTime AppLicationDate, int applicationTypeID, short applicationStatus, DateTime lastStatusDate, float PaidFees, int createdByUserID)
        {
            int ApplicationID = -1;
            string query = @"Insert into Applications (ApplicantPersonID,AppLicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) 
                             Values (@ApplicantPersonID,@AppLicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID)
                                SELECT Scope_IDENTITY()";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", applicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", applicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", lastStatusDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@AppLicationDate", AppLicationDate);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        ApplicationID = InsertedID;
                    }

                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return ApplicationID;
        }

        public static bool isApplicationExists(int ApplicationID)
        {
            bool isExists = false;
            string query = @"Select Found=1 from Applications where ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            string query = @"Delete from Applications where ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            string query = @"Select ActiveApplicationID= ApplicationID from Applications where ApplicationID = @ApplicationID and ApplicantPersonID = @ApplicantPersonID and ApplicationStatus = 1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ApplicationID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();


                    if (result != null && int.TryParse(result.ToString(), out int AppID))
                    {
                        ActiveApplicationID = AppID;
                    }
                }
                catch (Exception ex)
                {
                  
                }
               
            }

            return ActiveApplicationID;
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetApplicationID(int PersonID,  int LicenseClassID)
        {
            int ApplicationID = -1;
            string query = @"Select  A.ApplicationID from Applications A inner join LocalDrivingLicenseApplications L on L.ApplicationID = A.ApplicationID
                                where L.LicenseClassID = @LicenseClassID and A.ApplicantPersonID = @ApplicantPersonID

                                
";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);

                try
                {
                    connection.Open();
                    object  Result = command.ExecuteScalar();
                    if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                    {
                        ApplicationID = insertedID;

                      
                    }
                   
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }
            }
            return ApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClassID(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ApplicationID = -1;

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID= @ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int AppID))
                    {
                        ApplicationID = AppID;
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);
                }


                
            } 
            return ApplicationID;

        }
        public static bool UpdateStatus(int ApplicationID, short ApplicationStatus)
        {
            int rowsAffected = 0;
            string query = @"Update Applications set ApplicationStatus = @ApplicationStatus where ApplicationID = @ApplicationID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                
                
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

                try
                {
                   
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch
                (Exception Ex)
                {
                    
                }
            }

                return rowsAffected > 0;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dtApplications = new DataTable();

            string query = @"Select * from Applications";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtApplications.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Error: " + Ex.Message);

                }



            }

            return dtApplications;
        }
    }
}
