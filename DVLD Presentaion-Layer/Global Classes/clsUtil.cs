using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Global_Classes
{
    public class clsUtil
    {

        public static string GenerateGuid()
        {
            Guid NewGuid = Guid.NewGuid();

            return NewGuid.ToString();
        }

        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            string FileName = SourceFile;
           FileInfo fi = new FileInfo(FileName);
            
            return GenerateGuid() + fi.Extension;
        }

        public static bool CreateFolderIfNotExists(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (IOException iox)
                {
                   MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = "C:/DVLD_People_Images/";

            if(!CreateFolderIfNotExists(DestinationFolder))
            {
                return false;
            }


            string destinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourceFile);

            try
            {
                File.Copy(SourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            SourceFile = destinationFile; // Update the source file path to the new location
            return true;

        }
    }
}
