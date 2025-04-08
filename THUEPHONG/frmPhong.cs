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
using static DevExpress.XtraEditors.Mask.MaskSettings;

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
        public frmPhong(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            db = Entities.CreateEntities();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
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

        }
        void loadData()
        {
            var data = from p in db.tb_Phong
                       select new
                       {
                           IDPHONG = p.IDPHONG,
                           TENPHONG = p.TENPHONG,
                           IDTANG = p.IDTANG,
                           IDLOAIPHONG = p.IDLOAIPHONG,
                           TRANGTHAI = p.TRANGTHAI,
                           DISABLED = p.DISABLED
                       };

            gcDanhSach.DataSource = data.ToList();
            gvDanhSach.OptionsBehavior.Editable = false;

            // Thêm cột Unbound cho TẦNG và LOẠI PHÒNG
            if (!gvDanhSach.Columns.Contains(gvDanhSach.Columns["TENTANG"]))
            {
                var colTenTang = gvDanhSach.Columns.AddField("TENTANG");
                colTenTang.UnboundType = DevExpress.Data.UnboundColumnType.String;
                colTenTang.Caption = "TẦNG";
                colTenTang.Visible = true;
            }

            if (!gvDanhSach.Columns.Contains(gvDanhSach.Columns["TENLOAIPHONG"]))
            {
                var colTenLoaiPhong = gvDanhSach.Columns.AddField("TENLOAIPHONG");
                colTenLoaiPhong.UnboundType = DevExpress.Data.UnboundColumnType.String;
                colTenLoaiPhong.Caption = "LOẠI PHÒNG";
                colTenLoaiPhong.Visible = true;
            }

            // Kích hoạt sự kiện Unbound để load dữ liệu cho các cột Unbound
            gvDanhSach.CustomUnboundColumnData += gvDanhSach_CustomUnboundColumnData;
        }

        private void gvDanhSach_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var idTang = Convert.ToInt32(gvDanhSach.GetListSourceRowCellValue(e.ListSourceRowIndex, "IDTANG"));
                var idLoaiPhong = Convert.ToInt32(gvDanhSach.GetListSourceRowCellValue(e.ListSourceRowIndex, "IDLOAIPHONG"));

                if (e.Column.FieldName == "TENTANG")
                {
                    var tang = _tang.getItem(idTang);
                    e.Value = tang != null ? tang.TENTANG : string.Empty;
                }
                else if (e.Column.FieldName == "TENLOAIPHONG")
                {
                    var loaiPhong = _loaiphong.getItem(idLoaiPhong);
                    e.Value = loaiPhong != null ? loaiPhong.TENLOAIPHONG : string.Empty;
                }
            }
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
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _phong.delete(_idphong);
                OnDataUpdated?.Invoke(); // Kích hoạt sự kiện sau khi xóa thành công
            }
            loadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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