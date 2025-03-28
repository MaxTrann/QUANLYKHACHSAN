using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class DATPHONG_SANPHAM
    {
        Entities db;

        public DATPHONG_SANPHAM()
        {
            db = Entities.CreateEntities();
        }

        // Lấy danh sách sản phẩm theo ID đơn đặt phòng
        public List<tb_DatPhong_SanPham> getByIDDP(int idDP)
        {
            return db.tb_DatPhong_SanPham.Where(x => x.IDDP == idDP).ToList();
        }

        // Thêm sản phẩm vào đơn đặt phòng
        public void add(tb_DatPhong_SanPham dpsp)
        {
            try
            {
                db.tb_DatPhong_SanPham.Add(dpsp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm sản phẩm vào đơn đặt phòng: " + ex.Message);
            }
        }

        // Xóa tất cả sản phẩm theo ID đơn đặt phòng (nếu muốn xử lý cập nhật lại)
        public void deleteAll(int idDP)
        {
            List<tb_DatPhong_SanPham> lst = db.tb_DatPhong_SanPham.Where(x => x.IDDP == idDP).ToList();
            try
            {
                db.tb_DatPhong_SanPham.RemoveRange(lst);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa sản phẩm vào đơn đặt phòng: " + ex.Message);
            }
            
        }
       

    }
}
