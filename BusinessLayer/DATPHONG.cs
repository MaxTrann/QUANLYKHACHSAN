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
        public List<OBJ_DATPHONG> getAll(DateTime tungay, DateTime denngay, string macty, string madvi)
        {
            var listDP =  db.tb_DatPhong.Where(x => x.NGAYDATPHONG >=  tungay && x.NGAYDATPHONG < denngay && x.MACTY == macty && x.MADVI == madvi).ToList();
            List<OBJ_DATPHONG> lstDP = new List<OBJ_DATPHONG>();
            OBJ_DATPHONG dp;

            foreach (var item in listDP)
            {
                dp = new OBJ_DATPHONG();
                dp.IDDP = item.IDDP;
                dp.IDKH = item.IDKH;
                var kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == item.IDKH);
                dp.HOTEN = kh.HOTEN;
                dp.IDUSER = item.IDUSER;
                dp.NGAYDATPHONG = item.NGAYDATPHONG;
                dp.NGAYTRAPHONG = item.NGAYTRAPHONG;
                dp.MACTY = item.MACTY;
                dp.MADVI = item.MADVI;
                dp.SONGUOIO = item.SONGUOIO;
                dp.SOTIEN = item.SOTIEN;
                dp.STATUS = item.STATUS;
                dp.THEODOAN = item.THEODOAN;
                dp.DISABLED = item.DISABLED;
                dp.GHICHU = item.GHICHU;
                lstDP.Add(dp);
            }
            return lstDP;
        }
        // Lấy 1 đơn đặt phòng theo IDDP
        public tb_DatPhong getItem(int id)
        {
            return db.tb_DatPhong.FirstOrDefault(x => x.IDDP == id);
        }

        // Thêm mới
        public tb_DatPhong add(tb_DatPhong dp)
        {
            try
            {
                db.tb_DatPhong.Add(dp);
                db.SaveChanges();
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm đơn đặt phòng: " + ex.Message);
            }
        }
        public void updateStatus(int idDP)
        {
            var _dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == idDP);
            _dp.STATUS = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật trạng thái đặt phòng: " + ex.Message);
            }
        }
        // Cập nhật
        public tb_DatPhong update(tb_DatPhong dp)
        {
            var _dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == dp.IDDP);
            if (_dp != null)
            {
                _dp.IDKH = dp.IDKH;
                _dp.MACTY = dp.MACTY;
                _dp.MADVI = dp.MADVI;
                _dp.NGAYDATPHONG = dp.NGAYDATPHONG;
                _dp.NGAYTRAPHONG = dp.NGAYTRAPHONG;
                _dp.SONGUOIO = dp.SONGUOIO;
                _dp.SOTIEN = dp.SOTIEN;
                _dp.IDUSER = dp.IDUSER;
                _dp.DISABLED = dp.DISABLED;
                _dp.THEODOAN = dp.THEODOAN;
                _dp.GHICHU = dp.GHICHU;
                _dp.CREATED_DATE = dp.CREATED_DATE;
                //_dp.STATUS = dp.STATUS;
                //_dp.UPDATE_DATE = dp.UPDATE_DATE;
                //_dp.UPDATE_BY = dp.UPDATE_BY;
                try
                {
                    db.SaveChanges();
                    return dp;
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
