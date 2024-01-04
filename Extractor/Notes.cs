using System;
using System.Data.OleDb;
using System.Data;

public class Class1
{
    //public Class1()
    //{
    //    // Properties(a loader has data).
    //    public List<(string, int, List<string>, List<string>)> Schema { get; set; }

    //// Constructor.
    //public Retriever(OleDbConnection connection, Extractor.ConnectionType connectionType)
    //{
    //    Schema = new List<(string, int, List<string>, List<string>)>();

    //    using (connection)
    //    {
    //        try
    //        {
    //            Schema = GetSchema(connection, connectionType);
    //        }
    //        catch (Exception exception)
    //        {
    //            // Catch and wrap.
    //            throw new Exception($"Loading error here: {exception.StackTrace}", exception);
    //        }
    //    }
    //}

    //public List<(string, int, List<string>, List<string>)> GetSchema(OleDbConnection connection, Extractor.ConnectionType connectionType)
    //{
    //    switch (connectionType)
    //    {
    //        case Extractor.ConnectionType.OldAccess:

    //            // All tables.
    //            DataTable schemaDatabase = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
    //                new object[] { null, null, null, "TABLE" });

    //            foreach (DataRow table in schemaDatabase.Rows)
    //            {
    //                // Individual table.
    //                string tableName = table["TABLE_NAME"].ToString();

    //                // Individual table columns.
    //                DataTable columns = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
    //                    new object[] { null, null, tableName, null });

    //                List<string> columnNames = new List<string>();
    //                List<string> columnExamples = new List<string>();

    //                // Individual table data (rows).
    //                string query = $"SELECT COUNT(*) FROM [{tableName}]";
    //                OleDbCommand command = new OleDbCommand(query, connection);
    //                int dataCount = (int)command.ExecuteScalar();

    //                // If no data, terminate.
    //                if (dataCount < 1)
    //                {
    //                    continue;
    //                }

    //                // If data, log column.
    //                foreach (DataRow column in columns.Rows)
    //                {
    //                    string columnName = column["COLUMN_NAME"].ToString();
    //                    columnNames.Add($"[{columnName}]");

    //                    query = $"SELECT TOP 1 * FROM [{tableName}]";
    //                    command = new OleDbCommand(query, connection);
    //                    OleDbDataReader reader = command.ExecuteReader();

    //                    while (reader.Read())
    //                    {
    //                        string? example = reader[$"{columnName}"].ToString();
    //                        columnExamples.Add(example);
    //                    }

    //                }

    //                int columnCount = columns.Rows.Count;

    //                Schema.Add((tableName, columnCount, columnNames, columnExamples));
    //            }
    //            break;

    //        case Extractor.ConnectionType.SqlServer:
    //            throw new NotImplementedException();
    //    }

    //    return Schema;
    //}

    // Properties (a formatter has formatted data).
    //public List<(string, int, List<string>, List<string>)> FormattedData { get; set; }

    //public Formatter(List<(string, int, List<string>, List<string>)> data)
    //{
    //    FormattedData = new();

    //    try
    //    {
    //        FormattedData = FormatData(data);
    //    }
    //    catch (Exception exception)
    //    {
    //        // Catch and wrap.
    //        throw new Exception($"Formatting error here: {exception.StackTrace}", exception);
    //    }
    //}

    //public List<(string, int, List<string>, List<string>)> FormatData(List<(string, int, List<string>, List<string>)> data)
    //{
    //    //foreach (string e in data)
    //    //{
    //    //    char[] stringArray = e.ToCharArray();
    //    //    FormattedData.Add(string.Join("", stringArray.Reverse()));
    //    //}

    //    return FormattedData;
    //}

    // Properties (a Writer has a success state).
    //public bool IsSuccessful { get; set; }

    //public Writer(List<(string, int, List<string>, List<string>)> loaderData)
    //{
    //    try
    //    {
    //        WriteSchema(loaderData);
    //    }
    //    catch (Exception exception)
    //    {
    //        // Catch and wrap.
    //        throw new Exception($"Formatting error here: {exception.StackTrace}", exception);
    //    }
    //}

    //public void WriteSchema(List<(string, int, List<string>, List<string>)> loaderData)
    //{
    //    using (StreamWriter writer = new StreamWriter(@"C:\Users\steven.jimenez\Downloads\output.txt"))
    //    {

    //        writer.WriteLine($"LOG DATE: {DateTime.Now}");
    //        //writer.WriteLine($"DB PATH: {Extractor.DatabasePath}");
    //        writer.WriteLine($"TABLE TOTAL (non-empty): {loaderData.Count}");
    //        writer.WriteLine("--------------------------------------\n");

    //        foreach (var item in loaderData)
    //        {
    //            writer.WriteLine($"[{item.Item1}][{item.Item2}]");
    //            writer.WriteLine($"---");

    //            for (int i = 0; i < item.Item3.Count; i++)
    //            {
    //                writer.WriteLine($"{item.Item3[i]}: {item.Item4[i]}");
    //            }

    //            writer.WriteLine($"\n--------------------\n");
    //        }

    //        IsSuccessful = true;
    //    }
    //}
}
