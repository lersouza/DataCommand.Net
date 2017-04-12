using DataCommand.Core.Tests.Infra;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataCommand.Core.Tests
{
    public class BatchDataCommandTest
    {
        public BatchDataCommandTest()
        {

        }

        [Fact]
        public void BatchInsertTest()
        {
            var dataOptions = new FakeDataOptions() { ConnectionString = "Host=localhost;" };
            var loggerFactory = new FakeLoggerFactory();

            FakeBatchCommand fakeCommand = new FakeBatchCommand(10, dataOptions, loggerFactory);

            //Add up to 30 entities
            //Execute should be execute 3 times.
            for (int i = 0; i < 30; i++)
            {
                fakeCommand.AddEntity(new Entity { Id = i });
            }

            Assert.Equal(3, fakeCommand.ExecuteCount);

            fakeCommand.Run(); //In a real case, this would be called to dump remanescent data

            //Run for last execution (4 times)
            Assert.Equal(4, fakeCommand.ExecuteCount);
        }
    }
}
