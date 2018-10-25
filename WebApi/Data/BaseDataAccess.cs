using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    public abstract class BaseDataAccess
    {
        /// <summary>
        /// Creates and returns a DbCommand object using the specified parameters
        /// </summary>
        protected static DbCommand GetDBStoredProcedure(string procedureName,
            Database db, DbConnection connection)
        {
            DbCommand cmd = db.GetStoredProcCommand(procedureName);
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;

            return cmd;
        }

        /// <summary>
        /// Creates and returns a DbCommand object that will run under a transaction
        /// </summary>
        protected static DbCommand GetDBStoredProcedure(string procedureName,
            Database db, DbTransaction transaction)
        {
            DbCommand cmd = db.GetStoredProcCommand(procedureName);
            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandTimeout = transaction.Connection.ConnectionTimeout;

            return cmd;
        }

    }
}
