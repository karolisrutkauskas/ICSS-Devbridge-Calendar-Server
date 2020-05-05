﻿using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Post;
using System.Collections.Generic;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;
using DevBridgeAPI.Models.Patch;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IUsersDao
    {
        IEnumerable<IModel> SelectAllRows();
        User SelectByID(int userId);
        User SelectByEmail(string email);
        IModel SelectOneRow(string username, string password);
        IEnumerable<User> SelectSubordinates(int managerId);
        void InsertNewUser(PostUser user);
        void UpdateUser(User updatedUser);
        void UpdateUserAsync(User updatedUser);
        void UpdateGlobalRestrictions(UserRestrictions restrictions);
        void UpdateTeamRestrictions(UserRestrictions restrictions, int managerId);
        bool IsAncestorOf(int ancestor, int descendant);
    }
}