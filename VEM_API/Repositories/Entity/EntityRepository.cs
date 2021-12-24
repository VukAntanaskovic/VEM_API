using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;

namespace VEM_API.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        public void Refresh()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            foreach (var entity in db.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }
    }
}