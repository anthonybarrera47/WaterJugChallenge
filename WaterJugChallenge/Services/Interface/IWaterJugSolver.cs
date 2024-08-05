using WaterJugChallenge.Entities;

namespace WaterJugChallenge.Services.Interface
{
    public interface IWaterJugSolver
    {
        public List<WaterJugState> GetSolution(int xCapacity, int yCapacity, int zAmountWanted);
    }
}
