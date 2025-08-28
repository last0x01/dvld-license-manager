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

namespace DVLD_Presentaion_Layer.Forms.ApplicationsType
{
    public partial class FrmListApplicationTypes : Form
    {
        DataTable _dtAllApplicationType;
        public FrmListApplicationTypes()
        {
            InitializeComponent();
        }

        

        private void FrmListApplicationType_Load(object sender, EventArgs e)
        {
           _dtAllApplicationType = clsApplicationType.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationType;
            lblTotalRecords.Text = "#Records: " + dgvApplicationTypes.Rows.Count;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAllApplicationTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicatinTypeID = Convert.ToInt32(dgvApplicationTypes.CurrentRow.Cells[0].Value);

            FrmEditApplicationType frmUpdateApplicationType = new FrmEditApplicationType(ApplicatinTypeID);
            frmUpdateApplicationType.ShowDialog();
            FrmListApplicationType_Load(null, null);

        }
    }
}
