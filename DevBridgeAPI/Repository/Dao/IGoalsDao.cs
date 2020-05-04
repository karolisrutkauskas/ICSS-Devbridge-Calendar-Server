using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IGoalsDao
    {
        IEnumerable<Goal> SelectByUserId(int userId);
    }
}
