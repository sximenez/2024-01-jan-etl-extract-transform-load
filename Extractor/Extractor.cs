using System.Runtime.Versioning;

namespace Extractor
{
    public class Extractor
    {
        // Properties.
        public Connector Connector { get; set; }
        public Retriever Retriever { get; set; }
        public Formatter Formatter { get; set; }
        public Writer Writer { get; set; }

        // Constructor.
        [SupportedOSPlatform("windows")]
        public Extractor(Connector.ConnectorType connectorType, string databasePath, string query, string outputPath)
        {
            Connector = new Connector(connectorType, databasePath);

            using (Connector.Connection)
            {
                Retriever = new Retriever(Connector.Connection, query);
            }

            Formatter = new Formatter(Retriever.Data, Retriever.NumberOfColumns);
            Writer = new Writer(outputPath, Retriever.Headers, Formatter.FormattedData, Retriever.NumberOfColumns);
        }

        // Logger.
        public static void Log(Writer writer)
        {
            Console.WriteLine($"Successful? {(writer.HasWrittenFile ? "YES" : "NO")}");
        }

        // Runner.
        [SupportedOSPlatform("windows")]
        public static void Main()
        {
            Connector.ConnectorType connectorType = Connector.ConnectorType.OleDb;
            string databasePath = @"C:\Users\steven.jimenez\source\repos\2024-01-jan-etl-extract-transform-load\Input.xls";
            string query = "SELECT * FROM [Sheet1$]";
            string outputPath = @"C:\Users\steven.jimenez\source\repos\2024-01-jan-etl-extract-transform-load\Output.xlsx";

            Extractor extractor = new Extractor(connectorType, databasePath, query, outputPath);
            Log(extractor.Writer);
        }
    }
}