using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataCommand.Core.Tests.Infra
{
    public class FakeDataOptions : DataCommandOptions
    {
        public List<Type> ShouldRetryList { get; } = new List<Type>();

        public FakeDbConnection CreatedConnection { get; set; }

        public Action OnOpeningConnection { get; set; }

        public override IDbConnection CreateConnection()
        {
            CreatedConnection = new FakeDbConnection(ConnectionString, onOpeningConnection: OnOpeningConnection);
            return CreatedConnection;
        }

        public override bool ShouldRetryOn(Exception exception)
        {
            return ShouldRetryList.FirstOrDefault(t => t.FullName.Equals(exception.GetType().FullName)) != null;
        }
    }
}
