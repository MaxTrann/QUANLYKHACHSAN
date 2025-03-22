using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class KHACHHANG
    {
        Entities db;
        public KHACHHANG()
        {
            db = Entities.CreateEntities();
        }
        // Lấy tất cả khách hàng
        public List<tb_KhachHang> getAll()
        {
            return db.tb_KhachHang.ToList();
        }
        // Lấy khách hàng thông qua id
        public tb_KhachHang getItem(int makh)
        {
            return db.tb_KhachHang.FirstOrDefault(x => x.IDKH == makh);
        }

        // Thêm khách hàng
        public void add(tb_KhachHang kh)
        {
            try
            {
                db.tb_KhachHang.Add(kh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        // Update khách hàng
        public void update(tb_KhachHang kh)
        {
            tb_KhachHang _kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH ==  kh.IDKH);
            _kh.HOTEN = kh.HOTEN;
            _kh.CCCD = kh.CCCD;
            _kh.SDT = kh.SDT;
            _kh.EMAIL = kh.EMAIL;
            _kh.DIACHI = kh.DIACHI;
            _kh.GIOITINH = kh.GIOITINH;
            _kh.DISABLED = kh.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        public void delete(int makh)
        {
            tb_KhachHang _kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == makh);
            _kh.DISABLED = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

    }
}
