using DevBridgeAPI.Models;
using DevBridgeAPI.Repository.Dao;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases
{
    public class AssignmentLogic : IAssignmentLogic
    {
        private readonly IAssignmentsDao asgnSelector;
        private readonly IUsersDao usersSelector;

        public AssignmentLogic(IAssignmentsDao asgnSelector, IUsersDao usersSelector)
        {
            this.asgnSelector = asgnSelector;
            this.usersSelector = usersSelector;
        }

        public IEnumerable<Assignment> FindAssignments(int userId)
        {
            return asgnSelector.SelectByUserId(userId);
        }

        public IEnumerable<Assignment> FindSubordinatesAssignments(int managerId)
        {
            IEnumerable<User> subordinates = usersSelector.SelectSubordinates(managerId);
            List<Assignment> assignments = new List<Assignment>();
            foreach(var user in subordinates)
            {
                assignments.AddRange(asgnSelector.SelectByUserId(user.UserId));
            }
            return assignments;
        }

        public IEnumerable<Assignment> SelectAllAssignments()
        {
            return asgnSelector.SelectAllRows().Cast<Assignment>();
        }

        public void AddAssignment(Assignment assignment)
        {
            asgnSelector.AddRow(assignment);
        }
    }
}