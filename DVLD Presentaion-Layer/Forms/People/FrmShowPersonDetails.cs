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

namespace DVLD_Presentaion_Layer.Forms.People
{
    public partial class FrmShowPersonDetails : Form
    {
     
        public FrmShowPersonDetails(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        public FrmShowPersonDetails(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NationalNo);

        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
