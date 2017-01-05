using DataCommand.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace DataCommand.Postgres.Infrastructure
{
    public class PostgresProviderManager : IDBProviderManager
    {
        public IDbConnection CreateConnection() => new NpgsqlConnection(connectionString);

        public bool ShouldRetryOn(Exception ex) => (ex is NpgsqlException) ? true : false;
    }
}
