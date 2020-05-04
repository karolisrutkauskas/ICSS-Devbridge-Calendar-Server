using DevBridgeAPI.Models;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases
{
    public class GoalsLogic : IGoalsLogic
    {
        private readonly IGoalsDao goalsSelector;
        private readonly IUsersDao usersSelector;

        public GoalsLogic(IGoalsDao goalsSelector, IUsersDao usersSelector)
        {
            this.goalsSelector = goalsSelector;
            this.usersSelector = usersSelector;
        }

        public IEnumerable<Goal> SelectAllGoalsByUser(string email)
        {
            var user = usersSelector.SelectByEmail(email);
            return goalsSelector.SelectByUserId(user.UserId);
        }
    }
}