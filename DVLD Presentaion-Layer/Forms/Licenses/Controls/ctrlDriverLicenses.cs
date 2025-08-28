using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.Driving_Licenses_Services;
using DVLD_Presentaion_Layer.Forms.Manage_Applications.Local_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        int _DriverID = -1;
        clsDriver _Driver;
        DataTable _dtDriverLocalLicensesHistory;
        DataTable _dtDriverInternationalLicensesHistory;


        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(DriverID);


            if (_Driver == null)
            {
                MessageBox.Show("No Driver found for the given Driver ID = " + DriverID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadLocalDriverLicenses();
            _LoadInterationalDriverLicenses();
           
        }

        private void _LoadInterationalDriverLicenses()
        {
            
            _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);

                   
            
            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicensesHistory;

            lblInternationalCount.Text = "#Record: " + dgvInternationalLicensesHistory.Rows.Count.ToString();




            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
               

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
 
                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
   
                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
             

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
       
                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
            
            }
        }


        private void _LoadLocalDriverLicenses()
        {
            _dtDriverLocalLicensesHistory = clsLicense.GetDriverLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalCount.Text = "#Record: " +dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns["LicenseID"].HeaderText = "License ID";
                dgvLocalLicensesHistory.Columns["ApplicationID"].HeaderText = "Application ID";
                dgvLocalLicensesHistory.Columns["ClassName"].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns["IssueDate"].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns["ExpirationDate"].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns["IsActive"].HeaderText = "Is Active";
            }
            
        }


       public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);
           

            if (_Driver == null)
            {
               MessageBox.Show("There is no Driver linked with Person ID = "+ PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;

            _LoadLocalDriverLicenses();
            _LoadInterationalDriverLicenses();

        }


        private void ctrlDriverLicenses_Load(object sender, EventArgs e)
        {

        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = Convert.ToInt32(dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);

            FrmInternationalLicenseInfo frm = new FrmInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            FrmShowLicenseInfo frm = new FrmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        public void Clear()
        {
            _dtDriverInternationalLicensesHistory.Clear();
            _dtDriverLocalLicensesHistory.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
