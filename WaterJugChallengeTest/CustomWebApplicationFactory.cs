using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WaterJugChallenge.Entities;
using WaterJugChallenge.Services.Interface;

namespace WaterJugChallengeTest
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Mock IWaterJugSolver
                var mockSolver = new Mock<IWaterJugSolver>();
                mockSolver.Setup(s => s.GetSolution(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                          .Returns(new List<WaterJugState>
                          {
                              new WaterJugState { Step = 1, BucketX = 3, BucketY = 0, Action = "Fill bucket X", Status = "Unsolved" },
                              new WaterJugState { Step = 2, BucketX = 0, BucketY = 3, Action = "Transfer from X to Y", Status = "Solved" }
                          });

                // Replace the service
                services.AddSingleton<IWaterJugSolver>(mockSolver.Object);
            });
        }
    }
}