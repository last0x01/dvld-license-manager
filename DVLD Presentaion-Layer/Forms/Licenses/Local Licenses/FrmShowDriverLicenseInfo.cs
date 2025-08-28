using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Manage_Applications.Local_Forms
{
    public partial class FrmShowLicenseInfo : Form
    {
        private int _LicenseID;
        public FrmShowLicenseInfo(int LicenseID)
        {
            InitializeComponent();

            _LicenseID = LicenseID;
        }

        private void FrmShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(_LicenseID);
        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
