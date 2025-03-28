using BusinessLayer;
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

        List<OBJ_DPSP> lstDPSP;
        SYS_PARAM _param;

        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongchitiet;
        DATPHONG_SANPHAM _datphongsanpham;
        KHACHHANG _khachhang;
        SPDV _sanpham;
        PHONG _phong;
        GridHitInfo downHitInfor = null; // biến lưu cái dòng mình nhấn chuột


        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _datphongchitiet = new DATPHONG_CHITIET();
            _datphongsanpham = new DATPHONG_SANPHAM();
            _khachhang = new KHACHHANG();
            _sanpham = new SPDV();
            _param = new SYS_PARAM();
            _phong = new PHONG();

            lstDPSP = new List<OBJ_DPSP>();
            // chỉnh cái khung tg
            dtTuNgay.Value = myFunctions.GetFirstDayInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dtDenNgay.Value = DateTime.Now;

            var _pr = _param.getParam();
            _macty = _pr.MACTY;
            _madvi = _pr.MADVI;

            loadKH();
            loadSP();

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.DisplayMember = "_display";

            showHideControl(true);
            _enabled(false);

            gvPhong.ExpandAllGroups();
            tabDanhDanh.SelectedTabPage = pageDanhSach;
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            tabDanhDanh.SelectedTabPage = pageChiTiet;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
            tabDanhDanh.SelectedTabPage = pageChiTiet;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }

        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            saveData();
            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();
            _them = false;
            _enabled(false);
            showHideControl(true);

        }
        void saveData()
        {
            if (_them) // Nếu đang trong chế độ thêm mới
            {
                // Tạo đối tượng đặt phòng
                tb_DatPhong dp = new tb_DatPhong();
                tb_DatPhong_CT dpct;                  // Biến lưu chi tiết đặt phòng (theo phòng)
                tb_DatPhong_SanPham dpsp;            // Biến lưu sản phẩm dịch vụ sử dụng

                // Gán dữ liệu vào đối tượng đặt phòng
                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.EditValue.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.THEODOAN = chkDoan.Checked;
                dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.DISABLED = false; 
                dp.IDUSER = 1;              // Gán tạm user ID, có thể cập nhật theo user login thực tế
                dp.MACTY = _macty;          // Mã công ty
                dp.MADVI = _madvi;          // Mã đơn vị

                // Thêm vào database và nhận lại đối tượng (có ID mới tạo)
                var _dp = _datphong.add(dp);
                _idDP = _dp.IDDP;
                // Duyệt qua từng phòng đã chọn trong gridview "gvDatPhong"
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    dpct = new tb_DatPhong_CT();
                    dpct.IDDP = _dp.IDDP; // Gán ID đơn đặt phòng vừa thêm
                    dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());

                    // Tính số ngày ở giữa ngày trả và ngày đặt
                    dpct.SONGAYO = (dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days;
                    dpct.DONGIA = int.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                    dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;

                    // Thêm chi tiết phòng vào database
                    var _dpct =  _datphongchitiet.add(dpct);

                    // Cập nhật trạng thái phòng thành "đã đặt"
                    _phong.updateStatus(int.Parse(dpct.IDPHONG.ToString()), true);

                    // ====== XỬ LÝ SẢN PHẨM DỊCH VỤ THEO PHÒNG ======

                    if (gvSPDV.RowCount > 0) // Nếu có dịch vụ nào được chọn
                    { 
                        // Duyệt qua từng dòng sản phẩm dịch vụ trong grid gvSPDV
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            // Nếu sản phẩm này thuộc đúng phòng đang xét
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

                                // Thêm sản phẩm vào database
                                _datphongsanpham.add(dpsp);
                            }
                            else
                            {
                                // ❗ Trường hợp này hơi thừa: Nếu phòng không trùng, bạn vẫn thêm sản phẩm rỗng (không có IDSP, SOLUONG,...)
                                // => Có thể bỏ nếu không cần tạo record trống
                                dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDPHONG = dpct.IDPHONG;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                _datphongsanpham.add(dpsp);
                            }
                        }
                    }
                    else
                    {
                        // Nếu KHÔNG có dịch vụ nào được chọn, vẫn thêm 1 dòng "rỗng" cho phòng vào bảng sản phẩm
                        // Có thể không cần bước này nếu database không yêu cầu
                        dpsp = new tb_DatPhong_SanPham();
                        dpsp.IDDP = _dp.IDDP;
                        dpsp.IDPHONG = dpct.IDPHONG;
                        dpsp.IDDPCT = _dpct.IDDPCT;
                        _datphongsanpham.add(dpsp);


                    }
                }
            }

            else // update
            {
                tb_DatPhong dp = _datphong.getItem(_idDP);
                tb_DatPhong_CT dpct;                  
                tb_DatPhong_SanPham dpsp;

                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.EditValue.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.IDUSER = 1;             
               
                var _dp = _datphong.update(dp);

                _idDP = _dp.IDDP;
                _datphongchitiet.deleteAll(dp.IDDP);
                _datphongsanpham.deleteAll(dp.IDDP);

                // sau khi xóa hết thì thực hiện thêm lại
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    dpct = new tb_DatPhong_CT();
                    dpct.IDDP = _dp.IDDP;
                    dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                    dpct.SONGAYO = (dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days;
                    dpct.DONGIA = int.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                    dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;

                   
                    var _dpct = _datphongchitiet.add(dpct);
                    _phong.updateStatus(int.Parse(dpct.IDPHONG.ToString()), true);
                    if (gvSPDV.RowCount > 0) // Nếu có dịch vụ nào được chọn
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
                            else
                            {
                                dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDPHONG = dpct.IDPHONG;
                                _datphongsanpham.add(dpsp);
                            }
                        }
                    }
                    else
                    {
                        dpsp = new tb_DatPhong_SanPham();
                        dpsp.IDDP = _dp.IDDP;
                        dpsp.IDPHONG = dpct.IDPHONG;
                        _datphongsanpham.add(dpsp);


                    }
                }
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
        }

        private void gvDatPhong_MouseDown(object sender, MouseEventArgs e)
        {
            if (gvDatPhong.GetFocusedRowCellValue("IDPHONG") != null)
            {
                _idPhong = int.Parse(gvDatPhong.GetFocusedRowCellValue("IDPHONG").ToString());
                _tenPhong = gvDatPhong.GetFocusedRowCellValue("TENPHONG").ToString();
            }

            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfor = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfor.RowHandle >= 0)
            {
                downHitInfor = hitInfor;
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
                    view.GridControl.DoDragDrop(row, DragDropEffects.Move);
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
                Rectangle dragRect = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2,
                                                             downHitInfor.HitPoint.Y - dragSize.Height / 2),
                                                   dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfor.RowHandle);
                    view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                    downHitInfor = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gvPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfor = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfor.RowHandle >= 0)
            {
                downHitInfor = hitInfor;
            }
        }

        private void gcPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                table.ImportRow(row);
                row.Delete();
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

        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void gvPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhong.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần

                        SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                        Int32 _Width = Convert.ToInt32(_Size.Width) + 20;

                        BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvPhong); })); // Tăng kích thước nếu Text vượt quá
                    }
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần
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
            if (_idPhong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                // Thực hiện logic khi có ID sản phẩm được chọn
                OBJ_DPSP sp = new OBJ_DPSP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _tenPhong;
                sp.DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString());
                sp.SOLUONG = 1;
                sp.THANHTIEN = sp.DONGIA * sp.SOLUONG;

                foreach (var item in lstDPSP)
                {
                    // lần thứ 2 double click 
                    if (item.IDSP == sp.IDSP && item.IDPHONG == sp.IDPHONG)
                    {
                        item.SOLUONG = item.SOLUONG + 1;
                        item.THANHTIEN = item.SOLUONG * item.DONGIA;
                        loadDPSP();
                        return;
                    }
                }

                lstDPSP.Add(sp);
            }
            loadDPSP();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + double.Parse(gvDatPhong.Columns["DONGIA"].SummaryItem.SummaryValue.ToString())).ToString("N0");

        }

        void loadDPSP()
        {
            List<OBJ_DPSP> lsDP = new List<OBJ_DPSP>();
            foreach (var item in lstDPSP)
            {
                lsDP.Add(item);
            }
            gcSPDV.DataSource = lsDP;
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
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + double.Parse(gvDatPhong.Columns["DONGIA"].SummaryItem.SummaryValue.ToString())).ToString("N0");
        }

        private void gvDatPhong_RowCountChanged(object sender, EventArgs e)
        {
            double t = 0;
            if (gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue == null)
                t = 0;
            else
                t = double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString());

            txtThanhTien.Text = (double.Parse(gvDatPhong.Columns["DONGIA"].SummaryItem.SummaryValue.ToString()) + t).ToString("N0");

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

                        SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                        Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                        BeginInvoke(new MethodInvoker(delegate { gvSanPham.IndicatorWidth = Math.Max(gvSanPham.IndicatorWidth, _Width); }));
                    }
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
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

                        SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                        Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                        BeginInvoke(new MethodInvoker(delegate { gvDatPhong.IndicatorWidth = Math.Max(gvDatPhong.IndicatorWidth, _Width); }));
                    }
                }
            }
            else
            {
                e.Info.ImageIndex = -1;

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

                        SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                        Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                        BeginInvoke(new MethodInvoker(delegate { gvSPDV.IndicatorWidth = Math.Max(gvSPDV.IndicatorWidth, _Width); }));
                    }
                }
            }
            else
            {
                e.Info.ImageIndex = -1;

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
            _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());

        }
    }
}