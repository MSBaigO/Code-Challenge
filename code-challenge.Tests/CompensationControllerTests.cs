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
    public class CompensationControllerTests
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
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "d304d950-533f-4b78-b9b0-faf907cce84b",
                Salary = Convert.ToDecimal(535246.23),
                EffectiveDate = Convert.ToDateTime("2/22/2022"),
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        [TestMethod]
        public void GetCompensationById_Returns_Ok()
        {
           // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedEffectiveDate = Convert.ToDateTime("2018-06-04T00:00:00");
            var expectedSalary = Convert.ToDecimal(1050601.56);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(expectedEffectiveDate, compensation.EffectiveDate);

        }

        [TestMethod]
        public void UpdateCompensation_Returns_Ok()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "62c1084e-6e34-4630-93fd-9153afb65309",
                Salary = Convert.ToDecimal(6346346.23),
                EffectiveDate = Convert.ToDateTime("2/15/2022")
            };
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var putRequestTask = _httpClient.PutAsync($"api/compensation/{compensation.EmployeeId}",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var putResponse = putRequestTask.Result;
            
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);
            var updatedCompensation = putResponse.DeserializeContent<Compensation>();

            Assert.AreEqual(compensation.Salary, updatedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, updatedCompensation.EffectiveDate);
        }

        [TestMethod]
        public void UpdateCompensation_Returns_NotFound()
        {
            // Arrange
            var compensation = new Compensation()
            {
                EmployeeId = "Invalid_Id",
                Salary = Convert.ToDecimal(12246.23),
                EffectiveDate = Convert.ToDateTime("2/1/2021"),
            };
            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PutAsync($"api/compensation/{compensation.EmployeeId}",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
