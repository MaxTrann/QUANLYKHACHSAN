using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
   
    public class PHONG
    {
        Entities db;
        public PHONG()
        {
            db = Entities.CreateEntities();
        }

        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        public List<tb_Phong> getByTang(int idTang)
        {
            return db.tb_Phong.Where(x => x.IDTANG == idTang).ToList();
        }
    }
}
