using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Licenses.International_License.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID = -1;
        private clsInternationalLicense _InternationalLicense;
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0)
            {
                pbPersonalPhoto.Image = Properties.Resources.Male_512;
            }
            else
                pbPersonalPhoto.Image = Properties.Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonalPhoto.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Image file not found at " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("There is no International License with ID = " + InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            if(!clsInternationalLicense.IsInternationalLicenseExists(InternationalLicenseID))
            {
                MessageBox.Show("There is no International License with ID = " + InternationalLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            _InternationalLicenseID = InternationalLicenseID;

            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();

            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
            lblIsActive.Text = _InternationalLicense.IsActive.ToString();
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblGender.Text = (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            _LoadImage();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlDriverInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
