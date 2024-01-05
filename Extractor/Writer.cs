using OfficeOpenXml;

namespace Extractor
{
    public class Writer
    {
        // Properties.
        public bool HasWrittenFile { get; set; }

        // Constructor.
        public Writer()
        {
            HasWrittenFile = false;
        }

        public void WriteFile(string outputPath, List<string> headers, List<object> formattedData, int numberOfColumns)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo outputFile = new FileInfo(outputPath);

            if (outputFile.Exists)
            {
                outputFile.Delete();
            }

            using (ExcelPackage package = new ExcelPackage(outputFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                int row;
                int col;
                
                for (int i = 0; i < headers.Count; i++)
                {
                    row = i / numberOfColumns + 1;
                    col = i % numberOfColumns + 1;
                    worksheet.Cells[row, col].Value = headers[i];
                }

                for (int i = 0; i < formattedData.Count; i++)
                {
                    row = i / numberOfColumns + 2;
                    col = i % numberOfColumns + 1;
                    worksheet.Cells[row, col].Value = formattedData[i];
                }

                package.Save();
                HasWrittenFile = true;
            }
        }
    }
}
