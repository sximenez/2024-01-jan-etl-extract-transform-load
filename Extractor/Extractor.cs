using System.Data;

namespace Extractor
{
    public class Extractor
    {
        // Properties (Object.Properties).
        public Connector Connector { get; set; }
        public Retriever Retriever { get; set; }
        public Formatter Formatter { get; set; }
        public Writer Writer { get; set; }

        public static string FindDatabaseExtension(string databasePath)
        {
            return databasePath[databasePath.IndexOf(".")..];
        }

        // Common and effective pattern in programming (dictionary + enum).
        // Static means it imposes on every instance of Extractor.
        static Dictionary<string, ConnectionType> connectionTypes = new Dictionary<string, ConnectionType>()
        {
            {".mdb", ConnectionType.OldAccess },
            {".accdb", ConnectionType.NewAccess },
        };
        public enum ConnectionType
        {
            OldAccess,
            NewAccess,
            SqlServer,
            Excel
        }
        public static ConnectionType FindFormat(string Extension)
        {
            if (!connectionTypes.ContainsKey(Extension))
            {
                throw new Exception("Unhandled file extension.");
            }

            return connectionTypes[Extension];
        }

        // Constructor (an extractor is created and its properties initialized).
        public Extractor(Connector connector, Retriever retriever, Formatter formatter, Writer writer)
        {
            Connector = connector;
            Retriever = retriever;
            Formatter = formatter;
            Writer = writer;
        }

        // Logger (the run is logged).
        public static void Log()
        {
            //Console.WriteLine($"Successful? {(Writer.IsSuccessful ? "YES" : "NO")}");
            //Console.WriteLine($"Connection successful? {Connector.Connection.State == ConnectionState.Open}");
        }
        public static void Main()
        {
            //Connector connector = new Connector(DatabaseFormat, DatabasePath);
            //Retriever retriever = new Retriever(connector.Connection, DatabaseFormat);
            //Formatter formatter = new Formatter(retriever.Schema);
            //Writer writer = new Writer(retriever.Schema);

            //Extractor extractor = new Extractor(connector, retriever, formatter, writer);
            //extractor.Log();
        }
    }
}