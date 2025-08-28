using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses;
using DVLD_Presentaion_Layer.Forms.Manage_Applications.Local_Forms;
using DVLD_Presentaion_Layer.Forms.People;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Applications.Release_Detained_License
{
    public partial class FrmListDetainedLicenses : Form
    {
        DataTable _dtDetainedLicenses;
        public FrmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainedLicenseApplication frm = new FrmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            FrmDetainedLicenseApplication frm = new FrmDetainedLicenseApplication();
            frm.ShowDialog();

        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = "";
            
            switch (cbIsReleased.Text)
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

            if (cbIsReleased.Text == "All")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            }
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = "#Record: " + dgvDetainedLicenses.Rows.Count; 


        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;


                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = "#Record: " + dgvDetainedLicenses.Rows.Count;
                return;
            }


            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                //in this case we deal with numbers not string.
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblTotalRecords.Text = "#Record: " + dgvDetainedLicenses.Rows.Count;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if(cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
      
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text != "None")
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        private void FrmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            cbFilterBy.SelectedIndex = 0;

            lblTotalRecords.Text = "#Record: " + dgvDetainedLicenses.Rows.Count;

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

            }


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value.ToString());
            FrmShowPersonDetails frm = new FrmShowPersonDetails(Person.PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowLicenseInfo frm  = new FrmShowLicenseInfo
                (Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value));
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.Find(dgvDetainedLicenses.CurrentRow.Cells["NationalNo"].Value.ToString());
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(Person.PersonID);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedDetainID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells["LicenseID"].Value);
            FrmReleaseDetainedLicenseApplication frm = new FrmReleaseDetainedLicenseApplication(SelectedDetainID);
            frm.ShowDialog();
            FrmListDetainedLicenses_Load(null, null);

        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)(dgvDetainedLicenses.CurrentRow.Cells[3].Value);
        }
    }
}
