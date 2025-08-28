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

namespace DVLD_Presentaion_Layer.Forms.Tests
{
    public partial class FrmListTestAppointments : Form
    {
        DataTable _dtAllLicenseTestAppointments;
        int _LocalDrivingLicenseAppID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public FrmListTestAppointments()
        {
            InitializeComponent();
        }

        public FrmListTestAppointments(int LocalDrivingLicenseAppID , clsTestType.enTestType TestType )
        {
            InitializeComponent();

            _TestType = TestType;
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;

        }

        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            { 
                case clsTestType.enTestType.VisionTest:
                    pbTestTypePhoto.Image = Properties.Resources.Vision_512;
                    label1.Text = "Vision Test Appointments";
                    break;
                case clsTestType.enTestType.WrittenTest:
                    pbTestTypePhoto.Image = Properties.Resources.Written_Test_512;
                    label1.Text = "Written Test Appointments";
                    break;
                case clsTestType.enTestType.StreetTest:
                    pbTestTypePhoto.Image = Properties.Resources.driving_test_512;
                    label1.Text = "Street Test Appointments";
                    break;
            }

        }



        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseAppID);

            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("There is already an active or scheduled test appointment for this application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest Test = LocalDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (Test == null)
            {
                FrmScheduleTest frm1 = new FrmScheduleTest(_LocalDrivingLicenseAppID, _TestType);
                frm1.ShowDialog();
                FrmListTestAppointments_Load(null, null);
                return;
            }

            if (Test.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            FrmScheduleTest frm2 = new FrmScheduleTest(_LocalDrivingLicenseAppID, _TestType);
            frm2.ShowDialog();
            FrmListTestAppointments_Load(null, null);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void FrmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();

           ctrlLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseAppID);
            _dtAllLicenseTestAppointments = clsTestAppointment.GetAllTestAppointmentsPerTestType(_LocalDrivingLicenseAppID, _TestType);

            dgvLicenseTestAppointments.DataSource = _dtAllLicenseTestAppointments;

            lblTotalRecords.Text = "#Records: " + dgvLicenseTestAppointments.RowCount.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 120;
            }



        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            FrmScheduleTest frm = new FrmScheduleTest(_LocalDrivingLicenseAppID, _TestType , TestAppointmentID);
            frm.ShowDialog();
            FrmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            FrmTakeTest frm = new FrmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            FrmListTestAppointments_Load(null, null);

        }
    }
}
