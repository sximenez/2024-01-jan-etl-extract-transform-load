using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.Odbc;
using System.Runtime.Versioning;

namespace Extractor.Tests
{
    [SupportedOSPlatform("windows")]
    [TestClass()]
    public class ExtractorTests
    {

        #region Init

        private string mockedDatabasePath = string.Empty;
        private string mockedQuery = string.Empty;
        private string mockedOutputPath = string.Empty;

        [TestInitialize()]
        public void Init()
        {
            mockedDatabasePath = @"C:\Users\steven.jimenez\source\repos\2024-01-jan-etl-extract-transform-load\Input.xls";
            mockedQuery = "SELECT * FROM [Sheet1$]";
            mockedOutputPath = @"C:\Users\steven.jimenez\source\repos\2024-01-jan-etl-extract-transform-load\Output.xlsx";
        }

        #endregion Init

        #region Connector

        public static bool DoesFileExist(string path)
        {
            return File.Exists(path);
        }

        [TestMethod()]
        public void Check_If_File_Exists()
        {
            Assert.IsTrue(DoesFileExist(mockedDatabasePath));
            
            Console.WriteLine(mockedDatabasePath);
        }

        [TestMethod()]
        public void Connector_Should_Populate_Connection_String_When_File_Exists()
        {

            if (DoesFileExist(mockedDatabasePath))
            {
                string expectedConnectionString = $@"Driver=Microsoft Excel Driver (*.xls);DBQ=C:\Users\steven.jimenez\source\repos\2024-01-jan-etl-extract-transform-load\Input.xls;";

                Connector connector = new Connector();
                connector.SetConnectionString(mockedDatabasePath);
                Assert.AreEqual(expectedConnectionString, connector.ConnectionString);
                
                Console.WriteLine(connector.ConnectionString);
            }
            else
            {
                Assert.Fail("File doesn't exist.");
            }
        }

        [TestMethod()]
        public void Connector_Should_Establish_Connection_When_String_Populated()
        {

            if (DoesFileExist(mockedDatabasePath))
            {
                Connector connector = new Connector();
                connector.SetConnectionString(mockedDatabasePath);
                connector.SetConnection();

                using (connector.Connection)
                {
                    connector.OpenConnection();
                    Assert.IsTrue(connector.Connection.State == ConnectionState.Open);

                    Console.WriteLine(connector.Connection.State);
                }
                Assert.IsTrue(connector.Connection.State == ConnectionState.Closed);
            }
            else
            {
                Assert.Fail("File doesn't exist.");
            }
        }

        #endregion Connector

        #region Retriever
        public static bool DoesQueryExist(string query)
        {
            return query != string.Empty;
        }

        [TestMethod()]
        public void Check_If_Query_Exists()
        {
            Assert.IsTrue(DoesQueryExist(mockedQuery));

            Console.WriteLine(mockedQuery);
        }

        [TestMethod()]
        public void Retriever_Should_Execute_Command_When_Query_Exists()
        {

            if (DoesFileExist(mockedDatabasePath) && DoesQueryExist(mockedQuery))
            {
                Connector connector = new Connector();
                connector.SetConnectionString(mockedDatabasePath);
                connector.SetConnection();

                Retriever retriever = new Retriever();

                using (connector.Connection)
                {
                    connector.OpenConnection();
                    retriever.GetData(connector.Connection, mockedQuery);
                    Console.WriteLine(string.Join("\n", retriever.Data));
                }
            }
            else
            {
                Assert.Fail("File doesn't exist.");
            }
        }

        #endregion

        #region Formatter

        [TestMethod()]
        public void Formatter_Should_Apply_Changes_When_Input_Exists()
        {

            if (DoesFileExist(mockedDatabasePath) && DoesQueryExist(mockedQuery))
            {
                Connector connector = new Connector();
                connector.SetConnectionString(mockedDatabasePath);
                connector.SetConnection();

                Retriever retriever = new Retriever();

                using (connector.Connection)
                {
                    connector.OpenConnection();
                    retriever.GetData(connector.Connection, mockedQuery);
                }

                Formatter formatter = new Formatter();
                formatter.FormatData(retriever.Data, retriever.NumberOfColumns);

                Console.WriteLine(string.Join("\n", formatter.FormattedData));
            }
            else
            {
                Assert.Fail("File doesn't exist.");
            }
        }

        #endregion Formatter

        #region Writer

        [TestMethod()]
        public void Writer_Should_Create_New_File_When_Formatted_Data_Exists()
        {

            if (DoesFileExist(mockedDatabasePath) && DoesQueryExist(mockedQuery))
            {
                Connector connector = new Connector();
                connector.SetConnectionString(mockedDatabasePath);
                connector.SetConnection();

                Retriever retriever = new Retriever();

                using (connector.Connection)
                {
                    connector.OpenConnection();
                    retriever.GetData(connector.Connection, mockedQuery);
                }

                Formatter formatter = new Formatter();
                formatter.FormatData(retriever.Data, retriever.NumberOfColumns);

                Writer writer = new Writer();
                writer.WriteFile(mockedOutputPath, retriever.Headers, formatter.FormattedData, retriever.NumberOfColumns);

                Assert.IsTrue(writer.HasWrittenFile);
            }

            else
            {
                Assert.Fail("File doesn't exist.");
            }
        }

        #endregion Writer
    }
}