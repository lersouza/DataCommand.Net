using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCommand.Core.Tests.Infra
{
    public class FakeLoggerFactory : ILoggerFactory
    {

        public FakeLoggerFactory()
        {
        }

        public void AddProvider(ILoggerProvider provider)
        {
        }

        public ILogger CreateLogger(string name)
        {
            return Mock.Of<ILogger>();
        }

        public void Dispose()
        {
        }
    }
}
