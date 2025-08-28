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

namespace DVLD_Presentaion_Layer.Forms.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {

        public event Action<int> OnLicenseSelected;
        protected void LicenseSelected(int LicenseID)
                {
                    Action<int> handler = OnLicenseSelected;
                    if(handler != null)
                    {
                        handler(LicenseID);
                    }
                }


        int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
          
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
           
        }

        public bool _EnabledFilter = true;

        public bool FilterEnabled
        {
            get { return _EnabledFilter; }
            set
            {
                _EnabledFilter = value;
                gbFilter.Enabled = _EnabledFilter;
            }
        }
     
      
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);
           _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
          

            if (SelectedLicenseInfo != null && FilterEnabled)
            {
                OnLicenseSelected(_LicenseID);
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("License ID is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            int LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(LicenseID);
        }


        public void txtLicenseIDFoucs()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.';

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();

            }
        }

        private void DriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseID.Text))
            {
                errorProvider1.SetError(txtLicenseID, "Please enter a value to search.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, string.Empty);
            }
        }
    }
}
