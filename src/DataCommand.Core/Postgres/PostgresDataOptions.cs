using Npgsql;
using System;
using System.Data;

namespace DataCommand.Core.Postgres
{
    /// <summary>
    /// Provides data options for Postgresql.
    /// </summary>
    /// <remarks>
    /// When creating new connections, it already sets the <see cref="DataCommandOptions.ConnectionTimeout"/> and <see cref="DataCommandOptions.CommandTimeout"/> for the new connection.
    /// </remarks>
    public class PostgresDataOptions : DataCommandOptions
    {
        /// <summary>
        /// Creates a new, unopened, connection.
        /// </summary>
        /// <returns>A <see cref="NpgsqlConnection"/> objects, with the provided connections string and settings.</returns>
        public override IDbConnection CreateConnection()
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder(ConnectionString);
            connectionStringBuilder.Timeout = ConnectionTimeout;
            connectionStringBuilder.CommandTimeout = CommandTimeout;

            return new NpgsqlConnection(connectionStringBuilder.ToString());
        }

        /// <summary>
        /// Indicates whether or not a retry should be attempted when <paramref name="exception"/> was thrown.
        /// </summary>
        /// <remarks>
        /// This method will return true if <see cref="NpgsqlException"/> is thrown. 
        /// This kind of exception is related to server side issues, unlike <see cref="PostgresException"/>. So this seems to be a great idea to retry on it.
        /// </remarks>
        /// <param name="exception">The thrown exception to test.</param>
        /// <returns><c>true</c>, if a retry should be made. <c>false</c>, otherwise.</returns>
        public override bool ShouldRetryOn(Exception exception)
        {
            return (exception is NpgsqlException && !(exception is PostgresException));
        }
    }
}
