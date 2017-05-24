using DataCommand.Core.Tests.Infra;
using Moq;
using System;
using System.Data;
using Xunit;
using Moq.Protected;
using System.Threading.Tasks;
using Npgsql;

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
            FakeLoggerFactory loggerFac = new FakeLoggerFactory();

            Assert.Throws<ArgumentNullException>(() => new FakeDataCommand<int>(null, null, null));
            Assert.Throws<ArgumentNullException>(() => new FakeDataCommand<int>("", null, null));
            Assert.Throws<ArgumentNullException>(() => new FakeDataCommand<int>(" ", null, null));
            Assert.Throws<ArgumentNullException>(() => new FakeDataCommand<int>("WithAName", defaultOpts, null));

            // Should check argument exceptions, because some properties in data options are empty
            // ConnectionString is a required option
            Assert.Throws<ArgumentException>(() => new FakeDataCommand<int>("WithAName", defaultOpts, loggerFac));

            // Now, let's setup this as this should be
            defaultOpts.ConnectionString = "some connection string;";

            // Creates a data command. No Exception should be thrown at this time.
            new FakeDataCommand<int>("WithAName", defaultOpts, loggerFac);
        }

        [Fact]
        public void InvalidConnectionTest()
        {
            var mock = new Mock<DataCommandOptions>();
            mock.Setup(opt => opt.CreateConnection()).Returns(() => null);

            //Setup some valid properties
            mock.Object.ConnectionString = "Host=localhost;";

            FakeLoggerFactory loggerFac = new FakeLoggerFactory();
            var command = new FakeDataCommand<int>("ACommand", mock.Object, loggerFac, (conn) => 0);

            Assert.Throws<InvalidOperationException>(() => command.Run());
        }

        [Fact]
        public void ExecuteWithOpenConnectionTest()
        {
            var dataOptions = new FakeDataOptions() { ConnectionString = "Host=localhost;" };
            var loggerFactory = new FakeLoggerFactory();

            var mockCommand = new FakeDataCommand<int>("ACommand", dataOptions, loggerFactory, (conn) => 0);

            //Setup the mock command
            var ret = mockCommand.Run();

            //Do the asserts
            Assert.True(dataOptions.CreatedConnection != null);
            Assert.True(dataOptions.CreatedConnection.OpenCount == 1);
            Assert.True(dataOptions.CreatedConnection.State == ConnectionState.Closed);
            Assert.True(mockCommand.LastExecuteState == ConnectionState.Open);
            Assert.True(ret == 0);
        }

        [Fact]
        public void RetryTest1()
        {
            int tryOpenCount = 0;

            FakeDataOptions defaultOpts = new FakeDataOptions() {
                ConnectionString = "Host=localhost;",
                MaxRetries = 2,
                OnOpeningConnection = () => {
                    tryOpenCount++;
                    Task.Delay(1000);

                    throw new Npgsql.NpgsqlException();
            } };

            defaultOpts.ShouldRetryList.Add(typeof(Npgsql.NpgsqlException));

            FakeLoggerFactory loggerFac = new FakeLoggerFactory();

            DataCommand<int> command = new FakeDataCommand<int>("CommandForRetry", defaultOpts, loggerFac);
            Exception exception = null;

            try
            {
                command.Run();
            }
            catch(Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(tryOpenCount, 3); //The first attempt + 2 retries
            Assert.True(exception != null);
            Assert.IsType<Npgsql.NpgsqlException>(exception);
        }

        [Fact]
        public void RetryTest2()
        {
            FakeLoggerFactory loggerFac = new FakeLoggerFactory();
            FakeDataOptions defaultOpts = new FakeDataOptions()
            {
                ConnectionString = "Host=localhost;",
                MaxRetries = 2,
            };

            defaultOpts.ShouldRetryList.Add(typeof(Npgsql.NpgsqlException));


            FakeDataCommand<int> command = new FakeDataCommand<int>("CommandForRetry", defaultOpts, loggerFac, (conn) => {
                throw new PostgresException(); // Emulating a Query error, for instance.
            });
            Exception exception = null;

            try
            {
                command.Run();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Equal(command.ExecuteCount, 1); //Only one attempt, since no retry should be made to PostgresException
            Assert.True(exception != null);
            Assert.IsType<Npgsql.PostgresException>(exception);
        }
    }
}
