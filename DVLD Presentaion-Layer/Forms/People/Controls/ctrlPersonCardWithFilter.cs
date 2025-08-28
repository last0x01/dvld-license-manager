using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Forms.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnSelectedPerson;

        protected void PersonSelected(int PersonID)
        {
            Action<int> handler = OnSelectedPerson;

            if(handler != null)
            {
                handler(PersonID);
            }

        }



        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }

            set { _ShowAddPerson = value;
            btnAddNewPerson.Visible = value;
            }
        }

        private bool  _FilterEnabled = true;

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { _FilterEnabled = value;
                gbFilters.Enabled = value;
            }
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

       

       public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPerson
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
            }

            if (SelectedPerson != null && FilterEnabled)
            {
                OnSelectedPerson?.Invoke(PersonID);
               
            }
        }

     
  
        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                errorProvider1.SetError(txtFilterValue, "Please enter a value to search.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, string.Empty);
            }
        }

        private void DataBackFromAddNewPerson(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 1; // Reset to "Person ID"
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);

        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();
        }


        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            cbFilterBy.SelectedIndex = 0;
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            if(cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please fill the search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddUpdatePerson frm = new FrmAddUpdatePerson();
            frm.DataBack += DataBackFromAddNewPerson;
            frm.ShowDialog();
        }
    }
}
