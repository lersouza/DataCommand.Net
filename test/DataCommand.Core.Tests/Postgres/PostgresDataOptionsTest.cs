using DataCommand.Core.Postgres;
using Moq;
using Npgsql;
using System;
using System.Reflection;
using Xunit;
using System.Linq;

namespace DataCommand.Core.Tests.Postgres
{
    public class PostgresDataOptionsTest
    {
        [Fact]
        public void ConnectionTypeTest()
        {
            var dataOptions = new PostgresDataOptions();
            dataOptions.ConnectionString = "Host=localhost;";

            Assert.IsType<NpgsqlConnection>(dataOptions.CreateConnection());
        }


        [Fact]
        public void ShouldRetryTest()
        {
            var dataOptions = new PostgresDataOptions();
            dataOptions.ConnectionString = "Host=localhost;";

            var mockServerEx = new NpgsqlException();
            var postgresEx = new PostgresException();
            var generalEx = new Exception("some error");
            var anotherSpecificEx = new AggregateException("some aggregated error");

            // Should retry only on server specific errors
            Assert.True(dataOptions.ShouldRetryOn(mockServerEx));

            // Otherwise, should not retry
            Assert.False(dataOptions.ShouldRetryOn(postgresEx));
            Assert.False(dataOptions.ShouldRetryOn(generalEx));
            Assert.False(dataOptions.ShouldRetryOn(anotherSpecificEx));
        }
    }
}
