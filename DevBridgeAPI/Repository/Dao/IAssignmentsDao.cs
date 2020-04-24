using DevBridgeAPI.Models;
using System.Collections.Generic;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IAssignmentsDao
    {
        IEnumerable<IModel> SelectAllRows();
        IEnumerable<Assignment> SelectByUserId(int userId);
        void AddRow(Assignment assignment);
    }
}