using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases.UserCasesN
{
    public class TeamTreeNodeFactory : ITeamTreeNodeFactory
    {
        private readonly IUsersDao usersDao;

        public TeamTreeNodeFactory(IUsersDao usersDao)
        {
            this.usersDao = usersDao;
        }
        public TeamTreeNode ConstructFromRoot(User user)
        {
            var result = new TeamTreeNode();
            result.This = user;
            result.Children = FindAndGenerateChildren(user.UserId);
            return result;
        }

        private IEnumerable<TeamTreeNode> FindAndGenerateChildren(int parentId)
        {
            var result = new List<TeamTreeNode>();
            foreach (var child in usersDao.SelectSubordinates(parentId))
            {
                result.Add(ConstructFromRoot(child));
            }
            if (result.Count == 0) return null; 
            return result;
        }
    }
}