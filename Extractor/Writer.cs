using System.Runtime.Serialization.Formatters;

namespace Extractor
{
    public class Writer
    {
        // Properties (a Writer has a success state).
        public bool IsSuccessful { get; set; }

        public Writer(List<(string, int, List<string>, List<string>)> loaderData)
        {
            try
            {
                WriteSchema(loaderData);
            }
            catch (Exception exception)
            {
                // Catch and wrap.
                throw new Exception($"Formatting error here: {exception.StackTrace}", exception);
            }
        }

        public void WriteSchema(List<(string, int, List<string>, List<string>)> loaderData)
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\steven.jimenez\Downloads\output.txt"))
            {

                writer.WriteLine($"LOG DATE: {DateTime.Now}");
                //writer.WriteLine($"DB PATH: {Extractor.DatabasePath}");
                writer.WriteLine($"TABLE TOTAL (non-empty): {loaderData.Count}");
                writer.WriteLine("--------------------------------------\n");

                foreach (var item in loaderData)
                {
                    writer.WriteLine($"[{item.Item1}][{item.Item2}]");
                    writer.WriteLine($"---");

                    for (int i = 0; i < item.Item3.Count; i++)
                    {
                        writer.WriteLine($"{item.Item3[i]}: {item.Item4[i]}");
                    }

                    writer.WriteLine($"\n--------------------\n");
                }

                IsSuccessful = true;
            }
        }
    }
}
