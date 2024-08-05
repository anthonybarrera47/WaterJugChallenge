using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WaterJugChallenge.Entities;

namespace WaterJugChallengeTest
{
    public class WaterJugControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public WaterJugControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task Post_Solve_ReturnsSolution()
        {
            // Arrange
            var request = new WaterJugRequest { XCapacity = 3, YCapacity = 5, ZAmountWanted = 4 };

            // Act
            var response = await _client.PostAsJsonAsync("/WaterJug/GetSolution", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var solutionResponse = await response.Content.ReadFromJsonAsync<SolutionResponse>();
            Assert.NotNull(solutionResponse);
            Assert.NotNull(solutionResponse!.Solution);
            Assert.True(solutionResponse.Solution.Count > 0);
            Assert.Equal("Solved", solutionResponse.Solution[^1].Status);
        }
        private class SolutionResponse
        {
            public List<WaterJugState> Solution { get; set; }
        }
    }
}
