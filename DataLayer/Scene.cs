namespace DataLayer;

public class Scene
{
    public readonly int Width;
    public readonly int Height;
    public readonly List<Ball> Balls = new List<Ball>();

    public Scene(int width, int height, int ballRadius, int ballCount)
    {
        this.Width = width;
        this.Height = height;
        InitializeBalls(ballRadius, ballCount);
    }

    private void InitializeBalls(int ballRadius, int ballCount)
    {
        for (int i = 0; i < ballCount; i++)
        {
            AddBall(ballRadius);
        }
    }

    private void AddBall(int ballRadius)
    {
        int x, y;
        Random source = new Random();
        while (true)
        {
            x = source.Next(ballRadius, this.Width - ballRadius);
            y = source.Next(ballRadius, this.Height - ballRadius);
            int overlapCounter = 0;
            foreach (var ball in Balls)
            {
                double distanceBetweenCentres = Math.Sqrt(Math.Pow(ball.X - x, 2) + Math.Pow(ball.Y - y, 2));
                if (distanceBetweenCentres < ballRadius + ball.Radius)
                {
                    overlapCounter++;
                }
            }
            if (overlapCounter == 0)
            {
                this.Balls.Add(new Ball(x, y, ballRadius));
                return;
            }
        }
    }
}