using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SPDV
    {
        Entities db;
        public SPDV()
        {
            db = Entities.CreateEntities();
        }

        public List<tb_SanPham> getAll()
        {
            return db.tb_SanPham.ToList();
        }

        public tb_SanPham getItem(int id)
        {
            return db.tb_SanPham.FirstOrDefault(x => x.IDSP == id);
        }

        public void add(tb_SanPham sp)
        {
            try
            {
                db.tb_SanPham.Add(sp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        public void update(tb_SanPham sp)
        {
            tb_SanPham _sp = db.tb_SanPham.FirstOrDefault(x => x.IDSP == sp.IDSP);  
            _sp.TENSP = sp.TENSP;
            _sp.DONGIA = sp.DONGIA;
            _sp.DISABLED = sp.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }

        public void delete(int id)
        {
            tb_SanPham _sp = db.tb_SanPham.FirstOrDefault(x => x.IDSP == id);
            _sp.DISABLED = true;
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
