﻿using DevBridgeAPI.Models.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBridgeAPI.Models.Patch;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public interface IUserLogic
    {
        void RegisterNewUser(PostUser newUser);
        TeamTreeNode GetTeamTree(int rootUserId);
        User ChangeRestrictions(UserRestrictions userRestrictions, int userId);
    }
}
