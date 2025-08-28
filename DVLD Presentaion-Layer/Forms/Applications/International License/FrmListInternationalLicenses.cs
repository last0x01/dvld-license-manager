using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services;
using DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses;
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

namespace DVLD_Presentaion_Layer.Forms.Applications.International_License
{
    public partial class FrmListInternationalLicenses : Form
    {
        DataTable _dtAllInternationalLicenses;
        public FrmListInternationalLicenses()
        {
            InitializeComponent();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;

            }
            else {


                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if(cbFilterBy.Text != "None")
                {

                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void FrmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _dtAllInternationalLicenses;
            lblTotalRecords.Text = "Record: " + dgvInternationalLicenses.Rows.Count.ToString();


           /* if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";


                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";

            }
           */

        } 
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
 
                        FilterColumn = "ApplicationID";
                        break;
                    

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = "Record: " + dgvInternationalLicenses.Rows.Count.ToString();
                return;
            }



            _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblTotalRecords.Text = "Record: " + dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID(Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[2])).PersonID;

            FrmShowPersonDetails frm = new FrmShowPersonDetails(PersonID);
            frm.ShowDialog();
            FrmListInternationalLicenses_Load(null, null);

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmNewInternationalLicense frm = new FrmNewInternationalLicense();
            frm.ShowDialog();
            FrmListInternationalLicenses_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            FrmInternationalLicenseInfo frm = new FrmInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID =(int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
            FrmListInternationalLicenses_Load(null, null);

        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            FrmNewInternationalLicense frm = new FrmNewInternationalLicense();
            frm.ShowDialog();
            FrmListInternationalLicenses_Load(null, null);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e) { 
        string FilterColumn = "IsActive";
        string FilterValue = cbIsReleased.Text;

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
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

    lblTotalRecords.Text = "#Record: " + dgvInternationalLicenses.Rows.Count.ToString();
        }
    }
}
