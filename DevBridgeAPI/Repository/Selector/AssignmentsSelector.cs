using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Selector
{
    public class AssignmentsSelector : IModelSelector, IAssignmentsSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Assignments";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql);
            }
        }

        public IEnumerable<Assignment> SelectByUserId(int userId)
        {
            string sql = "SELECT * FROM Assignments " +
                         "WHERE UserId = @userId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql, new { userId });
            }
        }
    }
}