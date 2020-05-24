using DevBridgeAPI.Models;
using System.Collections.Generic;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IAssignmentsDao
    {
        Assignment SelectRow(int assignmentId);
        IEnumerable<IModel> SelectAllRows();
        IEnumerable<Assignment> SelectByUserId(int userId);
        void AddRow(Assignment assignment);
        void UpdateRow(Assignment assignment);
        IEnumerable<Assignment> SelectPlannedInFuture(int userId);
    }
}