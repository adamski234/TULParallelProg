using DataLayer;
using System.Diagnostics;

namespace LogicLayer;

public class BallLogic
{
	private Ball ball;
	private object mutex;
	private Thread runningThread;
	private bool isEnabled = true;
	private int sceneWidth;
	private int sceneHeight;
	public BallLogic(Ball input, ref object mutexObject, int width, int height)
	{
		this.sceneHeight = height;
		this.sceneWidth = width;
		this.ball = input;
		this.mutex = mutexObject;
		this.runningThread = new Thread(() =>
		{
			while (isEnabled)
			{
				if (!((this.ball.X + this.ball.speedX) > 0 &&
				      (this.ball.X + this.ball.speedX) < (sceneWidth - this.ball.Radius)))
				{
					Trace.WriteLine($"x {sceneWidth} {this.ball.X}");
					this.ball.speedX = -this.ball.speedX;
				}
				if (!((this.ball.Y + this.ball.speedY) > 0 &&
				      (this.ball.Y + this.ball.speedY) < (sceneHeight - this.ball.Radius)))
				{
					Trace.WriteLine($"y {sceneHeight} {this.ball.Y}");
					this.ball.speedY = -this.ball.speedY;
				}
				lock (mutex)
				{
					this.ball.X += this.ball.speedX;
					this.ball.Y += this.ball.speedY;
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