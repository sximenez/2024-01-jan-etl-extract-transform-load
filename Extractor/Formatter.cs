namespace Extractor
{
    public class Formatter
    {
        // Properties.
        public List<string> FormattedData { get; set; }

        // Constructor.
        public Formatter()
        {
            FormattedData = new List<string>();
        }

        public void FormatData(List<string> data, int numberOfColumns)
        {
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (i % numberOfColumns == 1)
                    {
                        var words = data[i].Split(' ')
                            .Select(word => word.ToLower())
                            .Select(word => new string(word.Reverse().ToArray()))
                            .Select(word => char.ToUpper(word[0]) + word[1..]);

                        var result = string.Join(" ", words);
                        FormattedData.Add(result);
                    }

                    else
                    {
                        FormattedData.Add(data[i]);
                    }
                }
            }
        }
    }
}
