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
    public partial class frmThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmThietBi()
        {
            InitializeComponent();
        }
        THIETBI _thietbi;
        bool _them;
        int _idtb;
        private void frmThietBi_Load(object sender, EventArgs e)
        {
            _thietbi = new THIETBI();

            loadData();
            showHideControl(true);
            _enabled(false);


        }
        void loadData()
        {
            gcDanhSach.DataSource = _thietbi.getAll();
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
            txtTenThietBi.Enabled = t;
            nudDonGia.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTenThietBi.Text = "";
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
                _thietbi.delete(_idtb);
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

                tb_ThietBi tb = new tb_ThietBi();
                tb.TENTB = txtTenThietBi.Text;
                tb.DONGIA = (int)nudDonGia.Value;
                tb.DISABLED = chkDisabled.Checked;
                _thietbi.add(tb);
            }
            else
            {


                tb_ThietBi tb = _thietbi.getItem(_idtb);
                tb.TENTB = txtTenThietBi.Text;
                tb.DONGIA = (int)nudDonGia.Value;
                tb.DISABLED = chkDisabled.Checked;
                _thietbi.update(tb);
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
            _idtb = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDTB"));
            txtTenThietBi.Text = gvDanhSach.GetFocusedRowCellValue("TENTB").ToString();
            nudDonGia.Value = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("DONGIA"));
            chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
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