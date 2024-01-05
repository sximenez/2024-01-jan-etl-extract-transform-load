using System.Data;
using System.Runtime.Versioning;

namespace Extractor
{
    [SupportedOSPlatform("windows")]
    public class Retriever
    {
        // Properties.
        public List<string> Headers { get; set; }
        public List<object> Data { get; set; }
        public int NumberOfColumns { get; set; }

        // Constructor.
        public Retriever()
        {
            Headers = new List<string>();
            Data = new List<object>();
            NumberOfColumns = 0;
        }

        public void GetData(IDbConnection connection, string query)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = query;
            IDataReader reader = command.ExecuteReader();
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
                    for (int i = 0; i < NumberOfColumns; i++)
                    {
                        var data = reader.GetValue(i);
                        if (data.GetType() == typeof(string))
                        {
                            Data.Add(reader.GetString(i));
                        }
                        else
                        {
                            Data.Add(reader.GetDouble(i));
                        }
                    }
                }
            }
        }
    }
}
