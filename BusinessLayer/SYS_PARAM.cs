using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class SYS_PARAM
    {
        Entities db;
        public SYS_PARAM()
        {
            db = Entities.CreateEntities();
        }
        public tb_Param getParam()
        {
            return db.tb_Param.FirstOrDefault();
        }
    }
}
