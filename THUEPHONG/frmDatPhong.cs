using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataLayer;
using DevExpress.Utils.Gesture;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
    public partial class frmDatPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmDatPhong()
        {
            InitializeComponent();
            DataTable tb = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C where A.IDTANG= B.IDTANG AND A.TRANGTHAI = 0 AND A.IDLOAIPHONG = C.IDLOAIPHONG");
            gcPhong.DataSource = tb;
            gcDatPhong.DataSource = tb.Clone();
        }

        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        bool _them;
        int _idPhong = 0;
        string _tenPhong;
        string _macty;
        string _madvi;
        int _idDP = 0;
        int _rowDatPhong = 0;
        double _tongtien = 0;

        List<OBJ_DPSP> lstDPSP;
        SYS_PARAM _param;

        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongchitiet;
        DATPHONG_SANPHAM _datphongsanpham;
        KHACHHANG _khachhang;
        SPDV _sanpham;
        PHONG _phong;
        GridHitInfo downHitInfor = null;

        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _datphongchitiet = new DATPHONG_CHITIET();
            _datphongsanpham = new DATPHONG_SANPHAM();
            _khachhang = new KHACHHANG();
            _sanpham = new SPDV();

            _phong = new PHONG();

            lstDPSP = new List<OBJ_DPSP>();
            dtTuNgay.Value = myFunctions.GetFirstDayInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dtDenNgay.Value = DateTime.Now;

            
            _macty = myFunctions._macty;
            _madvi = myFunctions._madvi;

            loadKH();
            loadSP();
            loadDanhSach();

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.DisplayMember = "_display";

            showHideControl(true);
            _enabled(false);
             
            gvPhong.ExpandAllGroups();
            tabDanhDanh.SelectedTabPage = pageDanhSach;

            dtNgayDat.ValueChanged += dtNgay_ValueChanged;
            dtNgayTra.ValueChanged += dtNgay_ValueChanged;


        }
        private void dtNgay_ValueChanged(object sender, EventArgs e)
        {

            updateTongTien();
        }

        void loadDanhSach()
        {
            _datphong = new DATPHONG();
            gcDanhSach.DataSource = _datphong.getAll(dtTuNgay.Value, dtDenNgay.Value, _macty, _madvi);
            gcDanhSach.RefreshDataSource(); // Làm mới giao diện
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        public void loadKH()
        {
            _khachhang = new KHACHHANG();
            cboKhachHang.DataSource = _khachhang.getAll();
            cboKhachHang.DisplayMember = "HOTEN";
            cboKhachHang.ValueMember = "IDKH";
        }

        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }

        void addReset()
        {
            DataTable tb = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C where A.IDTANG= B.IDTANG AND A.TRANGTHAI = 0 AND A.IDLOAIPHONG = C.IDLOAIPHONG");
            gcPhong.DataSource = tb;
            gcDatPhong.DataSource = tb.Clone();
            gvPhong.ExpandAllGroups();
            gcSPDV.DataSource = _datphongsanpham.getAllByDatPhong(0);
            txtThanhTien.Text = "0";
            lstDPSP.Clear(); // Reset danh sách sản phẩm/dịch vụ
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            addReset();
            _idPhong = 0;
            _tenPhong = null;
            tabDanhDanh.SelectedTabPage = pageChiTiet;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = false;
            _enabled(true);
            showHideControl(false);
            tabDanhDanh.SelectedTabPage = pageChiTiet;

            // Gán _idPhong cho phòng đầu tiên trong danh sách phòng đã đặt (nếu có)
            if (gvDatPhong.RowCount > 0)
            {
                _idPhong = int.Parse(gvDatPhong.GetRowCellValue(0, "IDPHONG").ToString());
                _tenPhong = gvDatPhong.GetRowCellValue(0, "TENPHONG").ToString();
            }
            else
            {
                _idPhong = 0;
                _tenPhong = null;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _datphong.delete(_idDP);
                var lstDPCT = _datphongchitiet.getAllByDatPhong(_idDP);
                foreach (var item in lstDPCT)
                {
                    _phong.updateStatus((int)item.IDPHONG, false);
                }
            }
            loadDanhSach();
            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtNgayTra.Value.Date <= dtNgayDat.Value.Date)
            {
                MessageBox.Show("Ngày trả phải sau ngày đặt ít nhất 1 ngày.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                saveData();
                loadDanhSach(); // Làm mới danh sách
                objMain.gControl.Gallery.Groups.Clear();
                objMain.showRoom();

                

                _them = false;
                _enabled(false);
                showHideControl(true);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void saveData()
        {
            if (_them) // Chế độ thêm mới
            {
                tb_DatPhong dp = new tb_DatPhong();
                tb_DatPhong_CT dpct;
                tb_DatPhong_SanPham dpsp;

                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.EditValue.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.THEODOAN = chkDoan.Checked;
                dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.DISABLED = false;
                dp.IDUSER = 1;
                dp.MACTY = _macty;
                dp.MADVI = _madvi;
                dp.CREATED_DATE = DateTime.Now;

                var _dp = _datphong.add(dp);
                _idDP = _dp.IDDP;

                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    dpct = new tb_DatPhong_CT();
                    dpct.IDDP = _dp.IDDP;
                    dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                    dpct.SONGAYO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1); // Ít nhất 1 ngày
                    dpct.DONGIA = int.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                    dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;
                    dpct.NGAY = DateTime.Now;
                    var _dpct = _datphongchitiet.add(dpct);

                    _phong.updateStatus(int.Parse(dpct.IDPHONG.ToString()), true);

                    if (gvSPDV.RowCount > 0)
                    {
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            if (dpct.IDPHONG == int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString()))
                            {
                                dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                dpsp.IDPHONG = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                                dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                                dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                                dpsp.DONGIA = int.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                                dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                                _datphongsanpham.add(dpsp);
                            }
                        }
                    }
                }
            }
            else // Chế độ chỉnh sửa
            {
                tb_DatPhong dp = _datphong.getItem(_idDP);
                if (dp == null)
                {
                    MessageBox.Show("Booking not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var originalRooms = _datphongchitiet.getAllByDatPhong(_idDP).Select(x => x.IDPHONG).ToList();
                var currentRooms = new List<int>();
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    var idPhongVal = gvDatPhong.GetRowCellValue(i, "IDPHONG");
                    if (idPhongVal != null)
                        currentRooms.Add(int.Parse(idPhongVal.ToString()));
                }

                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.Value.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.SOTIEN = double.TryParse(txtThanhTien.Text, out double total) ? total : 0;
                dp.GHICHU = txtGhiChu.Text;
                dp.IDUSER = 1;
                dp.UPDATE_BY = 1;
                dp.UPDATE_DATE = DateTime.Now;
                var _dp = _datphong.update(dp);
                _idDP = _dp.IDDP;

                _datphongchitiet.deleteAll(_idDP);
                _datphongsanpham.deleteAll(_idDP);

                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    tb_DatPhong_CT dpct = new tb_DatPhong_CT();
                    dpct.IDDP = _dp.IDDP;
                    dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                    dpct.SONGAYO = Math.Max((dtNgayTra.Value - dtNgayDat.Value).Days, 1);
                    dpct.DONGIA = int.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                    dpct.THANHTIEN = dpct.DONGIA * dpct.SONGAYO;
                    dpct.NGAY = DateTime.Now;

                    var _dpct = _datphongchitiet.add(dpct);
                    _phong.updateStatus((int)dpct.IDPHONG, true);

                    for (int j = 0; j < gvSPDV.RowCount; j++)
                    {
                        int idPhongSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                        if (dpct.IDPHONG == idPhongSP)
                        {
                            tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham();
                            dpsp.IDDP = _dp.IDDP;
                            dpsp.IDDPCT = _dpct.IDDPCT;
                            dpsp.IDPHONG = idPhongSP;
                            dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                            dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                            dpsp.DONGIA = float.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                            dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                            _datphongsanpham.add(dpsp);
                        }
                    }
                }

                var removedRooms = originalRooms.Where(x => !currentRooms.Contains((int)x)).ToList();
                foreach (var roomId in removedRooms)
                {
                    _phong.updateStatus((int)roomId, false);
                }

                loadDP();
                loadSPDV();
                objMain.showRoom();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            _reset();
            tabDanhDanh.SelectedTabPage = pageDanhSach;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dtNgayTra.Value.Date <= dtNgayDat.Value.Date)
            {
                MessageBox.Show("Ngày trả phải sau ngày đặt ít nhất 1 ngày.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_them)
            {
                saveData();
                // Tính lại tiền
                double tongDV = 0, tongPhong = 0;
                int songayo = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
                if (gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue != null)
                    double.TryParse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString(), out tongDV);

                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    var donGia = gvDatPhong.GetRowCellValue(i, "DONGIA");
                    if (donGia != null)
                        tongPhong += Convert.ToDouble(donGia) * songayo;
                }
                _tongtien = tongPhong + tongDV;

                var dp = _datphong.getItem(_idDP);
                dp.SOTIEN = _tongtien;
                _datphong.update(dp);
                _datphong.updateStatus(_idDP);
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    var idPhong = gvDatPhong.GetRowCellValue(i, "IDPHONG");
                    if (idPhong != null)
                    {
                        _phong.updateStatus(int.Parse(idPhong.ToString()), false);
                    }
                }

                XuatReport("PHIEU_DATPHONG", "Phiếu đặt phòng chi tiết");
                cboTrangThai.SelectedValue = true;
                objMain.gControl.Gallery.Groups.Clear();
                objMain.showRoom();
            }
            //XuatReport("PHIEU_DATPHONG", "Phiếu đặt phòng chi tiết");
        }
        private void XuatReport(string _rpName, string _rpTitle)
        {
            if (_idDP != 0)
            {
                Form frm = new Form();
                CrystalReportViewer crv = new CrystalReportViewer();
                crv.ShowGroupTreeButton = false;
                crv.ShowParameterPanelButton = false;
                crv.ToolPanelView = ToolPanelViewType.None;
                TableLogOnInfo Thongtin;
                ReportDocument doc = new ReportDocument();
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + _rpName + ".rpt");
                Thongtin = doc.Database.Tables[0].LogOnInfo;
                Thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                Thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                Thongtin.ConnectionInfo.UserID = myFunctions._us;
                Thongtin.ConnectionInfo.Password = myFunctions._pw;
                doc.Database.Tables[0].ApplyLogOnInfo(Thongtin);
                try
                {
                    doc.SetParameterValue("@IDDP", _idDP.ToString());

                    crv.Dock = DockStyle.Fill;
                    crv.ReportSource = doc;
                    crv.Refresh();
                    frm.Controls.Add(crv);
                    frm.Text = _rpTitle;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnPrint.Visible = t;
        }

        void _enabled(bool t)
        {
            cboKhachHang.Enabled = t;
            btnAddNew.Enabled = t;
            dtNgayDat.Enabled = t;
            dtNgayTra.Enabled = t;
            cboTrangThai.Enabled = t;
            chkDoan.Enabled = t;
            spSoNguoi.Enabled = t;
            txtGhiChu.Enabled = t;
            gcPhong.Enabled = t;
            gcSanPham.Enabled = t;
            gcDatPhong.Enabled = t;
            gcSPDV.Enabled = t;
        }

        void _reset()
        {
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            spSoNguoi.Text = "1";
            chkDoan.Checked = true;
            cboTrangThai.SelectedValue = false;
            txtGhiChu.Text = "";
            txtThanhTien.Text = "0";
            _idPhong = 0;
            _tenPhong = null;
        }

        private void gvDatPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfor = hitInfo;
                DataRow row = view.GetDataRow(hitInfo.RowHandle);
                if (row != null)
                {
                    _idPhong = int.Parse(row["IDPHONG"].ToString());
                    _tenPhong = row["TENPHONG"].ToString();
                }
            }
        }

        private void gvDatPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfor != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfor.RowHandle);
                    if (row != null)
                    {
                        _tenPhong = row["TENPHONG"].ToString();
                        if (!string.IsNullOrEmpty(_tenPhong))
                        {
                            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                        }
                    }
                    downHitInfor = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gvPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfor != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfor.RowHandle);
                    if (row != null)
                    {
                        _tenPhong = row["TENPHONG"].ToString();
                        if (!string.IsNullOrEmpty(_tenPhong))
                        {
                            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                        }
                    }
                    downHitInfor = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gvPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfor = hitInfo;
                DataRow row = view.GetDataRow(hitInfo.RowHandle);
                if (row != null)
                {
                    _tenPhong = row["TENPHONG"].ToString();
                }
            }
        }

        private void gcPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                int removedRoomId = int.Parse(row["IDPHONG"].ToString());
                table.ImportRow(row);
                row.Delete();

                lstDPSP.RemoveAll(sp => sp.IDPHONG == removedRoomId);
                loadDPSP();

                // Kiểm tra xem _idPhong có còn hợp lệ trong danh sách phòng đã đặt hay không
                bool isIdPhongValid = false;
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    int idPhongInGrid = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                    if (_idPhong == idPhongInGrid)
                    {
                        isIdPhongValid = true;
                        break;
                    }
                }

                if (!isIdPhongValid)
                {
                    if (gvDatPhong.RowCount > 0)
                    {
                        _idPhong = int.Parse(gvDatPhong.GetRowCellValue(0, "IDPHONG").ToString());
                        _tenPhong = gvDatPhong.GetRowCellValue(0, "TENPHONG").ToString();
                    }
                    else
                    {
                        _idPhong = 0;
                        _tenPhong = null;
                    }
                }

                updateTongTien();
            }
        }

        private void gcPhong_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void gcDatPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                table.ImportRow(row);
                row.Delete();

                // Reset _idPhong để yêu cầu người dùng chọn lại phòng
                _idPhong = 0;
                _tenPhong = null;

                updateTongTien(); // Cập nhật tổng tiền
            }
        }

        private void gcDatPhong_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void gvPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhong.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                        SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                        Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                        BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvPhong); }));
                    }
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvPhong); }));
            }
        }

        private void gvPhong_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption;
            if (info.Column.Caption == string.Empty)
                caption = info.Column.ToString();

            info.GroupText = string.Format("{0}: {1} ({2} phòng trống)", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }

        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (gvDatPhong.RowCount == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng trước khi thêm sản phẩm/dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool isIdPhongValid = false;
            for (int i = 0; i < gvDatPhong.RowCount; i++)
            {
                int idPhongInGrid = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                if (_idPhong == idPhongInGrid)
                {
                    isIdPhongValid = true;
                    break;
                }
            }

            if (!isIdPhongValid || _idPhong == 0)
            {
                MessageBox.Show("Phòng hiện tại không hợp lệ. Vui lòng chọn lại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                OBJ_DPSP sp = new OBJ_DPSP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _tenPhong;
                sp.DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString());
                sp.SOLUONG = 1;
                sp.THANHTIEN = sp.DONGIA * sp.SOLUONG;

                bool found = false;
                foreach (var item in lstDPSP)
                {
                    if (item.IDSP == sp.IDSP && item.IDPHONG == sp.IDPHONG)
                    {
                        item.SOLUONG++;
                        item.THANHTIEN = item.SOLUONG * item.DONGIA;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    lstDPSP.Add(sp);
                }
            }
            loadDPSP();
            updateTongTien();
        }

        void loadDPSP()
        {
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SOLUONG")
            {
                int sl = int.Parse(e.Value.ToString());
                if (sl != 0)
                {
                    double gia = double.Parse(gvSPDV.GetRowCellValue(gvSPDV.FocusedRowHandle, "DONGIA").ToString());
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", sl * gia);
                }
                else
                {
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", 0);
                }
            }
            gvSPDV.UpdateTotalSummary();
            updateTongTien();
        }

        private void gvDatPhong_RowCountChanged(object sender, EventArgs e)
        {
            updateTongTien();
        }

        private void gvSanPham_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvSanPham.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { gvSanPham.IndicatorWidth = Math.Max(gvSanPham.IndicatorWidth, _Width); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvSanPham); }));
            }
        }

        private void gvDatPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDatPhong.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { gvDatPhong.IndicatorWidth = Math.Max(gvDatPhong.IndicatorWidth, _Width); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvDatPhong); }));
            }
        }

        private void gvSPDV_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvSPDV.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { gvSPDV.IndicatorWidth = Math.Max(gvSPDV.IndicatorWidth, _Width); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvSPDV); }));
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        public void setKhachHang(int idkh)
        {
            var _kh = _khachhang.getItem(idkh);
            cboKhachHang.SelectedValue = _kh.IDKH;
            cboKhachHang.Text = _kh.HOTEN;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                var dp = _datphong.getItem(_idDP);
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                spSoNguoi.Text = dp.SONGUOIO.ToString();
                cboTrangThai.SelectedValue = dp.STATUS;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");

                loadDP();
                loadSPDV();

                // Gán _idPhong cho phòng đầu tiên trong danh sách phòng đã đặt (nếu có)
                if (gvDatPhong.RowCount > 0)
                {
                    _idPhong = int.Parse(gvDatPhong.GetRowCellValue(0, "IDPHONG").ToString());
                    _tenPhong = gvDatPhong.GetRowCellValue(0, "TENPHONG").ToString();
                }
                else
                {
                    _idPhong = 0;
                    _tenPhong = null;
                }
            }
        }

        void loadDP()
        {
            _rowDatPhong = 0;
            gcDatPhong.DataSource = myFunctions.laydulieu(
                "SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG " +
                "FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C, tb_DatPhong_CT D " +
                "WHERE A.IDTANG = B.IDTANG AND A.IDLOAIPHONG = C.IDLOAIPHONG AND A.IDPHONG = D.IDPHONG AND D.IDDP = '" + _idDP + "'"
            );

            _rowDatPhong = gvDatPhong.RowCount;
        }

        void loadSPDV()
        {
            lstDPSP = _datphongsanpham.getAllByDatPhong(_idDP).Select(sp => new OBJ_DPSP
            {
                IDSP = sp.IDSP.Value,
                TENSP = _sanpham.getItem(sp.IDSP.Value)?.TENSP,
                IDPHONG = sp.IDPHONG.Value,
                TENPHONG = _phong.getItem(sp.IDPHONG.Value)?.TENPHONG,
                DONGIA = sp.DONGIA.Value,
                SOLUONG = sp.SOLUONG.Value,
                THANHTIEN = sp.THANHTIEN.Value
            }).ToList();
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtTuNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                var dp = _datphong.getItem(_idDP);
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                spSoNguoi.Text = dp.SONGUOIO.ToString();
                cboTrangThai.SelectedValue = dp.STATUS;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");

                loadDP();
                loadSPDV();

                // Gán _idPhong cho phòng đầu tiên trong danh sách phòng đã đặt (nếu có)
                if (gvDatPhong.RowCount > 0)
                {
                    _idPhong = int.Parse(gvDatPhong.GetRowCellValue(0, "IDPHONG").ToString());
                    _tenPhong = gvDatPhong.GetRowCellValue(0, "TENPHONG").ToString();
                }
                else
                {
                    _idPhong = 0;
                    _tenPhong = null;
                }

                tabDanhDanh.SelectedTabPage = pageChiTiet;
            }
        }

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDanhSach.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { gvDanhSach.IndicatorWidth = Math.Max(gvDanhSach.IndicatorWidth, _Width); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvDanhSach); }));
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

        void updateTongTien()
        {
            double tongDV = 0;
            double tongPhong = 0;
            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);

            // Tổng dịch vụ
            var dv = gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue;
            if (dv != null)
                double.TryParse(dv.ToString(), out tongDV);

            // Tổng tiền phòng (phải nhân số ngày ở)
            for (int i = 0; i < gvDatPhong.RowCount; i++)
            {
                var gia = gvDatPhong.GetRowCellValue(i, "DONGIA");
                if (gia != null)
                {
                    tongPhong += Convert.ToDouble(gia) * soNgayO;
                }
            }

            txtThanhTien.Text = (tongDV + tongPhong).ToString("N0");
        }

        private void gvDanhSach_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "STATUS")
            {
                bool trangThai = Convert.ToBoolean(gvDanhSach.GetRowCellValue(e.RowHandle, "STATUS"));
                if (trangThai)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else
                {
                    e.Appearance.BackColor = Color.MistyRose;
                }
            }
        }
    }
}