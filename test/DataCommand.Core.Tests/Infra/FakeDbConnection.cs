using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataCommand.Core.Tests.Infra
{
    public class FakeDbConnection : DbConnection
    {
        private ConnectionState _state;
        private Action _onOpeningConnection;

        public FakeDbConnection(
            string connectionString,
            ConnectionState state = ConnectionState.Closed,
            Action onOpeningConnection = null)
        {
            ConnectionString = connectionString;
            _state = state;
            _onOpeningConnection = onOpeningConnection;
        }

        public void SetState(ConnectionState state)
            => _state = state;

        public override ConnectionState State => _state;

        public override string ConnectionString { get; set; }

        public override string Database { get; } = "Fake Database";

        public override string DataSource { get; } = "Fake DataSource";

        public override string ServerVersion
        {
            get { throw new NotImplementedException(); }
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public int OpenCount { get; private set; }

        public override void Open()
        {
            if (_onOpeningConnection != null)
            {
                _onOpeningConnection();
            }

            OpenCount++;
            _state = ConnectionState.Open;
        }

        public int OpenAsyncCount { get; private set; }

        public override Task OpenAsync(CancellationToken cancellationToken)
        {
            OpenAsyncCount++;
            return base.OpenAsync(cancellationToken);
        }

        public int CloseCount { get; private set; }

        public override void Close()
        {
            CloseCount++;
            _state = ConnectionState.Closed;
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new InvalidOperationException();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new InvalidOperationException();
        }

        public int DisposeCount { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeCount++;
            }

            base.Dispose(disposing);
        }
    }
}
