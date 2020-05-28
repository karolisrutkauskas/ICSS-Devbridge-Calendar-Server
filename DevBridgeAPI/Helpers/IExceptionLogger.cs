using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.Helpers
{
    public interface IExceptionLogger
    {
        void LogException(Exception exception);
    }
}
