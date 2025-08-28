using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Applications.Local_Driving_License
{
    public partial class FrmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _ApplicationID = -1;
        public FrmLocalDrivingLicenseApplicationInfo(int applicationID)
        {
            InitializeComponent();
            _ApplicationID = applicationID;
        }



        private void FrmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
        ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseAppID(_ApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
