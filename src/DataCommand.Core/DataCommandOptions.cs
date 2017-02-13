using System;
using System.Data;

namespace DataCommand.Core
{
    /// <summary>
    /// This class provides a base implementation for the options used by a <see cref="DataCommand{T}"/>.
    /// Specific providers should extend this class to add repository specific imlementations.
    /// </summary>
    public abstract class DataCommandOptions
    {
        /// <summary>
        /// Gets or sets the connection string to use when creating new connections.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries
        /// </summary>
        public int MaxRetries { get; set; }

        /// <summary>
        /// Gets or sets the ammount of time (in seconds) for command execution.
        /// </summary>
        public int CommandTimeout { get; set; }
        
        /// <summary>
        /// Gets or sets the ammount of time (in seconds) for connection establishment.
        /// </summary>
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// Creates a new connection to the database (not opened).
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection();

        /// <summary>
        /// Indicates whether or not a retry attempt should be made when <paramref name="exception"/> occurs.
        /// </summary>
        /// <param name="exception">The exception to test.</param>
        /// <returns></returns>
        public abstract bool ShouldRetryOn(Exception exception);
    }
}
