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
using BusinessLayer;
using DataLayer;

namespace USERMANAGEMENT
{
    public partial class frmGroup : DevExpress.XtraEditors.XtraForm
    {
        public frmGroup()
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
        SYS_GROUP _sysGroup;
        VIEW_USER_IN_GROUP _vUserInGroup;
        private void frmGroup_Load(object sender, EventArgs e)
        {
            _sysUser = new SYS_USER();
            _sysGroup = new SYS_GROUP();
            if (!_them)
            {
                var user = _sysUser.getItem(_idUser);
                txtTenNhom.Text = user.USERNAME;
                _macty = user.MACTY;
                _madvi = user.MADVI;
                txtMoTa.Text = user.FULLNAME;
                txtTenNhom.ReadOnly = true;
                loadUserInGroup(_idUser);
            }
            else
            {
                txtMoTa.Text = "";
                txtTenNhom.Text = "";
            }
            

        }
        public void loadUserInGroup(int idGroup)
        {
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            gcThanhVien.DataSource = _vUserInGroup.getUserInGroup(_macty, _madvi, idGroup);
            gvThanhVien.OptionsBehavior.Editable = false;

        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenNhom.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên nhóm. Tên nhóm nhập không dấu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhom.SelectAll();
                txtTenNhom.Focus();
                return;
            }
            saveData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void saveData()
        {
            if (_them)
            {
                bool checkUser = _sysUser.checkUserExist(_macty, _madvi, txtTenNhom.Text.Trim());
                if (checkUser)
                {
                    MessageBox.Show("Nhóm đã tồn tại. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenNhom.SelectAll();
                    txtTenNhom.Focus();
                    return;
                }
                // thêm
                _user = new tb_SYS_USER();
                _user.USERNAME = txtTenNhom.Text.Trim();
                _user.FULLNAME = txtMoTa.Text;
                _user.ISGROUP = true;
                _user.DISABLED = false;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;
                _sysUser.add(_user);
                
            }
            else
            {
                _user = _sysUser.getItem(_idUser);
                _user.FULLNAME = txtMoTa.Text;
                _sysUser.update(_user);
            }
            objMain.loadUser(_macty, _madvi);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmShowMembers frm = new frmShowMembers();
            frm._idGroup = _idUser;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnBot_Click(object sender, EventArgs e)
        {
            if (gvThanhVien.GetFocusedRowCellValue("IDUSER") != null)
            {
                _sysGroup.delGroup(int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUSER").ToString()), _idUser);
                loadUserInGroup(_idUser);
            }
        }
    }
}