using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class SYS_USER
    {
        Entities db;
        public SYS_USER() 
        { 
            db = Entities.CreateEntities();
        }

        public List<tb_SYS_USER> getAll()
        {
            return db.tb_SYS_USER.ToList();
        }
        public List<tb_SYS_USER> getUserbyDVI(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi).OrderByDescending(x => x.ISGROUP).ToList();
        }
        public List<tb_SYS_USER> getUserbyDViFunc(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi && x.DISABLED == false).OrderByDescending(x => x.ISGROUP).ToList();
        }
        public tb_SYS_USER getItem(int idUser)
        {
            return db.tb_SYS_USER.FirstOrDefault(u => u.IDUSER == idUser);
        }
        public tb_SYS_USER getItem(string username, string macty, string madvi)
        {
            return db.tb_SYS_USER.FirstOrDefault(u => u.USERNAME == username && u.MACTY == macty && u.MADVI == madvi);
        }
        public tb_SYS_USER getByUsername(string username)
        {
            return db.tb_SYS_USER.FirstOrDefault(u => u.USERNAME == username);
        }

        public tb_SYS_USER add(tb_SYS_USER user)
        {
            try
            {
                db.tb_SYS_USER.Add(user);
                db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm người dùng: " + ex.Message);
            }
        }

        public tb_SYS_USER update(tb_SYS_USER user)
        {
            try
            {
                var existing = db.tb_SYS_USER.FirstOrDefault(u => u.IDUSER == user.IDUSER);
                if (existing != null)
                {
                    existing.FULLNAME = user.FULLNAME;
                    existing.USERNAME = user.USERNAME;
                    existing.PASSWORD = user.PASSWORD;
                    existing.MACTY = user.MACTY;
                    existing.MADVI = user.MADVI;
                    existing.ISGROUP = user.ISGROUP;
                    existing.LAST_PWD_CHANGED = user.LAST_PWD_CHANGED;
                    existing.DISABLED = user.DISABLED;
                    db.SaveChanges();
                }
                return existing;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật người dùng: " + ex.Message);
            }
        }

        public void delete(int idUser)
        {
            try
            {
                var user = db.tb_SYS_USER.FirstOrDefault(u => u.IDUSER == idUser);
                if (user != null)
                {
                    db.tb_SYS_USER.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa người dùng: " + ex.Message);
            }
        }

        public bool checkUserExist(string macty, string madvi, string username)
        {
            var us = db.tb_SYS_USER.FirstOrDefault(x => x.MACTY == macty && x.MADVI == madvi && x.USERNAME == username);
            if (us != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
