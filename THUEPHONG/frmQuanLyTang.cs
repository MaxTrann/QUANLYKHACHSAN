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
    public partial class frmQuanLyTang : DevExpress.XtraEditors.XtraForm
    {
        public frmQuanLyTang()
        {
            InitializeComponent();
        }

        TANG _tang;
        bool _them;
        int _matang;
        private void frmQuanLyTang_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
            loadData();

            showHideControl(true);
            _enabled(false);
        }

        void loadData()
        {
            gcDanhSach.DataSource = _tang.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        // Ẩn/Hiện các button khi thao tác
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
            txtTenTang.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTenTang.Text = "";
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
                _tang.delete(_matang);
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
                tb_Tang tang = new tb_Tang();
                tang.TENTANG = txtTenTang.Text;
                tang.DISABLED = chkDisabled.Checked;
                _tang.add(tang);

            }
            else
            {
                
                tb_Tang tang = _tang.getItem(_matang);
                tang.TENTANG = txtTenTang.Text;
                tang.DISABLED = chkDisabled.Checked;
                _tang.update(tang);
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

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_28px;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            _matang = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTANG").ToString());
            txtTenTang.Text = gvDanhSach.GetFocusedRowCellValue("TENTANG").ToString();
            chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
        }
    }
}