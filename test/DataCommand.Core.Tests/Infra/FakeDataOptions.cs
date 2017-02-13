using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataCommand.Core.Tests.Infra
{
    public class FakeDataOptions : DataCommandOptions
    {
        public override IDbConnection CreateConnection()
        {
            return null;
        }

        public override bool ShouldRetryOn(Exception exception)
        {
            return false;
        }
    }
}
