using System.Data.OleDb;
using System.Data.Odbc;
using System.Runtime.Versioning;
using static Extractor.Extractor;
using static Extractor.Connector;

namespace Extractor
{
    [SupportedOSPlatform("windows")]
    public class Connector
    {
        // Properties.
        public string ConnectionString { get; set; }
        public OdbcConnection Connection { get; set; }

        // Constructor.
        public Connector()
        {
            ConnectionString = string.Empty;
            Connection = new OdbcConnection();
        }

        // Type of connector.
        public enum ConnectorType
        {
            Odbc,
            OleDb
        }

        public void SetConnectionString(string databasePath)
        {
            ConnectionString = $@"Driver=Microsoft Excel Driver (*.xls);DBQ={databasePath};";
        }

        public void SetConnection()
        {
            Connection = new OdbcConnection(ConnectionString);
        }

        public void OpenConnection()
        {
            try
            {
                Connection.Open();
                OdbcCommand command = new("SELECT * FROM [Sheet1$]", Connection);
                OdbcDataReader reader = command.ExecuteReader();
            }

            catch (OdbcException ex)
            {
                Console.WriteLine($"Error occured here: {ex.Message}");
            }
        }
    }
}
