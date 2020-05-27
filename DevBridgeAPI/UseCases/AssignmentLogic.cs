using Dapper;
using DevBridgeAPI.Helpers;
using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.Models.Patch;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.Resources;
using DevBridgeAPI.UseCases.Exceptions;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            foreach (var user in subordinates)
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
            // Validate against user restrictions
            var valInfo = ValidateUserRestrictions(assignment.Date, assignment.UserId);

            if (!valInfo.IsValid)
                throw new ValidationFailedException(valInfo);

            asgnSelector.AddRow(assignment);
        }

        public Assignment UpdateAssignment(UpdatedAssignment assignment, int id)
        {
            var assignmentToUpdate = asgnSelector.SelectRow(id);

            assignmentToUpdate.TopicId = assignment.TopicId;
            assignmentToUpdate.Comments = assignment.Comments;
            assignmentToUpdate.Date = assignment.Date;

            asgnSelector.UpdateRow(assignmentToUpdate);

            return assignmentToUpdate;
        }

        private ValidationInfo ValidateUserRestrictions(DateTime newAssignmentDate, int userId)
        {
            newAssignmentDate = newAssignmentDate.Date;
            var yearBounds = new Tuple<DateTime, DateTime>(
                new DateTime(newAssignmentDate.Year, 1, 1),                    // First day of year
                new DateTime(newAssignmentDate.Year + 1, 1, 1).AddDays(-1));   // Last Day of year

            var quarterStartMonth = ((newAssignmentDate.Month - 1) / 3) * 3 + 1;
            var quarterBounds = new Tuple<DateTime, DateTime>(
                new DateTime(newAssignmentDate.Year, quarterStartMonth, 1),
                new DateTime(newAssignmentDate.Year, quarterStartMonth + 2,
                    DateTime.DaysInMonth(newAssignmentDate.Year, quarterStartMonth + 2)
                    )
                );

            var monthBounds = new Tuple<DateTime, DateTime>(
                new DateTime(newAssignmentDate.Year, newAssignmentDate.Month, 1),
                new DateTime(newAssignmentDate.Year, newAssignmentDate.Month,
                    DateTime.DaysInMonth(newAssignmentDate.Year, newAssignmentDate.Month)
                    )
                );

            var existingAsgn = asgnSelector.SelectByUserId(
                userId: userId,
                minDate: yearBounds.Item1,
                maxDate: yearBounds.Item2);

            var monthlyCounter = 0;
            var quarterlyCounter = 0;
            var yearlyCounter = 0;
            foreach(var asgn in existingAsgn)
            {
                // Yearly counter condition (always true)
                yearlyCounter++;

                // Quarterly counter condition
                if (asgn.Date >= quarterBounds.Item1.Date && asgn.Date <= quarterBounds.Item2.Date)
                    quarterlyCounter++;

                // Monthly counter condition
                if (asgn.Date >= monthBounds.Item1.Date && asgn.Date <= monthBounds.Item2.Date)
                    monthlyCounter++;
            }

            var user = usersSelector.SelectByID(userId);
            var errors = new LinkedList<ErrorMessage>();
            if (user.YearlyLimit != null && user.YearlyLimit <= yearlyCounter)
                errors.AddLast(Errors.ExceededAssignmentLimits(nameof(user.YearlyLimit)));

            if (user.QuarterLimit != null && user.QuarterLimit <= quarterlyCounter)
                errors.AddLast(Errors.ExceededAssignmentLimits(nameof(user.QuarterLimit)));

            if (user.MonthlyLimit != null && user.MonthlyLimit <= monthlyCounter)
                errors.AddLast(Errors.ExceededAssignmentLimits(nameof(user.MonthlyLimit)));

            if (user.ConsecLimit != null)
            {
                var consecBeforeAsgn = existingAsgn
                    .Where(x => x.Date.Date <= newAssignmentDate && x.Date.Date >= newAssignmentDate.AddDays(-user.ConsecLimit.Value));
                var consecAfterAsgn = existingAsgn
                    .Where(x => x.Date.Date >= newAssignmentDate && x.Date.Date <= newAssignmentDate.AddDays(user.ConsecLimit.Value));

                if (user.ConsecLimit <= consecBeforeAsgn.Count() || user.ConsecLimit <= consecAfterAsgn.Count())
                    errors.AddLast(Errors.ExceededAssignmentLimits(nameof(user.ConsecLimit)));
            }

            return new ValidationInfo(errors);
        }
    }
}