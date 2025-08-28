using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.People;

namespace DVLD_Presentaion_Layer.Forms.Users
{
    public partial class FrmListPeople : Form
    {
        public FrmListPeople()
        {
            InitializeComponent();
        }

        private static  DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(true, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gender", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");



        private void _ReffreshPeopleList()
        {

            _dtAllPeople = clsPerson.GetAllPeople();

            _dtPeople = _dtAllPeople.DefaultView.ToTable(true, "PersonID", "NationalNo",
                                                          "FirstName", "SecondName", "ThirdName", "LastName",
                                                          "Gender", "DateOfBirth", "CountryName",
                                                          "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;

            lblTotalRecords.Text = "#Records: " + dgvPeople.Rows.Count.ToString();

        }



        private void FrmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;

            cbFilterBy.SelectedIndex = 0;

            lblTotalRecords.Text = "#Records: " + dgvPeople.Rows.Count.ToString();

            if (_dtPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderCell.Value = "Person ID";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 70;

                dgvPeople.Columns[7].HeaderText = "Date of Birth";
                dgvPeople.Columns[7].Width = 120;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 180;


            }
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
                txtFilterValue.Visible = false;
            else
            {
                txtFilterValue.Text = "";
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);

            FrmShowPersonDetails frm = new FrmShowPersonDetails(PersonID);

            frm.ShowDialog();
            _ReffreshPeopleList();



        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frm = new FrmAddUpdatePerson();
            frm.ShowDialog();
            _ReffreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frm = new FrmAddUpdatePerson(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _ReffreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this person?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value);
                
                    if (clsPerson.DeletePerson(PersonID))
                    {
                        MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _ReffreshPeopleList();

                    }
                    else
                    {
                        MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtFindBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                   FilterColumn = "PersonID";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Country Name":
                    FilterColumn = "CountryName";
                    break;
                case "Phone":
                   FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                case "Gender":
                    FilterColumn = "Gender";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || cbFilterBy.Text== "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblTotalRecords.Text = "#Records: " + dgvPeople.Rows.Count.ToString();
                return;
            }
          
           

            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = [{1}]", FilterColumn, txtFilterValue.Text);
            }
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lblTotalRecords.Text = "#Records: " + dgvPeople.Rows.Count.ToString();
        }

      
        private void pbAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frm = new FrmAddUpdatePerson();
            frm.ShowDialog();
            _ReffreshPeopleList();
           
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }
    }
}
