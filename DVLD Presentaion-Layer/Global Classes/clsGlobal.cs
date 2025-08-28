using DVLD_Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace DVLD_Presentaion_Layer.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUserNameAndPassword(string username, string password)
        {
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();

                string filePath = CurrentDirectory + "\\data.txt";

                if (username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return false;
                }

                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine($"{username}#//#{password}");
                    return true;
                }



            }
            catch (IOException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
        }

        public static bool GetStoredCredential(ref string username, ref string password)
        {

            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory();

                string filePath = CurrentDirectory + "\\data.txt";

                if (File.Exists(filePath))
                {


                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            if (parts.Length == 2)
                            {
                                username = parts[0];
                                password = parts[1];
                                return true;
                            }
                        }
                    }
                }
                else
                    return false;

            }


            catch (IOException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return true;
        }
    }
}
