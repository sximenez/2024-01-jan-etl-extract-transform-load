using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Extractor
{
    [SupportedOSPlatform("windows")]
    public class Retriever
    {
        // Properties.
        public List<string> Headers { get; set; }
        public List<string> Data { get; set; }
        public int NumberOfColumns { get; set; }

        // Constructor.
        public Retriever()
        {
            Headers = new List<string>();
            Data = new List<string>();
        }

        public void GetData(OdbcConnection connection, string query)
        {
            OdbcCommand command = new(query, connection);
            OdbcDataReader reader = command.ExecuteReader();
            NumberOfColumns = reader.FieldCount;

            if (NumberOfColumns > 0)
            {

                for (int i = 0; i < NumberOfColumns; i++)
                {
                    string header = reader.GetName(i);
                    Headers.Add(header);
                }

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Data.Add(reader.GetString(i));
                    }
                }
            }
        }
    }
}
