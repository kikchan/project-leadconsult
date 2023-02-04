using Microsoft.VisualStudio.TestTools.UnitTesting;
using project_leadconsult_core.BC;
using project_leadconsult_core.BE;
using project_leadconsult_core.Enums;
using Serilog;
using System;
using System.IO;

namespace project_leadconsult_core_tests
{
    /// <summary>
    /// TestBC
    /// </summary>
    [TestClass]
    public class TestBC
    {
        /// <summary>
        /// The data folder
        /// </summary>
        private const string DataFolder = "../../../Data/";

        /// <summary>
        /// The bc
        /// </summary>
        private ICoordinatesBC bc = new CoordinatesBC();

        /// <summary>
        /// The correlation identifier
        /// </summary>
        private Guid CorrelationID;

        /// <summary>
        /// Classes the setup.
        /// </summary>
        /// <param name="testContext">The test context.</param>
        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        [ClassCleanup]
        public static void ClassCleaup()
        {
            Log.CloseAndFlush();
        }

        /// <summary>
        /// Tests the ok.
        /// </summary>
        [TestMethod]
        public void TestOK()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(this.CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesOK.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = bc.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.OK, processFileResponse.Response);
            Assert.AreEqual(5, processFileResponse.FurthestPointsFromCenter.Count);
        }

        /// <summary>
        /// Tests the missing y.
        /// </summary>
        [TestMethod]
        public void TestKOMissingY()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(this.CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesKO(missing Y).txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = bc.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.NoDataFound, processFileResponse.Response);
            Assert.AreEqual(ResponseStatuses.NoDataFound.ToString(), processFileResponse.ResponseMessage);
            Assert.IsNull(processFileResponse.FurthestPointsFromCenter);
        }

        /// <summary>
        /// Tests the ko empty file.
        /// </summary>
        [TestMethod]
        public void TestKOEmptyFile()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(this.CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesKOEmptyFile.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = bc.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.NoDataFound, processFileResponse.Response);
            Assert.AreEqual(ResponseStatuses.NoDataFound.ToString(), processFileResponse.ResponseMessage);
            Assert.IsNull(processFileResponse.FurthestPointsFromCenter);
        }

        /// <summary>
        /// Tests the ko trash.
        /// </summary>
        [TestMethod]
        public void TestKOTrash()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(this.CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesKOTrash.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = bc.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.NoDataFound, processFileResponse.Response);
            Assert.AreEqual(ResponseStatuses.NoDataFound.ToString(), processFileResponse.ResponseMessage);
            Assert.IsNull(processFileResponse.FurthestPointsFromCenter);
        }

        [TestMethod]
        public void TestKOFileDoesNotExist()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(this.CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesKONonExistantFile.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = bc.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.FileDoesntExist, processFileResponse.Response);
            Assert.AreEqual(ResponseStatuses.FileDoesntExist.ToString(), processFileResponse.ResponseMessage);
            Assert.IsNull(processFileResponse.FurthestPointsFromCenter);
        }

        /// <summary>
        /// Tests the setup.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            this.CorrelationID = Guid.NewGuid();
        }
    }
}