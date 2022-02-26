using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public enum LogType
    {
        None,
        Success,
        Error
    };

    public class DataLogs
    {
        public String Message { get; private set; }

        public LogType Type { get; private set; }
        public DataLogs(LogType type, String message)
        {
            Type = type;
            Message = message;
        }
    }
}
