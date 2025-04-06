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
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace THUEPHONG
{
    public partial class frmDatPhongDon : DevExpress.XtraEditors.XtraForm
    {
        public frmDatPhongDon()
        {
            InitializeComponent();
        }
        public bool _them;
        public int _idPhong; // idPhong lấy từ frmMain nên cần phải public
        int _idDP = 0;
        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongct;
        DATPHONG_SANPHAM _datphongsp;
        OBJ_PHONG _phongHienTai;
        PHONG _phong;
        SPDV _sanpham;
        SYS_PARAM _param;
        string _macty;
        string _madvi;
        List<OBJ_DPSP> lstDPSP;
        double _tongtien = 0;

        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        private void frmDatPhongDon_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _datphongct = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SANPHAM();
            _phong = new PHONG();
            _sanpham = new SPDV();
            _param = new SYS_PARAM();
            lstDPSP = new List<OBJ_DPSP>();
            _phongHienTai = _phong.getItemFull(_idPhong);
            lblPhong.Text = _phongHienTai.TENPHONG + " - Đơn giá: " + _phongHienTai.DONGIA.GetValueOrDefault().ToString("n0") + " VNĐ";

            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.DisplayMember = "_display";

            spSoNguoi.Text = "1";

            var _pr = _param.getParam();
            _macty = _pr.MACTY;
            _madvi = _pr.MADVI;

            loadKH();
            loadSP();

            var dpct = _datphongct.getIDDPByPhong(_idPhong);
            if (!_them && dpct != null)
            {
                _idDP = (int) dpct.IDDP;
                _tongtien = TinhTongTien();
                txtThanhTien.Text = _tongtien.ToString("N0");
                var dp = _datphong.getItem(_idDP);
                searchKH.EditValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                
                if (dp.NGAYDATPHONG.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                    dtNgayTra.Value = dp.NGAYDATPHONG.Value.AddDays(1);
                else
                    dtNgayTra.Value = DateTime.Now;
                cboTrangThai.SelectedValue = dp.STATUS;
                spSoNguoi.Text = dp.SONGUOIO.ToString();
                txtGhiChu.Text = dp.GHICHU.ToString();
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");

            }
            loadSPDV();

            dtNgayDat.ValueChanged += dtNgay_ValueChanged;
            dtNgayTra.ValueChanged += dtNgay_ValueChanged;

            TongTienCapNhat(); // Gọi lại hàm tính tổng tiền sau khi load xong

        }
        void TongTienCapNhat()
        {
            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
            double tongDV = 0;
            double.TryParse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString(), out tongDV);

            _tongtien = tongDV + _phongHienTai.DONGIA.Value * soNgayO;
            txtThanhTien.Text = _tongtien.ToString("N0");
        }

        private void dtNgay_ValueChanged(object sender, EventArgs e)
        {
            // Tính và cập nhật lại tổng tiền
            double tongDV = 0;
            double.TryParse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString(), out tongDV);
            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
            double tongTien = tongDV + _phongHienTai.DONGIA.Value * soNgayO;

            txtThanhTien.Text = tongTien.ToString("N0");
        }
        void loadSPDV()
        {
            gcSPDV.DataSource = _datphongsp.getAllByDatPhong(_idDP);
            lstDPSP = _datphongsp.getAllByDatPhong(_idDP);
        }
        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }
        public void loadKH()
        {
            KHACHHANG _khachhang = new KHACHHANG();
            searchKH.Properties.DataSource = _khachhang.getAll();
            searchKH.Properties.DisplayMember = "HOTEN";
            searchKH.Properties.ValueMember = "IDKH";
        }

        public void setKH(int idKH)
        {
            searchKH.EditValue = idKH;
        }
        private double TinhTongTien()
        {
            double tongDV = 0;
            double.TryParse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString(), out tongDV);
            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
            return tongDV + _phongHienTai.DONGIA.Value * soNgayO;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (searchKH.EditValue == null || searchKH.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveData();
            _tongtien = TinhTongTien();
            var dp = _datphong.getItem(_idDP);
            dp.SOTIEN = _tongtien;
            _datphong.update(dp);
            // Sau khi lưu thì chuyển trạng thái về chế độ sửa
            _them = false;
            MessageBox.Show("Lưu thành công! Bạn có thể tiếp tục in hoặc đóng form.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng lưu phiếu đặt phòng trước khi in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_them)
            {
                saveData();
                _tongtien = TinhTongTien();
                var dp = _datphong.getItem(_idDP);
                dp.SOTIEN = _tongtien;
                _datphong.update(dp);
                _datphong.updateStatus(_idDP);
                _phong.updateStatus(_idPhong, false);
                XuatReport("PHIEU_DATPHONG_DON", "Phiếu đặt phòng chi tiết");
                cboTrangThai.SelectedValue = true;
                objMain.gControl.Gallery.Groups.Clear();
                objMain.showRoom();
            }

            //// sau khi in xong thì mới set OK để frmMain cập nhật
            //this.DialogResult = DialogResult.OK;
            
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
            this.DialogResult = DialogResult.OK;  // để frmMain reload màu
            this.Close();
        }
        public void getKH(int idKH)
        {
            KHACHHANG _khachhang = new KHACHHANG();
            var kh = _khachhang.getItem(idKH);
            searchKH.EditValue = kh.IDKH;
            searchKH.Text = kh.HOTEN;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.kh_dp = "datphongdon";
            frm.ShowDialog();


        }

        private void gvSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idPhong == 0)
            {
                MessageBox.Show("Phòng hiện tại không hợp lệ. Vui lòng chọn lại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                OBJ_DPSP sp = new OBJ_DPSP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _phongHienTai.TENPHONG;
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
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA.Value * Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1)).ToString("N0");

        }

        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {

        }
        void loadDPSP()
        {
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA.Value * Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1)).ToString("N0");



        }

        private void gvSPDV_HiddenEditor(object sender, EventArgs e)
        {
            gvSPDV.UpdateCurrentRow();
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
                dp.THEODOAN = false;
                dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.DISABLED = false;
                dp.IDUSER = 1;
                dp.MACTY = _macty;
                dp.MADVI = _madvi;
                dp.CREATED_DATE = DateTime.Now;

                var _dp = _datphong.add(dp);
                _idDP = _dp.IDDP;

                dpct = new tb_DatPhong_CT();
                dpct.IDDP = _dp.IDDP;
                dpct.IDPHONG = _idPhong;
                dpct.SONGAYO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1); // Ít nhất 1 ngày
                dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;
                dpct.NGAY = DateTime.Now;
                var _dpct = _datphongct.add(dpct);

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
                            _datphongsp.add(dpsp);
                        }
                    }
                }

            }
            else // Chế độ chỉnh sửa
            {
                tb_DatPhong dp = _datphong.getItem(_idDP);
                tb_DatPhong_CT dpct;
                tb_DatPhong_SanPham dpsp;

                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.Value.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                dp.SOTIEN = double.TryParse(txtThanhTien.Text, out double total) ? total : 0;
                dp.GHICHU = txtGhiChu.Text;
                dp.IDUSER = 1;
                dp.UPDATE_BY = 1;
                dp.UPDATE_DATE = DateTime.Now;
                var _dp = _datphong.update(dp);
                _idDP = _dp.IDDP;

                _datphongct.deleteAll(_idDP);
                _datphongsp.deleteAll(_idDP);

                dpct = new tb_DatPhong_CT();

                dpct.IDDP = _dp.IDDP;
                dpct.IDPHONG = _idPhong;
                dpct.SONGAYO = Math.Max((dtNgayTra.Value - dtNgayDat.Value).Days, 1);
                dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                dpct.THANHTIEN = dpct.DONGIA * dpct.SONGAYO;
                dpct.NGAY = DateTime.Now;

                var _dpct = _datphongct.add(dpct);
                _phong.updateStatus((int)dpct.IDPHONG, true);

                for (int j = 0; j < gvSPDV.RowCount; j++)
                {
                    int idPhongSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                    if (dpct.IDPHONG == idPhongSP)
                    {
                        dpsp = new tb_DatPhong_SanPham();
                        dpsp.IDDP = _dp.IDDP;
                        dpsp.IDDPCT = _dpct.IDDPCT;
                        dpsp.IDPHONG = idPhongSP;
                        dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                        dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                        dpsp.DONGIA = float.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                        dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                        _datphongsp.add(dpsp);
                    }
                }
            }
        }
    }

}