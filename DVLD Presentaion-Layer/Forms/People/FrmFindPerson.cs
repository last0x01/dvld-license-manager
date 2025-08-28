using DVLD_Presentaion_Layer.Controls;
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
    public partial class FrmFindPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
   

        public FrmFindPerson()
        {
            InitializeComponent();

          
        }

        private void FrmFindUser_Load(object sender, EventArgs e)
        {

        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
            this.Close();
     
        }

        private void ctrlFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
