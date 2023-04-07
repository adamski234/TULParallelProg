namespace DataLayer;

public abstract class AbstractDataApi
{
    public abstract List<Ball> GetBalls();
    internal class DataApi : AbstractDataApi
    {
        private readonly object mutex = new object();
        public bool enabled = true;
        public readonly Scene scene;
        public DataApi(int width, int height, int ballCount, int ballRadius)
        {
            this.scene = new Scene(width, height, ballRadius, ballCount);
        }
        public override List<Ball> GetBalls()
        {
            return scene.Balls;
        }
    }
}