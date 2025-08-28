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
    public partial class FrmListUsers : Form
    {
        DataTable _dtAllUsers;
        public FrmListUsers()
        {
            InitializeComponent();
        }

       


    

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbAddNewUser_Click(object sender, EventArgs e)
        {
            FrmAddUpdateUser frmAddEditUser = new FrmAddUpdateUser();
            frmAddEditUser.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void FrmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();

            cbFilterBy.SelectedIndex = 0; 

            dgvUsers.DataSource = _dtAllUsers;

            lblTotalRecords.Text = "#Records: " + dgvUsers.Rows.Count.ToString();


            if (_dtAllUsers.Rows.Count > 0)
            {
                
                dgvUsers.Columns[0].HeaderText = "User ID"; 
                dgvUsers.Columns[1].HeaderText = "Person ID"; 
                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[3].HeaderText = "UserName"; 
                dgvUsers.Columns[4].HeaderText = "Is Active"; 
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int userID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

                if(clsUser.DeleteUser(userID))
                {
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmListUsers_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Failed to delete user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowUserInfo frmShowUserDetails = new FrmShowUserInfo(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frmShowUserDetails.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdateUser frmAddEditUser = new FrmAddUpdateUser();
            frmAddEditUser.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);
            FrmAddUpdateUser frmAddEditUser = new FrmAddUpdateUser(UserID);
            frmAddEditUser.ShowDialog();
            FrmListUsers_Load(null, null);
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangePassword frm = new FrmChangePassword(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));

            frm.ShowDialog();
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;


                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

  

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                    case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                    case "Full Name":
                    FilterColumn = "FullName";
                    break;

                    case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "None":
                    FilterColumn = "None";
                    break;

            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Empty;
                lblTotalRecords.Text = "#Records: " + dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            }
            lblTotalRecords.Text = "#Records: " + dgvUsers.Rows.Count.ToString();


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
                lblTotalRecords.Text = "#Records: " + dgvUsers.Rows.Count.ToString();
            }

            lblTotalRecords.Text = "#Records: " + dgvUsers.Rows.Count.ToString();
        }

     
    }
}
