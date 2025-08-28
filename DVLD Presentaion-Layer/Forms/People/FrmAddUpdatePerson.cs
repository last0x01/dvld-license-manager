using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Controls;
using DVLD_Presentaion_Layer.Global_Classes;
using DVLD_Presentaion_Layer.Properties;
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

namespace DVLD_Presentaion_Layer.Forms.People
{
    public partial class FrmAddUpdatePerson : Form
    {


        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        public enum enGender { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;


        public FrmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
           

        }

        public FrmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefualtValues()
        {
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person ";

            }

           

            LinklblRemoveImage.Visible = (pbPersonalPhoto.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Iraq");

            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty;
            txtThirdName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtNationalNo.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            rbMale.Checked = true;

            if (rbMale.Checked)
            {
                pbPersonalPhoto.Image = Resources.Male_512;
            }
            else
                pbPersonalPhoto.Image = Resources.Female_512;


        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            txtNationalNo.Text = _Person.NationalNo;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (pbPersonalPhoto.ImageLocation != "")
            {
                pbPersonalPhoto.ImageLocation = _Person.ImagePath;
            }

            LinklblRemoveImage.Visible = (_Person.ImagePath != "");

        }

        private void FrmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private bool _IsValidPhone(string Phone)
        {
            if (string.IsNullOrEmpty(Phone) || string.IsNullOrWhiteSpace(Phone))
                return false;

            return Phone.Length >= 6 && Phone.All(char.IsDigit);
        }


        private bool _HandlePersonPhoto()
        {
            if (pbPersonalPhoto.ImageLocation != _Person.ImagePath)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath); // Delete the old image file if it exists
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Error deleting old image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (pbPersonalPhoto.ImageLocation != null)
            {
                string SourceImageFile = pbPersonalPhoto.ImageLocation; // Update the image path in the person object

                if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile)) 
                    {
                    _Person.ImagePath = SourceImageFile;
                    return true;

                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            return true;
           
        }
        

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbPersonalPhoto.ImageLocation == null)
            {
                pbPersonalPhoto.Image = Resources.Male_512;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbPersonalPhoto.ImageLocation == null)
            {
                pbPersonalPhoto.Image = Resources.Female_512;
            }
        }

   
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == string.Empty)
            {
                return;
            }

            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.BackColor = Color.MistyRose;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                txtEmail.BackColor = Color.White;
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
           

            if (string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                e.Cancel = true; // Cancel the event to prevent focus loss
                txtNationalNo.BackColor = Color.MistyRose;
                errorProvider1.SetError(txtNationalNo, "This Field Is requierd.");

            }
            else
            {
                txtNationalNo.BackColor = Color.White;
                errorProvider1.SetError(txtNationalNo, null);
            }

            if (clsPerson.IsPersonExists(txtNationalNo.Text.Trim()) && txtNationalNo.Text.Trim() != _Person.NationalNo)
            {
                e.Cancel = true; // Cancel the event to prevent focus loss
                txtNationalNo.BackColor = Color.MistyRose;
                errorProvider1.SetError(txtNationalNo, "The National No is Allready Exists.");

            }
            else
            {
                txtEmail.BackColor = Color.White;

                errorProvider1.SetError(txtNationalNo, null);

            }

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
                Temp.BackColor = Color.MistyRose;
            }
            else
            {
                //e.Cancel = false;
                Temp.BackColor = Color.White;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void LinklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath=openFileDialog1.FileName;
                pbPersonalPhoto.Load(SelectedFilePath); 
                LinklblRemoveImage.Visible = true;
            }
            

        }

        private void LinklblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonalPhoto.ImageLocation = null; // Clear the PictureBox image
            LinklblRemoveImage.Visible = false;
            if (rbMale.Checked)
                pbPersonalPhoto.Image = Resources.Male_512;
            else
                pbPersonalPhoto.Image = Resources.Female_512;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!_HandlePersonPhoto())
            {
                return;
            }

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Address = txtAddress.Text.Trim();

            _Person.Gender = (rbMale.Checked) ? (byte)enGender.Male : (byte)enGender.Female;

            _Person.NationalityCountryID = NationalityCountryID;


            if(pbPersonalPhoto.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonalPhoto.ImageLocation;
            }
            else
            {
                _Person.ImagePath = string.Empty; // Clear the image path if no image is set
            }

            if(_Person.Save())
            {
              
                _Mode = enMode.Update;
                lblPersonID.Text = _Person.PersonID.ToString();
                lblTitle.Text = "Update Person";
                MessageBox.Show("The Person Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _PersonID);
            }
            else
            {
                MessageBox.Show("Failed To Save Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
                {
                    this.Close();
                }


        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {

            if (!_IsValidPhone(txtPhone.Text.Trim()))
            {
                e.Cancel = true; // Cancel the event to prevent focus loss
                txtPhone.Focus();
                txtPhone.BackColor = Color.MistyRose;
                errorProvider1.SetError(txtPhone, "Invalid Phone Number Format!");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
                txtPhone.BackColor = Color.White;

            }
        }
      
      
    }
    }
