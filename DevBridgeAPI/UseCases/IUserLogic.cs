using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.UseCases
{
    public interface IUserLogic
    {
        void RegisterNewUser(int registeredById, User newUser);
    }
}
