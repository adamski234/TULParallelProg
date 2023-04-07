namespace DataLayer;

public abstract class AbstractDataApi
{
    public abstract List<Ball> GetBalls();

    public static AbstractDataApi CreateApi(int width, int height, int ballCount, int ballRadius)
    {
        return new DataApi(width, height, ballCount, ballRadius);
    }
    internal class DataApi : AbstractDataApi
    {
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