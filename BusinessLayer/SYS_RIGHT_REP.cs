using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class SYS_RIGHT_REP
    {
        Entities db;
        public SYS_RIGHT_REP()
        {
            db = Entities.CreateEntities();
        }
        public void update(int idUser, int rep_code, bool right)
        {
            tb_SYS_RIGHT_REP sRight = db.tb_SYS_RIGHT_REP.FirstOrDefault(x => x.IDUSER == idUser && x.REP_CODE == rep_code);
            try
            {
                sRight.USER_RIGHT = right;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

    }
}
