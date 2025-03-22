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


namespace THUEPHONG
{
    public partial class frmDonVi : DevExpress.XtraEditors.XtraForm
    {
        public frmDonVi()
        {
            InitializeComponent();
        }
        DONVI _donvi;
        CONGTY _congty;
        bool _them;
        string _madvi;

        private void frmDonVi_Load(object sender, EventArgs e)
        {
            _donvi =  new DONVI();
            _congty = new CONGTY();
            loadCongTy();
            
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            cboCty.SelectedIndexChanged += CboCty_SelectedIndexChanged;
            loadDviByCty();

        }

        private void CboCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDviByCty();
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
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Text = "";
            chkDisabled.Checked = false;
        }
        void loadCongTy()
        {
            cboCty.DataSource = _congty.getAll();
            cboCty.DisplayMember = "TENCTY"; // hiển thị tên công ty trong cbo
            cboCty.ValueMember = "MACTY"; // lưu giá trị là mã công ty
        }
        void loadData()
        {
            gcDanhSach.DataSource= _donvi.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        void loadDviByCty()
        {
            gcDanhSach.DataSource = _donvi.getAll(cboCty.SelectedValue.ToString());
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            txtMa.Enabled = true;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _donvi.delete(_madvi);
            }
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
            txtMa.Enabled = false;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_DonVi dvi = new tb_DonVi();
                dvi.MADVI = txtMa.Text;
                dvi.MACTY = cboCty.SelectedValue.ToString();
                dvi.TENDVI = txtTen.Text;
                dvi.DIACHI = txtDiaChi.Text;
                dvi.SDT = txtDienThoai.Text;
                dvi.FAX = txtFax.Text;
                dvi.EMAIL = txtEmail.Text;
                dvi.DISABLED = chkDisabled.Checked;
                _donvi.add(dvi);

            }
            else
            {
                tb_DonVi dvi = _donvi.getItem(_madvi);
                dvi.MACTY = cboCty.SelectedValue.ToString();
                dvi.TENDVI = txtTen.Text;
                dvi.DIACHI = txtDiaChi.Text;
                dvi.SDT = txtDienThoai.Text;
                dvi.FAX = txtFax.Text;
                dvi.EMAIL = txtEmail.Text;
                dvi.DISABLED = chkDisabled.Checked;
                _donvi.update(dvi);
            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
            txtMa.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            
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
                _madvi = gvDanhSach.GetFocusedRowCellValue("MADVI").ToString();
                cboCty.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MACTY");

                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MADVI").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENDVI").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}