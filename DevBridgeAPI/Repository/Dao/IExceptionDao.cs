using DevBridgeAPI.Helpers;
using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IExceptionDao
    {
        void InsertException(ErrorLog exception);
    }
}
