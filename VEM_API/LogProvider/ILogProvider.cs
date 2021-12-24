using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.DbModel;

namespace VEM_API.LogProvider
{
    public interface ILogProvider
    {
        void AddToLog(string function, string message, bool isError);
        IEnumerable<log_VEM> GetLog();
    }
}
