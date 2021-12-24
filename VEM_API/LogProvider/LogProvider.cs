using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Repositories;

namespace VEM_API.LogProvider
{
    public class LogProvider:ILogProvider
    {
        private readonly IEntityRepository _entity;
        public LogProvider(IEntityRepository entity)
        {
            _entity = entity;
        }
        public void AddToLog(string function, string message, bool isError)
        {
            try
            {
                VEMTESTEntities db = new VEMTESTEntities();
                log_VEM log = new log_VEM()
                {
                    log_function = function,
                    log_date = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    log_time = DateTime.UtcNow.ToShortTimeString(),
                    log_message = message,
                    log_isError = isError
                };
                db.log_VEM.Add(log);
                db.SaveChanges();
                _entity.Refresh();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        } //Logovanje korisnickih aktivnosti, post, put, delete, login, logout...

        public IEnumerable<log_VEM> GetLog()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<log_VEM> log = new List<log_VEM>();
            foreach (var l in db.log_VEM.OrderByDescending(x => x.log_id))
            {
                log.Add(new log_VEM()
                {
                    log_id = l.log_id,
                    log_date = l.log_date,
                    log_function = l.log_function,
                    log_message = l.log_message,
                    log_time = l.log_time,
                    log_isError = l.log_isError
                });
            }
            return log;
        }
    }
}