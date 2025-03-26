using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DATPHONG
    {
        Entities db;
        public DATPHONG()
        {
            db = Entities.CreateEntities();
        }

        // Lấy tất cả đơn đặt phòng
        public List<tb_DatPhong> getAll()
        {
            return db.tb_DatPhong.ToList();
        }

        // Lấy 1 đơn đặt phòng theo IDDP
        public tb_DatPhong getItem(int id)
        {
            return db.tb_DatPhong.FirstOrDefault(x => x.IDDP == id);
        }

        // Thêm mới
        public void add(tb_DatPhong dp)
        {
            try
            {
                db.tb_DatPhong.Add(dp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm đơn đặt phòng: " + ex.Message);
            }
        }

        // Cập nhật
        public void update(tb_DatPhong dp)
        {
            var _dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == dp.IDDP);
            if (_dp != null)
            {
                _dp.IDKH = dp.IDKH;
                _dp.NGAYDATPHONG = dp.NGAYDATPHONG;
                _dp.NGAYTRAPHONG = dp.NGAYTRAPHONG;
                _dp.SOTIEN = dp.SOTIEN;
                _dp.SONGUOIO = dp.SONGUOIO;
                _dp.IDUSER = dp.IDUSER;
                _dp.MACTY = dp.MACTY;
                _dp.MADVI = dp.MADVI;
                _dp.STATUS = dp.STATUS;
                _dp.DISABLED = dp.DISABLED;
                _dp.THEODOAN = dp.THEODOAN;
                _dp.GHICHU = dp.GHICHU;
                _dp.CREATED_DATE = dp.CREATED_DATE;
                _dp.UPDATE_DATE = dp.UPDATE_DATE;
                _dp.UPDATE_BY = dp.UPDATE_BY;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật đơn đặt phòng: " + ex.Message);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy đơn đặt phòng cần cập nhật.");
            }
        }

        // Xóa mềm (DISABLED = true)
        public void delete(int id)
        {
            var _dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == id);
            if (_dp != null)
            {
                _dp.DISABLED = true;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi xóa đơn đặt phòng: " + ex.Message);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy đơn đặt phòng cần xóa.");
            }
        }
    }
}
