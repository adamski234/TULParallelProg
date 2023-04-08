using DataLayer;

namespace LogicLayer;

public class BallLogic
{
    private Ball ball;
    private object mutex;
    private Thread runningThread;
    private bool isEnabled = true;
    public BallLogic(Ball input, ref object mutexObject)
    {
        this.ball = input;
        this.mutex = mutexObject;
        this.runningThread = new Thread(() =>
        {
            while (isEnabled)
            {
                Random source = new Random();
                lock (mutex)
                {
                    this.ball.X += source.Next(-5, 5);
                    this.ball.Y += source.Next(-5, 5);
                }
                Thread.Sleep(50); // 20 tps tickrate
            }
        });
        this.runningThread.Start();
    }
    public void Stop()
    {
        this.isEnabled = false;
    }
}