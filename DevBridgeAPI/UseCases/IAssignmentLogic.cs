using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Patch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.UseCases
{
    public interface IAssignmentLogic
    {
        IEnumerable<Assignment> SelectAllAssignments();
        IEnumerable<Assignment> SelectAllAssignmentsByUser(string email);
        IEnumerable<Assignment> FindSubordinatesAssignments(string managerEmail);
        void AddAssignment(Assignment assignment);
        Assignment UpdateAssignment(UpdatedAssignment assignment, int id);
    }
}
