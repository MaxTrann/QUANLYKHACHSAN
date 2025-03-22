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

namespace THUEPHONG
{
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        KHACHHANG _khachhang;
        bool _them;
        int _makh;
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            _khachhang = new KHACHHANG();
            loadData();
            showHideControl(true);
            _enabled(false);
        }

        void loadData()
        {
            gcDanhSach.DataSource = _khachhang.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            txtHoTen.Enabled = t;
            txtCCCD.Enabled = t;
            txtSDT.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkNam.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtHoTen.Text = "";
            txtCCCD.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";

            chkNam.Checked = false;
            chkDisabled.Checked = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _khachhang.delete(_makh);
            }
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_KhachHang kh = new tb_KhachHang();
                kh.HOTEN = txtHoTen.Text;
                kh.SDT = txtSDT.Text;
                kh.CCCD = txtCCCD.Text;
                kh.EMAIL = txtEmail.Text;
                kh.DIACHI = txtDiaChi.Text;
                kh.GIOITINH = chkNam.Checked;
                kh.DISABLED = chkDisabled.Checked;
                _khachhang.add(kh);
            }
            else
            {

                tb_KhachHang kh = _khachhang.getItem(_makh);
                kh.HOTEN = txtHoTen.Text;
                kh.SDT = txtSDT.Text;
                kh.CCCD = txtCCCD.Text;
                kh.EMAIL = txtEmail.Text;
                kh.DIACHI = txtDiaChi.Text;
                kh.GIOITINH = chkNam.Checked;
                kh.DISABLED = chkDisabled.Checked;
                _khachhang.update(kh);
            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            _reset();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_28px;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }

        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _makh = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDKH"));
                txtHoTen.Text = gvDanhSach.GetFocusedRowCellValue("HOTEN").ToString();
                txtCCCD.Text = gvDanhSach.GetFocusedRowCellValue("CCCD").ToString();
                txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkNam.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("GIOITINH").ToString());
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}