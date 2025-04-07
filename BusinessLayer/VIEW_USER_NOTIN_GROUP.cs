using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class VIEW_USER_NOTIN_GROUP
    {
        Entities db;
        public VIEW_USER_NOTIN_GROUP()
        {
            db = Entities.CreateEntities();
        }

        public List<V_USER_NOTIN_GROUP> getUserNotInGroup(string macty, string madvi)
        {
            return db.V_USER_NOTIN_GROUP.Where(x => x.MACTY == macty && x.MADVI == madvi && x.ISGROUP == false && x.DISABLED == false).ToList(); 
        }
    }
}
