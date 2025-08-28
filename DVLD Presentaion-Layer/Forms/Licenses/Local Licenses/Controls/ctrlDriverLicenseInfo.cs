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

namespace DVLD_Presentaion_Layer.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {

        int _LicenseID = -1;
        clsLicense _License;

        public int LicenseID
        {
            get { return _LicenseID; }

        }

        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }   

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }


        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void _LoadImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
            {
                pbPersonalPhoto.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbPersonalPhoto.Image = Properties.Resources.Female_512;
            }

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

           if(ImagePath != "")
            {
                if(File.Exists(ImagePath))
                {
                    pbPersonalPhoto.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Image file not found: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
          
        }

        public void LoadDriverLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            if(!clsLicense.IsLicenseExists(_LicenseID))
            {
                MessageBox.Show("License Does not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }


            lblLicenseID.Text = _License.LicenseID.ToString();
            lblClassType.Text = _License.LicenseClassInfo.ClassName;
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes ;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblGender.Text = (_License.DriverInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            _LoadImage();

            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = (_License.IsDetainedLicense) ? "Yes" : "No";
            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueReason.Text = _License.IssueReasonText;



        }
    }
    }
