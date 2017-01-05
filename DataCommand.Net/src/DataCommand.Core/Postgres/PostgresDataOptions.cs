using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataCommand.Core.Postgres
{
    public class PostgresDataOptions : DataCommandOptions
    {
        public override IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        public override bool ShouldRetryOn(Exception exception)
        {
            return (exception is NpgsqlException);

        }
    }
}
