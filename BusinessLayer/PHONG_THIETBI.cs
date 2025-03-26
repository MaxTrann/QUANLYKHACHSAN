using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public  class PHONG_THIETBI
    {
        Entities db;

        public PHONG_THIETBI()
        {
            db = Entities.CreateEntities();
        }

        // Lấy toàn bộ danh sách
        public List<tb_Phong_ThietBi> getAll()
        {
            return db.tb_Phong_ThietBi.ToList();
        }

        // Lấy 1 dòng theo (IDPHONG, IDTHIETBI)
        public tb_Phong_ThietBi getItem(int idPhong, int idTB)
        {
            return db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == idPhong && x.IDTB == idTB);
        }

        // Lấy danh sách theo IDPHONG
        public List<tb_Phong_ThietBi> getByPhong(int idPhong)
        {
            return db.tb_Phong_ThietBi.Where(x => x.IDPHONG == idPhong).ToList();
        }

        // Lấy danh sách theo IDTB
        public List<tb_Phong_ThietBi> getByThietBi(int idTB)
        {
            return db.tb_Phong_ThietBi.Where(x => x.IDTB == idTB).ToList();
        }

        public void add(tb_Phong_ThietBi ptb)
        {
            try
            {
                db.tb_Phong_ThietBi.Add(ptb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        public void update(tb_Phong_ThietBi ptb)
        {
            tb_Phong_ThietBi _ptb = db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == ptb.IDPHONG && x.IDTB == ptb.IDTB);
            _ptb.SOLUONG = ptb.SOLUONG;
            
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        public void delete(int idPhong, int idTB)
        {
            tb_Phong_ThietBi _ptb = db.tb_Phong_ThietBi.FirstOrDefault(x => x.IDPHONG == idPhong && x.IDTB == idTB);
            try
            {
                db.tb_Phong_ThietBi.Remove(_ptb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
    }
}
