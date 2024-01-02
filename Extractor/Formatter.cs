namespace Extractor
{
    public class Formatter
    {
        // Properties (a formatter has formatted data).
        public List<(string, int, List<string>, List<string>)> FormattedData { get; set; }

        public Formatter(List<(string, int, List<string>, List<string>)> data)
        {
            FormattedData = new();

            try
            {
                FormattedData = FormatData(data);
            }
            catch (Exception exception)
            {
                // Catch and wrap.
                throw new Exception($"Formatting error here: {exception.StackTrace}", exception);
            }
        }

        public List<(string, int, List<string>, List<string>)> FormatData(List<(string, int, List<string>, List<string>)> data)
        {
            //foreach (string e in data)
            //{
            //    char[] stringArray = e.ToCharArray();
            //    FormattedData.Add(string.Join("", stringArray.Reverse()));
            //}

            return FormattedData;
        }
    }
}
