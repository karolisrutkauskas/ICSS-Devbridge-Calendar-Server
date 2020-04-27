using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.Tests.Helpers
{
    /// <summary>
    /// Will generate a fake SQL esception by using reflection. Exception message and
    /// SQL error code can be provided with builder methods.
    /// </summary>
    /// <see cref="https://stackoverflow.com/a/29939664">This class is based on this answer</see>
    public class SQLExceptionBuilder
    {
        private int errorNumber;
        private string errorMessage;

        public SqlException Build()
        {
            SqlError error = this.CreateError();
            SqlErrorCollection errorCollection = this.CreateErrorCollection(error);
            SqlException exception = this.CreateException(errorCollection);

            return exception;
        }

        public SQLExceptionBuilder Number(int number)
        {
            this.errorNumber = number;
            return this;
        }

        public SQLExceptionBuilder Message(string message)
        {
            this.errorMessage = message;
            return this;
        }

        public static SQLExceptionBuilder CreateInstance()
        {
            return new SQLExceptionBuilder();
        }

        private SqlError CreateError()
        {
            // Create instance via reflection...
            var ctors = typeof(SqlError).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var firstSqlErrorCtor = ctors.FirstOrDefault(
                ctor =>
                ctor.GetParameters().Length == 7); // Need a specific constructor!
            SqlError error = firstSqlErrorCtor.Invoke(
                new object[]
                {
                this.errorNumber,
                new byte(),
                new byte(),
                string.Empty,
                string.Empty,
                string.Empty,
                new int()
                }) as SqlError;

            return error;
        }

        private SqlErrorCollection CreateErrorCollection(SqlError error)
        {
            // Create instance via reflection...
            var sqlErrorCollectionCtor = typeof(SqlErrorCollection).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            SqlErrorCollection errorCollection = sqlErrorCollectionCtor.Invoke(Array.Empty<object>()) as SqlErrorCollection;

            // Add error...
            typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(errorCollection, new object[] { error });

            return errorCollection;
        }

        private SqlException CreateException(SqlErrorCollection errorCollection)
        {
            // Create instance via reflection...
            var ctor = typeof(SqlException).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            SqlException sqlException = ctor.Invoke(
                new object[]
                { 
                // With message and error collection...
                this.errorMessage,
                errorCollection,
                null,
                Guid.NewGuid()
                }) as SqlException;

            return sqlException;
        }
    }
}