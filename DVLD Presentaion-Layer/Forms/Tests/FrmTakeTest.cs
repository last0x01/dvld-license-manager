using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Tests
{
    public partial class FrmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestType.enTestType _TestTypeID;

        private clsTest _Test;


        public FrmTakeTest(int TestAppointmentID, clsTestType.enTestType TestTypeID )
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void FrmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);

            if(ctrlScheduledTest1.TestAppointmentID ==-1)
            
                    btnSave.Enabled = false;
            else
                 btnSave.Enabled = true;

            

            int TestID = ctrlScheduledTest1.TestID;
            if (TestID != -1)
            {
                _Test = clsTest.Find(TestID);
                lblUserMessage.Visible = true;

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                rbPass.Enabled = false;
                rbFail.Enabled = false;
                btnSave.Enabled = false;

            }
            else
            {
                _Test = new clsTest();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _TestAppointmentID;
 
            _Test.Notes = txtNotes.Text;

            _Test.TestResult = rbPass.Checked; 
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                MessageBox.Show("Test result saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
              btnSave.Enabled = false;
               
            }
            else
            {
                MessageBox.Show("Failed to save test result. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
