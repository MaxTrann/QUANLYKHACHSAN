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
    public partial class frmLoaiPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
        }
        LOAIPHONG _loaiphong;
        bool _them;
        int _idloaiphong;

        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            _loaiphong = new LOAIPHONG();
            loadData();

            showHideControl(true);
            _enabled(false);
        }

        void loadData()
        {
            gcDanhSach.DataSource = _loaiphong.getAll();
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
            txtLoaiPhong.Enabled = t;
            nudDonGia.Enabled = t;
            nudSoNguoi.Enabled = t;
            nudSoGiuong.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtLoaiPhong.Text = "";
            nudDonGia.Value = 0;
            nudSoNguoi.Value = 0;
            nudSoGiuong.Value = 0;
            chkDisabled.Text = "";
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
                _loaiphong.delete(_idloaiphong);
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
                tb_LoaiPhong lp = new tb_LoaiPhong();
                lp.TENLOAIPHONG = txtLoaiPhong.Text;
                lp.DONGIA = (int)nudDonGia.Value;
                lp.SONGUOI = (int) nudSoNguoi.Value;
                lp.SOGIUONG = (int) nudSoGiuong.Value;
                lp.DISABLED = chkDisabled.Checked;
                _loaiphong.add(lp);
            }
            else
            {
                
                tb_LoaiPhong lp = _loaiphong.getItem(_idloaiphong);
                lp.TENLOAIPHONG = txtLoaiPhong.Text;
                lp.DONGIA = (int)nudDonGia.Value;
                lp.SONGUOI = (int)nudSoNguoi.Value;
                lp.SOGIUONG = (int)nudSoGiuong.Value;
                lp.DISABLED = chkDisabled.Checked;
                lp.DISABLED = chkDisabled.Checked;
                _loaiphong.update(lp);
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
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idloaiphong = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG"));
                nudDonGia.Value = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("DONGIA"));
                nudSoGiuong.Value = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("SOGIUONG"));
                nudSoNguoi.Value = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("SONGUOI"));
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
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
    }
}