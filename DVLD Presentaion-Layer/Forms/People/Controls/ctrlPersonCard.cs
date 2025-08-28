using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.People;
using DVLD_Presentaion_Layer.Properties;
using System;

using System.Windows.Forms;
using System.IO;

namespace DVLD_Presentaion_Layer.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPerson _Person;
        int _PersonID = -1;

        public clsPerson Person
        {
            get { return _Person; }
           
        }

        public int PersonID
        { get { return _PersonID; }
            
        }


        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void _FillPersonInfo()
        {
            LinklblEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo.ToString();

            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";

            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;

            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            _LoadPersonPhoto();

        }

        private void _LoadPersonPhoto()
        {
            pbPersonalPhoto.Image = (_Person.Gender == 0) ? Resources.Male_512 : Resources.Female_512;

            string imagePath = _Person.ImagePath;

            if(imagePath != "")
            {
                if(File.Exists(imagePath))
                {
                    pbPersonalPhoto.ImageLocation = imagePath;
                }
                else
                    MessageBox.Show("Could Not Find This Image: = " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        public void LoadPersonInfo(int PersonID)
        {
             _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _FillPersonInfo();
            }

           

        }

        public void ResetPersonInfo()
        {
            _PersonID  = -1;
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGender.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblAddress.Text = "[????]";
            lblPhone.Text = "[????]";
            lblEmail.Text = "[????]";
            lblCountry.Text = "[????]";
            pbPersonalPhoto.Image = Resources.Male_512;
            
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalNo = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _FillPersonInfo();
            }



        }


        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {
            
        }

        private void LinklblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmAddUpdatePerson frm = new FrmAddUpdatePerson(_PersonID);
            
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
