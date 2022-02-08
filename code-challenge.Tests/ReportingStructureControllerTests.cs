using challenge.Controllers;
using challenge.Data;
using challenge.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using code_challenge.Tests.Integration.Extensions;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using code_challenge.Tests.Integration.Helpers;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetDirectReportsById_Returns_NotFound()
        {
            // Arrange
            var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309";
            var expectedNumberOfReports = 0;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedNumberOfReports, reportingStructure.NumberOfReports);
        }

    }
}
