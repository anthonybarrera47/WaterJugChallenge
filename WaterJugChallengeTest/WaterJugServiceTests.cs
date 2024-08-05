using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterJugChallenge.Entities;
using WaterJugChallenge.Services;

namespace WaterJugChallengeTest
{
    public class WaterJugServiceTests
    {
        private readonly WaterJugSolver _waterJugService;

        public WaterJugServiceTests()
        {
            _waterJugService = new WaterJugSolver();
        }

        [Fact]
        public void GetSolution_ShouldReturnCorrectSolution_ForExample1()
        {
            // Arrange
            int xCapacity = 2;
            int yCapacity = 10;
            int zAmountWanted = 4;

            // Act
            var solution = _waterJugService.GetSolution(xCapacity, yCapacity, zAmountWanted);

            // Assert
            var expectedSolution = new List<WaterJugState>
            {
                new () { Step = 1, BucketX = 2, BucketY = 0, Action = "Fill bucket X", Status = "Unsolved" },
                new () { Step = 2, BucketX = 0, BucketY = 2, Action = "Transfer from X to Y", Status = "Unsolved" },
                new () { Step = 3, BucketX = 2, BucketY = 2, Action = "Fill bucket X", Status = "Unsolved" },
                new () { Step = 4, BucketX = 0, BucketY = 4, Action = "Transfer from X to Y", Status = "Solved" }
            };

            Assert.Equal(expectedSolution.Count, solution.Count);
            for (int i = 0; i < expectedSolution.Count; i++)
            {
                Assert.Equal(expectedSolution[i].Step, solution[i].Step);
                Assert.Equal(expectedSolution[i].BucketX, solution[i].BucketX);
                Assert.Equal(expectedSolution[i].BucketY, solution[i].BucketY);
                Assert.Equal(expectedSolution[i].Action, solution[i].Action);
                Assert.Equal(expectedSolution[i].Status, solution[i].Status);
            }
        }

        [Fact]
        public void GetSolution_ShouldReturnNoSolution_ForImpossibleCase()
        {
            // Arrange
            int xCapacity = 2;
            int yCapacity = 6;
            int zAmountWanted = 5;

            // Act
            var solution = _waterJugService.GetSolution(xCapacity, yCapacity, zAmountWanted);

            // Assert
            Assert.Single(solution);
            Assert.Equal(1, solution[0].Step);
            Assert.Equal(0, solution[0].BucketX);
            Assert.Equal(0, solution[0].BucketY);
            Assert.Equal("No solution possible", solution[0].Action);
            Assert.Equal("Unsolved", solution[0].Status);
        }
        [Fact]
        public void GetSolution_ShouldReturnCorrectSolution_ForExample2()
        {
            // Arrange
            int xCapacity = 2;
            int yCapacity = 100;
            int zAmountWanted = 96;

            // Act
            var solution = _waterJugService.GetSolution(xCapacity, yCapacity, zAmountWanted);

            // Assert
            // Verificar los pasos de la soluci�n para el caso (3, 5, 4)
            var expectedSolution = new List<WaterJugState>
            {
                new () { Step = 1, BucketX = 0, BucketY = 100, Action = "Fill bucket Y", Status = "Unsolved" },
                new () { Step = 2, BucketX = 2, BucketY = 98, Action = "Transfer from Y to X", Status = "Unsolved" },
                new () { Step = 3, BucketX = 0, BucketY = 98, Action = "Empty bucket X", Status = "Unsolved" },
                new () { Step = 4, BucketX = 2, BucketY = 96, Action = "Transfer from Y to X", Status = "Solved" }
            };

            Assert.Equal(expectedSolution.Count, solution.Count);
            for (int i = 0; i < expectedSolution.Count; i++)
            {
                Assert.Equal(expectedSolution[i].Step, solution[i].Step);
                Assert.Equal(expectedSolution[i].BucketX, solution[i].BucketX);
                Assert.Equal(expectedSolution[i].BucketY, solution[i].BucketY);
                Assert.Equal(expectedSolution[i].Action, solution[i].Action);
                Assert.Equal(expectedSolution[i].Status, solution[i].Status);
            }
        }
    }
}
