using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DATPHONG_CHITIET
    {
        Entities db;
        public DATPHONG_CHITIET()
        {
            db = Entities.CreateEntities();
        }

        public List<tb_DatPhong_CT> getAll()
        {
            return db.tb_DatPhong_CT.ToList();
        }


        // Lấy 1 dòng theo IDDPCT
        public tb_DatPhong_CT getItem(int id)
        {
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == id);
        }

        // Lấy tất cả chi tiết theo ID đơn đặt phòng
        public List<tb_DatPhong_CT> getAllByDatPhong(int id)
        {
            return db.tb_DatPhong_CT.Where(x => x.IDDP == id).ToList();
        }

        // Thêm mới
        public tb_DatPhong_CT add(tb_DatPhong_CT dpct)
        {
            try
            {
                db.tb_DatPhong_CT.Add(dpct);
                db.SaveChanges();
                return dpct;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm đơn đặt phòng: " + ex.Message);
            }
        }

        public void update(tb_DatPhong_CT dpct)
        {
            tb_DatPhong_CT _dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == dpct.IDDPCT);
            _dpct.IDDP = dpct.IDDP;
            _dpct.IDPHONG = dpct.IDPHONG;
            _dpct.IDDPCT = dpct.IDDPCT;
            _dpct.NGAY = dpct.NGAY;
            _dpct.DONGIA = dpct.DONGIA;
            _dpct.SONGAYO = dpct.SONGAYO;
            _dpct.THANHTIEN = dpct.THANHTIEN;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật đơn đặt phòng: " + ex.Message);
            }
        }
        public void delete(int idDPCT)
        {
            tb_DatPhong_CT _dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT ==  idDPCT);
            try
            {
                db.tb_DatPhong_CT.Remove(_dpct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật đơn đặt phòng: " + ex.Message);
            }
        }
        public void deleteAll(int idDP)
        {
            List<tb_DatPhong_CT> lst = db.tb_DatPhong_CT.Where(x => x.IDDP == idDP).ToList();
            try
            {
                db.tb_DatPhong_CT.RemoveRange(lst);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }

        }


    }
}
