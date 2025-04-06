using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace USERMANAGEMENT
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        public frmUser()
        {
            InitializeComponent();
        }
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        public string _macty;
        public string _madvi;
        public int _idUser;
        public string _username;
        public string _fullname;
        public bool _them;
        SYS_USER _sysUser;
        tb_SYS_USER _user;
        private void frmUser_Load(object sender, EventArgs e)
        {
            _sysUser = new SYS_USER();
            if (!_them)
            {
                var user = _sysUser.getItem(_idUser);
                txtUsername.Text = user.USERNAME;
                _macty = user.MACTY;
                _madvi = user.MADVI;
                txtHoTen.Text = user.FULLNAME;
                chkDisabled.Checked = user.DISABLED.Value;
                txtUsername.ReadOnly = true;

                txtPass.Text = Encryptor.Decrypt(user.PASSWORD, "qwert@123!poiuy", true);
                txtRePass.Text = Encryptor.Decrypt(user.PASSWORD, "qwert@123!poiuy", true);
            }
            else
            {
                txtHoTen.Text = "";
                txtPass.Text = "";
                txtRePass.Text = "";
                chkDisabled.Checked = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên người dùng. Tên nhóm nhập không dấu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;
            }

            if (!txtPass.Text.Equals(txtRePass.Text))
            {
                MessageBox.Show("Mật khẩu không trùng khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;
            }
            saveData();
        }
        void saveData()
        {
            if (_them)
            {
                bool checkUser = _sysUser.checkUserExist(_macty, _madvi, txtUsername.Text.Trim());
                if (checkUser)
                {
                    MessageBox.Show("Tên người dùng đã tồn tại. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.SelectAll();
                    txtUsername.Focus();
                    return;
                }
                // thêm
                _user = new tb_SYS_USER();
                _user.USERNAME = txtUsername.Text.Trim();
                _user.FULLNAME = txtHoTen.Text;
                _user.PASSWORD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.ISGROUP = false;
                _user.DISABLED = false;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;
                _sysUser.add(_user);

            }
            else
            {
                _user = _sysUser.getItem(_idUser);
                _user.FULLNAME = txtHoTen.Text;
                _user.PASSWORD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.ISGROUP = false;
                _user.DISABLED = chkDisabled.Checked;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;

                _sysUser.update(_user);
            }
            objMain.loadUser(_macty, _madvi);
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}