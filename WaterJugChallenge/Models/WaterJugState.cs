namespace WaterJugChallenge.Entities
{
    public class WaterJugState
    {
        public int Step { get; set; }
        public int BucketX { get; set; }
        public int BucketY { get; set; }
        public string? Action { get; set; }
        public string? Status { get; set; }
    }
}
