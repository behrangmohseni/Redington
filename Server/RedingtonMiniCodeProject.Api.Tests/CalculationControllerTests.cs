using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RedingtonMiniCodeProject.Api.Tests
{
    public class CalculationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public CalculationControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
        [Fact]
        public async Task CalculationControllerTests_GetAllCalculationTypes_ReturnsSuccessful()
        {
            var response = await _client.GetAsync("/api/calculation/types");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = JsonConvert.DeserializeObject<string[]>(await response.Content.ReadAsStringAsync());
            Assert.Equal("CombinedWith", result[0]);
            Assert.Equal("Either", result[1]);
        }

        [Theory]
        [InlineData("CombinedWith", 0.5, 0.5, 0.25)]
        [InlineData("Either", 0.5, 0.5, 0.75)]
        public async Task Calculate_ReturnsSuccessful(string method, double input1, double input2, double result)
        {
            var response = await _client.GetAsync($"/api/calculation/calc/{method}/{input1}/{input2}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var res = JsonConvert.DeserializeObject<CalculationResult>(await response.Content.ReadAsStringAsync());
            Assert.True(res.IsValid);
            Assert.Equal(result, res.Result.Value);
        }

        [Theory]
        [InlineData("InvalidMethod123")]
        public async Task Calculate_InvalidMethodName_Returns400(string method)
        {
            var response = await _client.GetAsync($"/api/calculation/calc/{method}/{0.5}/{0.5}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("CombinedWith", -0.5, 0.5)]
        [InlineData("CombinedWith", 0.5, -0.5)]
        [InlineData("CombinedWith", 2, 0.5)]
        [InlineData("CombinedWith", 0.5, 2)]
        [InlineData("CombinedWith", 1, -1)]
        [InlineData("CombinedWith", -1, 1)]
        [InlineData("Either", -0.5, 0.5)]
        [InlineData("Either", 0.5, -0.5)]
        [InlineData("Either", 2, 0.5)]
        [InlineData("Either", 0.5, 2)]
        [InlineData("Either", 1, -1)]
        [InlineData("Either", -1, 1)]
        public async Task Calculate_InvalidInputs_Returns400(string method, double input1, double input2)
        {
            var response = await _client.GetAsync($"/api/calculation/calc/{method}/{input1}/{input2}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public class CalculationResultValue
        {
            public double Value { get; set; }
        }

        public class CalculationResult
        {
            public CalculationResultValue Result { get; set; }
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }

            public CalculationResult()
            {
            }

            public CalculationResult(double value)
            {
                Result = new CalculationResultValue() { Value = value };
                IsValid = true;
                ErrorMessage = string.Empty;
            }

            public CalculationResult(Exception ex)
            {
                IsValid = false;
                ErrorMessage = ex.Message;
            }
        }
    }

}
