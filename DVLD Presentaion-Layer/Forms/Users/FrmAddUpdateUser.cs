using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Users
{
    public partial class FrmAddUpdateUser : Form
    {
         private enum enMode { AddNew, Update }
         private enMode _Mode = enMode.AddNew;

        clsUser _User;

        int _UserID ;

        public FrmAddUpdateUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
           
        }

        public FrmAddUpdateUser(int user)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = user;
           
        }

       
     
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                _User = new clsUser();
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                tpLoginInfo.Enabled = false;

            }
            
            if(_Mode == enMode.Update)
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true ;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            cbIsActive.Checked = true;
        }

        private void _LoadData()
        {
            
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            cbIsActive.Checked = _User.IsActive;
            lblUserID.Text = Convert.ToString(_User.UserID);
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            

            if (!this.ValidateChildren()) {

                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                   "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }


            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = cbIsActive.Checked;

            if(_User.Save())
            {
                MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  lblUserID.Text = Convert.ToString(_User.UserID);
                 _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Failed to save user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          

        }

        private void FrmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }

        }

      

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                    if (clsUser.DoesUserExistForPerson(ctrlPersonCardWithFilter1.PersonID))
                    {

                        MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                       
                    }

                    else
                    {
                        btnSave.Enabled = true;
                        tpLoginInfo.Enabled = true;
                        tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    }
                }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }

        }

       

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text))
            {

                errorProvider1.SetError(txtUserName, "User Name is required.");
                e.Cancel = true;
                txtUserName.BackColor = Color.LightPink;

            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
                txtUserName.BackColor = Color.White;
            }


            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExists(txtUserName.Text.Trim()))
                {
                    errorProvider1.SetError(txtUserName, "This User Name already exists. Please choose another one.");
                    e.Cancel = true;
                    txtUserName.BackColor = Color.LightPink;
                }
                else
                {
                 
                    errorProvider1.SetError(txtUserName, null);
                    txtUserName.BackColor = Color.White;
                }
            }
            else
            {
                if (txtUserName.Text != _User.UserName)
                {
                    errorProvider1.SetError(txtUserName, "This User Name already exists. Please choose another one.");
                    e.Cancel = true;
                    txtUserName.BackColor = Color.LightPink;
                }
                else
                {
                  
                    errorProvider1.SetError(txtUserName, null);
                    txtUserName.BackColor = Color.White;

                }
            }

        }

    

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtConfirmPassword.Text != txtPassword.Text)
            {
                errorProvider1.SetError(txtConfirmPassword, "Passwords do not match.");
                e.Cancel = true;
                txtConfirmPassword.BackColor = Color.LightPink;
            }
            else
            {
               
                errorProvider1.SetError(txtConfirmPassword, null);
                txtConfirmPassword.BackColor = Color.White;
            }
        }

     

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {

                errorProvider1.SetError(txtPassword, "Password Cannot be blank.");
                e.Cancel = true;
                txtPassword.BackColor = Color.LightPink;

            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
                txtPassword.BackColor = Color.White;
            }

        }

        private void FrmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
