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
    public partial class frmSPDV : DevExpress.XtraEditors.XtraForm
    {
        public frmSPDV()
        {
            InitializeComponent();
        }
        SPDV _spdv;
        bool _them;
        int _idsp;
        private void frmSPDV_Load(object sender, EventArgs e)
        {
            _spdv = new SPDV();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void loadData()
        {
            gcDanhSach.DataSource = _spdv.getAll();
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
            txtTen.Enabled = t;
            nudDonGia.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            nudDonGia.Value = 0;
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
                _spdv.delete(_idsp);
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
                
                tb_SanPham sp = new tb_SanPham();
                sp.TENSP = txtTen.Text;
                sp.DONGIA = (int)nudDonGia.Value;
                sp.DISABLED = chkDisabled.Checked;
                _spdv.add(sp);
            }
            else
            {

                
                tb_SanPham sp = _spdv.getItem(_idsp);
                sp.TENSP = txtTen.Text;
                sp.DONGIA = (int)nudDonGia.Value;
                sp.DISABLED = chkDisabled.Checked;
                _spdv.update(sp);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            _idsp = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDSP"));
            txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENSP").ToString();
            nudDonGia.Value = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("DONGIA"));
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