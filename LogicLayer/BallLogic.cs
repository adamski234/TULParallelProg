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
				Random source = new Random();
				int moveX, moveY;
				do
				{
					moveX = source.Next(-5, 5);
					moveY = source.Next(-5, 5);
					//Trace.WriteLine($"{moveX} {moveY} {ball.X} {ball.Y} {sceneHeight} {sceneWidth}");
				} while (!(
					(this.ball.X + moveX) > 0 && (this.ball.X + moveX) < (sceneWidth - this.ball.Radius) &&
					(this.ball.Y + moveY) > 0 && (this.ball.Y + moveY) < (sceneHeight - this.ball.Radius)
				));
				lock (mutex)
				{
					this.ball.X += moveX;
					this.ball.Y += moveY;
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