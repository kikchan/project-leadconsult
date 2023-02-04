using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using project_leadconsult_core.BC;
using project_leadconsult_core.BE;
using project_leadconsult_core.Enums;
using System;
using System.IO;

namespace project_leadconsult_core_tests
{
    /// <summary>
    /// TestAutofac
    /// </summary>
    [TestClass]
    public class TestAutofac
    {
        /// <summary>
        /// The correlation identifier
        /// </summary>
        private Guid CorrelationID;

        /// <summary>
        /// The data folder
        /// </summary>
        private const string DataFolder = "../../../Data/";

        /// <summary>
        /// Tests the mock.
        /// </summary>
        [TestMethod]
        public void TestMock()
        {
            var mock = new Mock<ICoordinatesBC>();
            mock.Setup(m => m.ProcessFile(null)).Returns(new ProcessFileResponse() { Response = ResponseStatuses.OK });
            
            ProcessFileRequest processFileRequest = new ProcessFileRequest(CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesKONonExistantFile.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse processFileResponse = mock.Object.ProcessFile(null);

            Assert.AreEqual(ResponseStatuses.OK, processFileResponse.Response);
            Assert.IsNull(processFileResponse.ResponseMessage);
            Assert.IsNull(processFileResponse.FurthestPointsFromCenter);
        }

        /// <summary>
        /// Tests the mock.
        /// </summary>
        [TestMethod]
        public void TestMock2()
        {
            ProcessFileRequest processFileRequest = new ProcessFileRequest(CorrelationID)
            {
                FileName = Path.Combine(DataFolder, "coordinatesOK.txt"),
                Target = "out.txt"
            };

            ProcessFileResponse mockedResponse = new ProcessFileResponse()
            {
                Response = ResponseStatuses.NoDataFound,
                ResponseMessage = "Hello, I'm a mocked response messaged that's impossible to obtain through the normal execution of the code!"
            };

            var mock = new Mock<ICoordinatesBC>();
            mock.Setup(m => m.ProcessFile(processFileRequest)).Returns(mockedResponse);


            ProcessFileResponse processFileResponse = mock.Object.ProcessFile(processFileRequest);

            Assert.AreEqual(ResponseStatuses.NoDataFound, processFileResponse.Response);
            Assert.AreEqual("Hello, I'm a mocked response messaged that's impossible to obtain through the normal execution of the code!", processFileResponse.ResponseMessage);
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