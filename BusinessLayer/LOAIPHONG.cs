using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LOAIPHONG
    {
        Entities db;
        public LOAIPHONG()
        {
            db = Entities.CreateEntities();
        }

        // lấy tất cả loại phòng
        public List<tb_LoaiPhong> getAll()
        {
            return db.tb_LoaiPhong.ToList();
        }

        // lấy thông tin loại phòng theo ID
        public tb_LoaiPhong getItem(int id)
        {
            return db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == id);
        }

        

        // thêm loại phòng 
        public void add(tb_LoaiPhong loai)
        {
            if (db.tb_LoaiPhong.Any(x => x.TENLOAIPHONG == loai.TENLOAIPHONG))
            {
                {
                    throw new Exception("Loại phòng này đã tồn tại.");
                }
            }

            try
            {
                db.tb_LoaiPhong.Add(loai);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }

        }

        // Update loại phòng
        public void update(tb_LoaiPhong lp)
        {
            tb_LoaiPhong _lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG==lp.IDLOAIPHONG);
            _lp.TENLOAIPHONG = lp.TENLOAIPHONG;
            _lp.DONGIA = lp.DONGIA;
            _lp.SONGUOI = lp.SONGUOI;
            _lp.SOGIUONG = lp.SOGIUONG;
            _lp.DISABLED = lp.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
        // Xóa loại phòng
        public void delete(int id)
        {
            tb_LoaiPhong _lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == id);
            _lp.DISABLED = true;
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
