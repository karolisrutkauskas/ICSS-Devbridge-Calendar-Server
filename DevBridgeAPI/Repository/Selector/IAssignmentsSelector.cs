using DevBridgeAPI.Models;
using System.Collections.Generic;

namespace DevBridgeAPI.Repository.Selector
{
    public interface IAssignmentsSelector
    {
        IEnumerable<IModel> SelectAllRows();
        IEnumerable<Assignment> SelectByUserId(int userId);
    }
}