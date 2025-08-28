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
    public partial class FrmShowUserInfo : Form
    {
       
        int _UserID = -1;
        public FrmShowUserInfo(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

          
        }


        private void FrmShowUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlUserCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
