using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_FUNC
    {
        Entities db;
        public SYS_FUNC()
        {
            db = Entities.CreateEntities();
        }

        // lấy các menu cha
        public List<tb_SYS_FUNC> getParent()
        {
            return db.tb_SYS_FUNC.Where(x => x.ISGROUP == true && x.MENU == true).OrderBy(s => s.SORT).ToList();
        }

        // lấy các menu con
        public List<tb_SYS_FUNC> getChild(string parent)
        {
            return db.tb_SYS_FUNC.Where(x => x.ISGROUP == false && x.MENU == true && x.PARENT == parent).OrderBy(s => s.SORT).ToList();
        }
    }
}
