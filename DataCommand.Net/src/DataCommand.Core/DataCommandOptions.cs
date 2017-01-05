using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCommand.Core
{
    /// <summary>
    /// The options to be used by a <see cref="DataCommand{T}"/>, like connection strings.
    /// </summary>
    public class DataCommandOptions
    {
        /// <summary>
        /// Gets or sets the connection string to be used by the data service.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of retries that will be performed in case of connection Errors.
        /// </summary>
        public int MaxRetries { get; set; }
    }
}
