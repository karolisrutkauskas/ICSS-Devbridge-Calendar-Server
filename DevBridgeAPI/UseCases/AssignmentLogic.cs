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

        public IEnumerable<Assignment> FindSubordinatesAssignments(string managerEmail)
        {
            var manager = usersSelector.SelectByEmail(managerEmail);
            IEnumerable<User> subordinates = usersSelector.SelectSubordinates(manager.UserId);
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

        public IEnumerable<Assignment> SelectAllAssignmentsByUser(string email)
        {
            var user = usersSelector.SelectByEmail(email);
            return asgnSelector.SelectByUserId(user.UserId);
        }

        public void AddAssignment(Assignment assignment)
        {
            asgnSelector.AddRow(assignment);
        }
    }
}