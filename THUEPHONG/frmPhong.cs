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
using DataLayer;
using BusinessLayer;

namespace THUEPHONG
{
    public partial class frmPhong : DevExpress.XtraEditors.XtraForm
    {
        Entities db;
        public frmPhong()
        {
            InitializeComponent();
            db = Entities.CreateEntities();
        }
        PHONG _phong;
        TANG _tang;
        LOAIPHONG _loaiphong;
        bool _them;
        int _idphong;

        private void frmPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _tang = new TANG();
            _loaiphong = new LOAIPHONG();

            loadData();
            loadTang();
            loadLoaiPhong();

            showHideControl(true);
            _enabled(false);

            // Ẩn cột ID để tránh hiển thị
            //gvDanhSach.Columns["IDTANG"].Visible = true;
            //gvDanhSach.Columns["IDTANG"].Width = 0;

            //gvDanhSach.Columns["IDLOAIPHONG"].Visible = true;
            //gvDanhSach.Columns["IDLOAIPHONG"].Width = 0;


        }
        void loadData()
        {
            var data = from p in db.tb_Phong
                       join t in db.tb_Tang on p.IDTANG equals t.IDTANG into t_join
                       from t in t_join.DefaultIfEmpty()
                       join lp in db.tb_LoaiPhong on p.IDLOAIPHONG equals lp.IDLOAIPHONG into lp_join
                       from lp in lp_join.DefaultIfEmpty()
                       select new
                       {
                           IDPHONG = p.IDPHONG,
                           TENPHONG = p.TENPHONG,
                           TANG = t != null ? t.TENTANG : "",         // Đổi tên trường thành TANG
                           IDTANG = t != null ? t.IDTANG : (int?)null,
                           LOAIPHONG = lp != null ? lp.TENLOAIPHONG : "", // Đổi tên trường thành LOAIPHONG
                           IDLOAIPHONG = lp != null ? lp.IDLOAIPHONG : (int?)null,
                           TRANGTHAI = p.TRANGTHAI,
                           DISABLED = p.DISABLED
                       };

            gcDanhSach.DataSource = data.ToList();
            gvDanhSach.OptionsBehavior.Editable = false;

            // Ẩn cột IDTANG và IDLOAIPHONG nếu cần
            gvDanhSach.Columns["IDTANG"].Visible = false;
            gvDanhSach.Columns["IDLOAIPHONG"].Visible = false;

        }

        void loadTang()
        {
            cboTang.DataSource = _tang.getAll();
            cboTang.DisplayMember = "TENTANG";
            cboTang.ValueMember = "IDTANG";
        }

        void loadLoaiPhong()
        {
            cboLoaiPhong.DataSource = _loaiphong.getAll();
            cboLoaiPhong.DisplayMember = "TENLOAIPHONG";
            cboLoaiPhong.ValueMember = "IDLOAIPHONG";
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
            txtTenPhong.Enabled = t;
            cboTang.Enabled = t;
            cboLoaiPhong.Enabled = t;
            chkTrangThai.Enabled = t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtTenPhong.Text = "";
            cboTang.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            chkTrangThai.Checked = false;
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
                _phong.delete(_idphong);
                OnDataUpdated?.Invoke(); // Kích hoạt sự kiện sau khi xóa thành công
            }
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(false);
            _enabled(true);
        }

        public delegate void UpdateDataHandler();
        public event UpdateDataHandler OnDataUpdated;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_Phong phong = new tb_Phong();
                phong.TENPHONG = txtTenPhong.Text;
                phong.IDTANG = (int)cboTang.SelectedValue;
                phong.IDLOAIPHONG = (int)cboLoaiPhong.SelectedValue;
                phong.TRANGTHAI = chkTrangThai.Checked;
                phong.DISABLED = chkDisabled.Checked;
                _phong.add(phong);
            }
            else
            {
                tb_Phong phong = _phong.getItem(_idphong);
                phong.TENPHONG = txtTenPhong.Text;
                phong.IDTANG = (int)cboTang.SelectedValue;
                phong.IDLOAIPHONG = (int)cboLoaiPhong.SelectedValue;
                phong.TRANGTHAI = chkTrangThai.Checked;
                phong.DISABLED = chkDisabled.Checked;
                _phong.update(phong);
            }

            // Kích hoạt sự kiện khi thêm hoặc sửa thành công
            OnDataUpdated?.Invoke();
            
            _them = false;
            loadData(); // Tải lại danh sách trong frmPhong
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
            _idphong = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDPHONG"));
            txtTenPhong.Text = gvDanhSach.GetFocusedRowCellValue("TENPHONG").ToString();

            // Lấy IDTANG từ cột ẩn và gán vào ComboBox
            if (gvDanhSach.GetFocusedRowCellValue("IDTANG") != null)
                cboTang.SelectedValue = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDTANG"));

            // Lấy IDLOAIPHONG từ cột ẩn và gán vào ComboBox
            if (gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG") != null)
                cboLoaiPhong.SelectedValue = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG"));



            chkTrangThai.Checked = (bool)gvDanhSach.GetFocusedRowCellValue("TRANGTHAI");
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