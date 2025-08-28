using DVLD_Business_Layer;
using DVLD_Presentaion_Layer.Global_Classes;
using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD_Presentaion_Layer.Forms.Users
{
    public partial class FrmLoginScreen : Form
    {
        enum enEyeMode { HidePass, ShowPass }
        enEyeMode _EyeMode = enEyeMode.HidePass;
        public FrmLoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {
                if (cbRemeberMe.Checked)
                {
                    clsGlobal.RememberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUserNameAndPassword("", "");
                }

                if (User.IsActive)
                {
                    clsGlobal.CurrentUser = User;
                    this.Hide();
                    MainMenu frm = new MainMenu(this);
                    frm.ShowDialog();
                }
                else
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "InActive Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void FrmLoginScreen_Load(object sender, EventArgs e)
        {
            string userName = "";
            string password = "";
             txtUserName.Focus();

            if (clsGlobal.GetStoredCredential(ref userName, ref password))
            {
                txtUserName.Text = userName;
                txtPassword.Text = password;
                cbRemeberMe.Checked = true;
            }
            else
            {
                cbRemeberMe.Checked = false;
               
            }

           
        }

        private void pbShowAndHidePassword_Click(object sender, EventArgs e)
        {
  

            switch (_EyeMode) {
                case enEyeMode.HidePass:
                    txtPassword.PasswordChar = '\0';
                    pbShowAndHidePassword.Image = Properties.Resources.Millenium_eye24;
                    _EyeMode = enEyeMode.ShowPass;
                    break;
                case enEyeMode.ShowPass:
                    txtPassword.PasswordChar = '*';
                    pbShowAndHidePassword.Image = Properties.Resources.Closed_eye;
                    _EyeMode = enEyeMode.HidePass;
                    break;



            }
        }
    }
}
