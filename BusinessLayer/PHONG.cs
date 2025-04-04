using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace BusinessLayer
{
   
    public class PHONG
    {
        Entities db;
        public PHONG()
        {
            db = Entities.CreateEntities();
        }
        // Lấy toàn bộ danh sách phòng
        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        // Lấy danh sách phòng theo ID tầng
        public List<tb_Phong> getByTang(int idTang)
        {
            return db.tb_Phong.Where(x => x.IDTANG == idTang).ToList();
        }

        // Lấy thông tin phòng theo ID
        public tb_Phong getItem(int idPhong)
        {
            return db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idPhong);
        }

        // Thêm phòng mới
        public void add(tb_Phong phong)
        {
            try
            {
                db.tb_Phong.Add(phong);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
        // Cập nhật phòng
        public void update(tb_Phong phong)
        {
            tb_Phong _phong = db.tb_Phong.FirstOrDefault(x => x.IDPHONG==phong.IDPHONG);
            _phong.TENPHONG = phong.TENPHONG;
            _phong.TRANGTHAI = phong.TRANGTHAI;
            _phong.DISABLED = phong.DISABLED;

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        // Xóa phòng
        public void delete(int idPhong)
        {
            tb_Phong _phong = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idPhong);
            _phong.DISABLED = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
        // Cập nhật trạng thái phòng
        public void updateStatus(int idPhong, bool status)
        {
            tb_Phong phong = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idPhong);
            if (phong != null)
            {
                phong.TRANGTHAI = status;

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Có lỗi xảy ra khi cập nhật trạng thái phòng: " + ex.Message);
                }
            }
        }

        // Trả về kiểu OBJ_PHONG thay vì tb_Phong
        public OBJ_PHONG getItemFull(int idPhong)
        {
            var result = (from p in db.tb_Phong
                          join lp in db.tb_LoaiPhong on p.IDLOAIPHONG equals lp.IDLOAIPHONG
                          where p.IDPHONG == idPhong
                          select new OBJ_PHONG
                          {
                              IDPHONG = p.IDPHONG,
                              TENPHONG = p.TENPHONG,
                              TRANGTHAI = p.TRANGTHAI,
                              IDTANG = p.IDTANG,
                              IDLOAIPHONG = p.IDLOAIPHONG,
                              DISABLED = p.DISABLED,
                              DONGIA = lp.DONGIA // <-- lấy từ bảng LoaiPhong
                          }).FirstOrDefault();

            return result;
        }


    }
}
