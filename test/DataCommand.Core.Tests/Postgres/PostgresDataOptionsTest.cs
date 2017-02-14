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
            var commandEx = new PostgresException();
            var generalEx = new Exception("some error");
            var anotherSpecificEx = new AggregateException("some aggregated error");

            // Should retry only on server specific errors
            Assert.True(dataOptions.ShouldRetryOn(mockServerEx));

            // Otherwise, should not retry
            Assert.False(dataOptions.ShouldRetryOn(commandEx));
            Assert.False(dataOptions.ShouldRetryOn(generalEx));
            Assert.False(dataOptions.ShouldRetryOn(anotherSpecificEx));
        }

        public static T CreateException<T>()
        {
            Type type = typeof(T);
            TypeInfo info = type.GetTypeInfo();
            var constructors = info.DeclaredConstructors;

            foreach (var ctor in constructors)
            {
                if(ctor.GetParameters().Count() == 0)
                    return (T)ctor.Invoke(new object[] { });
                else if(ctor.GetParameters().Count() == 1 && ctor.GetParameters().First().ParameterType.IsAssignableFrom(typeof(string)))
                    return (T)ctor.Invoke(new object[] { "any message" });
            }

            throw new InvalidOperationException($"Could not fnd any constructor for the exception '{type.FullName}'");
        }
    }
}
