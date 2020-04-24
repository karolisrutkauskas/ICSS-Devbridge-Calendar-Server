using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.UseCases.UserCasesN
{
    public interface IUserLogic
    {
        void RegisterNewUser(int registeredById, Models.User newUser);
        TeamTreeNode GetTeamTree(int rootUserId);
    }
}
