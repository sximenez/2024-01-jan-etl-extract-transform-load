using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Extractor
{
    #region Interface

    public interface IConnection
    {
        string SetConnectionString(string databasePath);
        IDbConnection SetConnection();
        void OpenConnection();
    }

    #endregion Interface

    #region ODBC

    [SupportedOSPlatform("windows")]
    public class OdbcStrategy : IConnection
    {
        string connectionString = string.Empty;
        OdbcConnection connection = new();

        public string SetConnectionString(string databasePath)
        {
            connectionString = $@"Driver=Microsoft Excel Driver (*.xls);DBQ={databasePath};";
            return connectionString;
        }

        public IDbConnection SetConnection()
        {
            connection = new OdbcConnection(connectionString);
            return connection;
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
                OdbcCommand command = new("SELECT * FROM [Sheet1$]", connection);
                OdbcDataReader reader = command.ExecuteReader();
            }

            catch (OdbcException ex)
            {
                Console.WriteLine($"Error occured here: {ex.Message}");
            }
        }
    }

    #endregion ODBC

    #region OLEDB

    [SupportedOSPlatform("windows")]
    public class OleDbStrategy : IConnection
    {
        string connectionString = string.Empty;
        OleDbConnection connection = new();

        public string SetConnectionString(string databasePath)
        {
            connectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath};Extended Properties=Excel 8.0;";
            return connectionString;
        }

        public IDbConnection SetConnection()
        {
            connection = new OleDbConnection(connectionString);
            return connection;
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new("SELECT * FROM [Sheet1$]", connection);
                OleDbDataReader reader = command.ExecuteReader();
            }

            catch (OleDbException ex)
            {
                Console.WriteLine($"Error occured here: {ex.Message}");
            }
        }
    }

    #endregion OLEDB

    #region Connector

    [SupportedOSPlatform("windows")]
    public class Connector
    {
        // Type of connector.
        public enum ConnectorType
        {
            Odbc,
            OleDb
        }

        // Properties.
        public string ConnectionString { get; set; }
        public IDbConnection Connection { get; set; }

        // Constructor.
        public Connector(ConnectorType connectorType, string databasePath)
        {
            ConnectionString = databasePath;
            Connection = new OdbcConnection();

            switch (connectorType)
            {
                case ConnectorType.Odbc:
                    ConnectionString = SetConnectionString(connectorType, databasePath);
                    Connection = SetConnection(connectorType);
                    OpenConnection(Connection);
                    break;

                case ConnectorType.OleDb:
                    ConnectionString = SetConnectionString(connectorType, databasePath);
                    Connection = SetConnection(connectorType);
                    OpenConnection(Connection);
                    break;
            }
        }

        public string SetConnectionString(ConnectorType connectorType, string databasePath)
        {
            switch (connectorType)
            {
                case ConnectorType.Odbc:
                    ConnectionString = @$"Driver=Microsoft Excel Driver (*.xls);DBQ={databasePath};";
                    break;
                case ConnectorType.OleDb:
                    ConnectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={databasePath};Extended Properties=Excel 8.0;";
                    break;
            }
            return ConnectionString;
        }

        public IDbConnection SetConnection(ConnectorType connectorType)
        {
            switch (connectorType)
            {
                case ConnectorType.Odbc:
                    Connection = new OdbcConnection(ConnectionString);
                    break;
                case ConnectorType.OleDb:
                    Connection = new OleDbConnection(ConnectionString);
                    break;
            }
            return Connection;
        }

        public static void OpenConnection(IDbConnection connection)
        {
            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM [Sheet1$]";
                IDataReader reader = command.ExecuteReader();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error occured here: {ex.Message}");
            }
        }
    }

    #endregion Connector
}
