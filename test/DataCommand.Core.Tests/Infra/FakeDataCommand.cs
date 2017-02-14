using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DataCommand.Core.Tests.Infra
{
    public class FakeDataCommand<T> : DataCommand<T>
    {
        public ConnectionState LastExecuteState { get; set; }
        public int ExecuteCount { get; set; } = 0;

        private Func<IDbConnection, T> _returnObj;

        public FakeDataCommand(string name, DataCommandOptions options, ILoggerFactory loggerFactory, Func<IDbConnection, T> returnObj = null) 
            : base(name, options, loggerFactory)
        {
            _returnObj = returnObj;
        }

        protected override T Execute(IDbConnection connection, DataCommandOptions options)
        {
            LastExecuteState = connection.State;
            ExecuteCount++;

            if (_returnObj != null)
                return _returnObj(connection);
            else
                return default(T);
        }
    }
}
