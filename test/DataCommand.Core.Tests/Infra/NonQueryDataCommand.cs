using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DataCommand.Core.Tests.Infra
{
    public class NonQueryDataCommand : DataCommand<int>
    {
        public NonQueryDataCommand(string name, DataCommandOptions options, ILoggerFactory loggerFactory) : base(name, options, loggerFactory)
        {
        }

        protected override int Execute(IDbConnection connection, DataCommandOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
