﻿ using BusinessLayer;
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

namespace THUEPHONG
{
    public partial class frmPhong_ThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmPhong_ThietBi()
        {
            InitializeComponent();
        }
        public frmPhong_ThietBi(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        PHONG_THIETBI _ptb;
        PHONG _phong;
        THIETBI _thietbi;

        bool _them;
        int _idPhong;
        int _idTB;

        private void frmPhong_ThietBi_Load(object sender, EventArgs e)
        {
            _ptb = new PHONG_THIETBI();
            _phong = new PHONG();
            _thietbi = new THIETBI();

            loadPhong();     // Đổ dữ liệu vào cboPhong
            loadThietBi();   // Đổ dữ liệu vào cboThietBi
            loadData();      // Hiển thị danh sách tb_Phong_ThietBi

            showHideControl(true);
            _enabled(false);
        }

        void loadPhong()
        {
            cboPhong.DataSource = _phong.getAll();
            cboPhong.DisplayMember = "TENPHONG";    // cột tên phòng
            cboPhong.ValueMember = "IDPHONG";       // cột ID
        }

        void loadThietBi()
        {
            cboThietBi.DataSource = _thietbi.getAll();
            cboThietBi.DisplayMember = "TENTB";  // Phải khớp tên property
            cboThietBi.ValueMember = "IDTB";         // cột ID
        }

        void loadData()
        {
            // Join để hiển thị tên phòng, tên thiết bị
            var data = from ptb in _ptb.getAll()
                       join p in _phong.getAll() on ptb.IDPHONG equals p.IDPHONG
                       join tb in _thietbi.getAll() on ptb.IDTB equals tb.IDTB
                       select new
                       {
                           IDPHONG = ptb.IDPHONG,
                           IDTB = ptb.IDTB,
                           TENPHONG = p.TENPHONG,
                           TENTHIETBI = tb.TENTB, // <-- cột này
                           SOLUONG = ptb.SOLUONG,
                           DISABLED = ptb.DISABLED
                       };

            gcDanhSach.DataSource = data.ToList();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnXoa.Visible = t;
            btnSua.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            cboPhong.Enabled = t;
            cboThietBi.Enabled = t;
            nudSoLuong.Enabled = t;
        }

        void _reset()
        {
            cboPhong.SelectedIndex = 0;
            cboThietBi.SelectedIndex = 0;
            nudSoLuong.Value = 0;
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
            if (MessageBox.Show("Xóa thiết bị khỏi phòng?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _ptb.delete(_idPhong, _idTB);
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int idPhong = Convert.ToInt32(cboPhong.SelectedValue);
            int idTB = Convert.ToInt32(cboThietBi.SelectedValue);
            int soLuong = Convert.ToInt32(nudSoLuong.Value);

            if (_them)
            {
                // Kiểm tra trùng trước khi thêm
                var existing = _ptb.getItem(idPhong, idTB);
                if (existing != null)
                {
                    MessageBox.Show("Thiết bị này đã tồn tại trong phòng!\nBạn có thể nhấn 'Sửa' để thay đổi số lượng.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tb_Phong_ThietBi ptb = new tb_Phong_ThietBi
                {
                    IDPHONG = idPhong,
                    IDTB = idTB,
                    SOLUONG = soLuong
                };
                _ptb.add(ptb);
            }
            else
            {
                // Sửa thiết bị
                tb_Phong_ThietBi ptb = _ptb.getItem(_idPhong, _idTB);
                if (ptb != null)
                {
                    ptb.IDPHONG = idPhong;
                    ptb.IDTB = idTB;
                    ptb.SOLUONG = soLuong;
                    _ptb.update(ptb);
                }
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
            if (gvDanhSach.RowCount > 0)
            {
                var idPhongVal = gvDanhSach.GetFocusedRowCellValue("IDPHONG");
                var idTBVal = gvDanhSach.GetFocusedRowCellValue("IDTB");
                var soLuongVal = gvDanhSach.GetFocusedRowCellValue("SOLUONG");

                if (idPhongVal != null && idTBVal != null)
                {
                    _idPhong = Convert.ToInt32(idPhongVal);
                    _idTB = Convert.ToInt32(idTBVal);
                }

                if (cboPhong.Items.Count > 0)
                {
                    cboPhong.SelectedValue = _idPhong;
                }
                if (cboThietBi.Items.Count > 0)
                {
                    cboThietBi.SelectedValue = _idTB;
                }

                if (soLuongVal != null)
                {
                    nudSoLuong.Value = Convert.ToDecimal(soLuongVal);
                }
                
            }
        }
    }
}