using BusinessLayer;
using DataLayer;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
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
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        TANG _tang;
        PHONG _phong;
        FUNC _func;
        GalleryItem item = null;
        private void frmMain_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
            _phong = new PHONG();
            _func = new FUNC();
            
            leftMenu();
            showRoom();
        }
        void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach(var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.DESCRIPTION);
                navGroup.Tag = _pr.FUNC_CODE;
                navGroup.Name = _pr.FUNC_CODE;
                navGroup.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(navGroup);

                var _lsChile = _func.getChild(_pr.FUNC_CODE);
                foreach (var _ch in _lsChile)
                {
                    NavBarItem navItem = new NavBarItem(_ch.DESCRIPTION);
                    navItem.Tag = _ch.FUNC_CODE;
                    navItem.Name = _ch.FUNC_CODE;
                    navItem.ImageOptions.SmallImageIndex = 0;
                    navGroup.ItemLinks.Add(navItem);
                }
                navMain.Groups[navGroup.Name].Expanded = true;
            }
            
        }
        public void showRoom()
        {
            _tang = new TANG();
            _phong = new PHONG();

            gControl.Gallery.Groups.Clear(); // Xóa dữ liệu cũ trước khi nạp mới

            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            gControl.Gallery.ImageSize = new Size(64, 64);
            gControl.Gallery.ShowItemText = true;
            gControl.Gallery.ShowGroupCaption = true;
            foreach (var t in lsTang)
            {
                var galleryItem = new GalleryItemGroup();
                galleryItem.Caption = t.TENTANG;
                galleryItem.CaptionAlignment = GalleryItemGroupCaptionAlignment.Stretch;

                List<tb_Phong> lsPhong = _phong.getByTang(t.IDTANG);
                foreach (var p in lsPhong)
                {
                    var gc_item = new GalleryItem();
                    gc_item.Caption = p.TENPHONG;
                    gc_item.Value = p.IDPHONG;
                    if (p.TRANGTHAI == false) // chưa có người đặt thì hiện nhà màu xanh
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[0];
                    }
                    else // có người đặt thì hiện nhà màu đỏ
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[1];
                    }
                    galleryItem.Items.Add(gc_item);
                }
                gControl.Gallery.Groups.Add(galleryItem);
            }
            
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();
            switch (func_code) 
            {
                case "CONGTY":
                    {
                        frmCongTy frm = new frmCongTy();
                        frm.ShowDialog();
                        break;
                    }
                case "DONVI":
                    {
                        frmDonVi frm = new frmDonVi();
                        frm.ShowDialog();
                        break;
                    }
                case "TANG":
                    {
                        frmQuanLyTang frm = new frmQuanLyTang();
                        frm.ShowDialog();
                        break;
                    }
                case "LOAIPHONG":
                    {
                        frmLoaiPhong frm = new frmLoaiPhong();
                        frm.ShowDialog();
                        break;
                    }
                case "KHACHHANG":
                    {
                        frmKhachHang frm = new frmKhachHang();
                        frm.ShowDialog();
                        break;
                    }
                case "PHONG":
                    {
                        frmPhong frm = new frmPhong();
                        // Đăng ký sự kiện để cập nhật danh sách phòng sau khi frmPhong đóng
                        frm.OnDataUpdated += () => showRoom();
                        frm.FormClosed += (s, args) => showRoom(); // Thêm sự kiện FormClosed để làm mới khi đóng form
                        frm.ShowDialog();
                        // Sau khi frmPhong đóng, gọi lại showRoom để chắc chắn cập nhật
                        showRoom();
                        break;
                    }
                case "SANPHAM":
                    {
                        frmSPDV frm = new frmSPDV();
                        frm.ShowDialog();
                        break;
                    }
                case "THIETBI":
                    {
                        frmThietBi frm = new frmThietBi();
                        frm.ShowDialog(); break;
                    }
                case "PHONG_THIETBI":
                    {
                        frmPhong_ThietBi frm = new frmPhong_ThietBi();
                        frm.ShowDialog(); break;
                    }
                case "DATPHONG":
                    {
                        frmDatPhong frm = new frmDatPhong();
                        frm.ShowDialog(); break;
                    }

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {

        }

        private void popupMenu1_Popup(object sender, EventArgs e)
        {
            Point point = gControl.PointToClient(Control.MousePosition);
            RibbonHitInfo hitInfo = gControl.CalcHitInfo(point);
            if (hitInfo.InGalleryItem || hitInfo.HitTest == RibbonHitTest.GalleryImage)
                item = hitInfo.GalleryItem;            
        }

        private void btnDatPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                showRoom(); // phương thức reload lại danh sách phòng, đổi màu theo trạng thái
            }
        }

        private void btnChuyenPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.ShowDialog();
        }

        private void btnSPDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.ShowDialog();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.ShowDialog();
        }
    }
}