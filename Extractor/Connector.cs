using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Extractor
{
    [SupportedOSPlatform("windows")]
    public class Connector
    {
        // Properties (a connector has a connection string and a connection state).
        public OleDbConnection Connection { get; set; }
        public string? ConnectionString { get; set; }

        // Constructor (a connector is created and its properties initialized).
        public Connector(Extractor.ConnectionType connectionType, string databasePath)
        {
            string generic = string.Empty;

            switch (connectionType)
            {
                case Extractor.ConnectionType.OldAccess:
                    generic = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
                    ConnectionString = generic + databasePath;
                    break;

                case Extractor.ConnectionType.NewAccess:
                    generic = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                    ConnectionString = generic + databasePath;
                    break;

                case Extractor.ConnectionType.SqlServer:
                    throw new NotImplementedException();
            }

            Connection = new OleDbConnection(ConnectionString);

            try
            {
                Connection.Open();
            }
            catch (OleDbException)
            {
                throw;
            }
        }
    }
}
