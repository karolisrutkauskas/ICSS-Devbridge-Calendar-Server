using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.UseCases
{
    public interface IGoalsLogic
    {
        IEnumerable<Goal> SelectAllGoalsByUser(string email);
    }
}
