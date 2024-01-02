using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Versioning;

namespace Extractor.Tests
{
    [TestClass()]
    public class ExtractorTests
    {

        [TestMethod()]
        public void Extractor_Should_Find_Database_Extension()
        {
            string mockedDatabasePath = "test.mdb";

            string mockedDatabaseExtension = Extractor.FindDatabaseExtension(mockedDatabasePath);

            string expectedExtension = ".mdb";

            Assert.AreEqual(expectedExtension, mockedDatabaseExtension);
        }

        [TestMethod()]
        public void Extractor_Should_Find_Connection_Format()
        {
            string mockedDatabasePath = "test.mdb";

            string mockedDatabaseExtension = Extractor.FindDatabaseExtension(mockedDatabasePath);

            Extractor.ConnectionType mockedConnectionType = Extractor.FindFormat(mockedDatabaseExtension);

            Extractor.ConnectionType expectedConnectionType = Extractor.ConnectionType.OldAccess;

            Assert.AreEqual(expectedConnectionType, mockedConnectionType);
        }

        [SupportedOSPlatform("windows")]
        [TestMethod()]
        public void Connector_Should_Populate_Connection_String()
        {
            string mockedDatabasePath = @"D:\Exports\FISIMED_Broder\bd\fisimed.mdb";

            string mockedDatabaseExtension = Extractor.FindDatabaseExtension(mockedDatabasePath);

            Extractor.ConnectionType mockedConnectionType = Extractor.FindFormat(mockedDatabaseExtension);

            Connector connector = new(mockedConnectionType, mockedDatabasePath);

            string expectedConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Exports\FISIMED_Broder\bd\fisimed.mdb";

            Assert.AreEqual(expectedConnectionString, connector.ConnectionString);
        }


        [SupportedOSPlatform("windows")]
        [TestMethod()]
        public void Connector_Should_Handle_Connection_When_Args_Correct()
        {
            string mockedDatabasePath = @"D:\Exports\FISIMED_Broder\bd\fisimed.mdb";

            string mockedDatabaseExtension = Extractor.FindDatabaseExtension(mockedDatabasePath);

            Extractor.ConnectionType mockedConnectionType = Extractor.FindFormat(mockedDatabaseExtension);

            Connector connector = new(mockedConnectionType, mockedDatabasePath);

            using (connector.Connection)
            {
                Assert.IsTrue(connector.Connection.State == ConnectionState.Open);
            }
            Assert.IsTrue(connector.Connection.State == ConnectionState.Closed);
        }

        [SupportedOSPlatform("windows")]
        [TestMethod()]
        public void Connector_Should_Throw_Exception_When_Args_Incorrect()
        {

            try
            {
                Connector connector = new(Extractor.ConnectionType.OldAccess, "hello");
                Assert.Fail("No exception thrown.");
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception is OleDbException);
            }
        }
    }
}