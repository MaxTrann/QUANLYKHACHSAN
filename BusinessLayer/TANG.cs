using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TANG
    {
        Entities db;

        public TANG()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_Tang> getAll() // Lấy toàn bộ danh sách
        {
            return db.tb_Tang.ToList();
        }

        public tb_Tang getItem(int id) // Lấy thông tin tầng theo ID
        {
            return db.tb_Tang.FirstOrDefault(x => x.IDTANG == id);
        }

        // thêm tầng mới
        public void add(tb_Tang tang)
        {
            try
            {
                db.tb_Tang.Add(tang);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        // Sửa thông tin tầng
        public void update(tb_Tang tang)
        {
            var _tang = db.tb_Tang.FirstOrDefault(x => x.IDTANG ==  tang.IDTANG);
            _tang.TENTANG = tang.TENTANG;
            _tang.DISABLED = tang.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        // Xóa tầng
        public void delete(int tang)
        {
            tb_Tang _tang = db.tb_Tang.FirstOrDefault(x => x.IDTANG == tang);
            _tang.DISABLED = true;
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
