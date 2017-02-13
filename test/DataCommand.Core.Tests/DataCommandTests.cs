using DataCommand.Core.Tests.Infra;
using System;
using Xunit;

namespace DataCommand.Core.Tests
{
    public class DataCommandTests
    {
        public DataCommandTests()
        {
        }

        [Fact]
        public void ConstructorTest()
        {
            DataCommandOptions defaultOpts = new FakeDataOptions();
            TestLoggerFactory loggerFac = new TestLoggerFactory();

            Assert.Throws<ArgumentNullException>(() => new NonQueryDataCommand(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new NonQueryDataCommand("", null, null));
            Assert.Throws<ArgumentNullException>(() => new NonQueryDataCommand(" ", null, null));
            Assert.Throws<ArgumentNullException>(() => new NonQueryDataCommand("WithAName", defaultOpts, null));

            // Should check argument exceptions, because some properties in data options are empty
            // ConnectionString is a required option
            Assert.Throws<ArgumentException>(() => new NonQueryDataCommand("WithAName", defaultOpts, loggerFac));

            // Now, let's setup this as this should be
            defaultOpts.ConnectionString = "some connection string;";

            // Creates a data command. No Exception should be thrown at this time.
            new NonQueryDataCommand("WithAName", defaultOpts, loggerFac);
        }
    }
}
