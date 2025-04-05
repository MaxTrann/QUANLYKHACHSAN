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
    public partial class frmChuyenPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmChuyenPhong()
        {
            InitializeComponent();
        }
        public int _idPhong;
        PHONG _phong;
        DATPHONG_CHITIET _datphongct;
        DATPHONG_SANPHAM _datphongsp;
        DATPHONG _datphong;
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        private void frmChuyenPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _datphongct = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SANPHAM();
            _datphong = new DATPHONG();

            var p = _phong.getItemFull(_idPhong);
            lblPhong.Text = p.TENPHONG + " - Đơn giá: " + p.DONGIA.GetValueOrDefault().ToString("N0");
            loadPhongTrong();
        }
        void loadPhongTrong()
        {
            searchPhong.Properties.DataSource = _phong.getPhongTrongFull();
            searchPhong.Properties.ValueMember = "IDPHONG";
            searchPhong.Properties.DisplayMember = "TENPHONG";
        }

        private void btnChuyenPhong_Click(object sender, EventArgs e)
        {
            if (searchPhong.EditValue == null || searchPhong.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn phòng muốn chuyển đến.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int tongtien1 = 0; // update lại tổng tiền khi chuyển phòng
            int tongtien2 = 0;
            var phonghientai = _datphongct.getIDDPByPhong(_idPhong);
            var phongchuyenden = _phong.getItemFull(int.Parse(searchPhong.EditValue.ToString()));
            // Update đặt phòng sản phẩm
            List<tb_DatPhong_SanPham> lstDPSP = _datphongsp.getAllByPhong((int)phonghientai.IDDP, phonghientai.IDDPCT);
            foreach(var item in lstDPSP)
            {
                item.IDPHONG = int.Parse(searchPhong.EditValue.ToString());
                tongtien1 = tongtien1 +int.Parse(item.DONGIA.ToString()) * int.Parse(item.SOLUONG.ToString());
                _datphongsp.update(item);
            }
            // Update đặt phòng chi tiết
            var dpct = _datphongct.getItem((int)phonghientai.IDDP, _idPhong);
            dpct.IDPHONG = phongchuyenden.IDPHONG;
            dpct.DONGIA = int.Parse(phongchuyenden.DONGIA.ToString());
            dpct.THANHTIEN = dpct.SONGAYO * int.Parse(phongchuyenden.DONGIA.ToString());
            tongtien2 = int.Parse(dpct.THANHTIEN.ToString());
            _datphongct.update(dpct);
            // Cập nhật lại trạng thái phòng
            _phong.updateStatus(_idPhong, false);
            _phong.updateStatus(phongchuyenden.IDPHONG, true);

            var dp = _datphong.getItem((int)phonghientai.IDDP);
            dp.SOTIEN = tongtien1 + tongtien2;
            _datphong.update(dp);

            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();
            this.Close();

        }
    }
}