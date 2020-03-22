using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Selector
{
    public class AssignmentsSelector : IModelSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Assignments";
            using (var db = new DbContext().Connection)
            {
                return db.Query<Assignment>(sql);
            }
        }
    }
}