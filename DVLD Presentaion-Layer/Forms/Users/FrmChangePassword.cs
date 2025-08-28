using DVLD_Business_Layer;
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
    public partial class FrmChangePassword : Form
    {
        clsUser _User;
        int _UserID = -1;

        public FrmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

        }

        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtCurrentPassword.Focus();
        }


        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("User Not found With this ID + " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                   "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Cannot Be Blank!");
  
                return;
            }

            else if(txtCurrentPassword.Text != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password Is Wrong!");
                txtCurrentPassword.Focus();
                return;

            }
            else if (txtNewPassword.Text == txtCurrentPassword.Text )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "It is match the Current Password Try another Password!");
                return;
            }
          
            else
            {

                errorProvider1.SetError(txtCurrentPassword, string.Empty);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");

            }
            else
            {

                errorProvider1.SetError(txtNewPassword, null);
            }
            ;
        }



        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtConfirmPassword, "Confirm Password cannot be blank");
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
               
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!.");
            }
            else
            {
          
                errorProvider1.SetError(txtConfirmPassword, string.Empty);
            }
        }

        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
