using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Logging;

namespace DataCommand.Core.Tests.Infra
{
    internal class Entity
    {
        public int Id { get; set; }
    }

    internal class FakeBatchCommand : BatchDataCommand<Entity, int>
    {
        public int ExecuteCount { get; private set; }

        public FakeBatchCommand(int batchSize, DataCommandOptions options, ILoggerFactory loggerFactory) 
            : base("FakeBatchCommand", batchSize, options, loggerFactory)
        {
        }

        protected override int Execute(IDbConnection connection, DataCommandOptions options)
        {
            ExecuteCount++;
            return Entities.Count;
        }
    }
}
