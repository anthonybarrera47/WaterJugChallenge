using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WaterJugChallenge.Entities;
using WaterJugChallenge.Services.Interface;

namespace WaterJugChallenge.Services
{
    public class WaterJugSolver : IWaterJugSolver
    {
        /// <summary>
        /// Solves the Water Jug problem using the BFS (Breadth-First Search) algorithm to find a sequence of steps to get the desired amount of water.
        /// </summary>
        /// <param name="xCapacity">The capacity of jug X.</param>
        /// <param name="yCapacity">The capacity of jug Y.</param>
        /// <param name="zAmountWanted">The desired amount of water to measure using the jugs.</param>
        /// <returns>A list of <see cref="WaterJugState"/> representing the steps to achieve the desired amount of water.
        /// If no solution is found, a single step with the action "No solution possible" is returned.</returns>
        public List<WaterJugState> GetSolution(int xCapacity, int yCapacity, int zAmountWanted)
        {
            var result = new List<WaterJugState>();
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int, int, List<WaterJugState>)>();

            queue.Enqueue((0, 0, new List<WaterJugState>()));

            while (queue.Count > 0)
            {
                var (currentX, currentY, steps) = queue.Dequeue();

                if (currentX == zAmountWanted || currentY == zAmountWanted)
                {
                    if (steps.Count > 0)
                    {
                        steps[^1].Status = "Solved";
                    }
                    return steps;
                }

                if (visited.Contains((currentX, currentY)))
                    continue;
                visited.Add((currentX, currentY));

                // Fill X
                if (currentX != xCapacity)
                {
                    var newSteps1 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = xCapacity, BucketY = currentY, Action = "Fill bucket X", Status = "Unsolved" }
                    };
                    if (!visited.Contains((xCapacity, currentY)))
                        queue.Enqueue((xCapacity, currentY, newSteps1));
                }

                // Fill Y
                if (currentY != yCapacity)
                {
                    var newSteps2 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = currentX, BucketY = yCapacity, Action = "Fill bucket Y", Status = "Unsolved" }
                    };
                    if (!visited.Contains((currentX, yCapacity)))
                        queue.Enqueue((currentX, yCapacity, newSteps2));
                }

                // Empty X
                if (currentX != 0)
                {
                    var newSteps3 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = 0, BucketY = currentY, Action = "Empty bucket X", Status = "Unsolved" }
                    };
                    if (!visited.Contains((0, currentY)))
                        queue.Enqueue((0, currentY, newSteps3));
                }

                // Empty Y
                if (currentY != 0)
                {
                    var newSteps4 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = currentX, BucketY = 0, Action = "Empty bucket Y", Status = "Unsolved" }
                    };
                    if (!visited.Contains((currentX, 0)))
                        queue.Enqueue((currentX, 0, newSteps4));
                }

                // Transfer X to Y
                var transferXToY = Math.Min(currentX, yCapacity - currentY);
                if (transferXToY > 0)
                {
                    var newSteps5 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = currentX - transferXToY, BucketY = currentY + transferXToY, Action = "Transfer from X to Y", Status = "Unsolved" }
                    };
                    if (!visited.Contains((currentX - transferXToY, currentY + transferXToY)))
                        queue.Enqueue((currentX - transferXToY, currentY + transferXToY, newSteps5));
                }

                // Transfer Y to X
                var transferYToX = Math.Min(currentY, xCapacity - currentX);
                if (transferYToX > 0)
                {
                    var newSteps6 = new List<WaterJugState>(steps)
                    {
                        new() { Step = steps.Count + 1, BucketX = currentX + transferYToX, BucketY = currentY - transferYToX, Action = "Transfer from Y to X", Status = "Unsolved" }
                    };

                    if (!visited.Contains((currentX + transferYToX, currentY - transferYToX)))
                        queue.Enqueue((currentX + transferYToX, currentY - transferYToX, newSteps6));
                }
            }
            return [new() { Step = 1, BucketX = 0, BucketY = 0, Action = "No solution possible", Status = "Unsolved" }];
        }
    }
}
