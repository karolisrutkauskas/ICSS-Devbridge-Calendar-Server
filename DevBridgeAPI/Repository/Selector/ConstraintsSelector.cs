using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Selector
{
    public class ConstraintsSelector : IModelSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Constraints";
            using (var db = new DbContext().Connection)
            {
                return db.Query<Constraint>(sql);
            }
        }
    }
}