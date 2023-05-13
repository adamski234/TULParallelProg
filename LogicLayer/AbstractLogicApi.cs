
using System.ComponentModel;
using System.Diagnostics;
using DataLayer;

namespace LogicLayer
{
	public abstract class AbstractLogicApi
	{
		public abstract List<Ball> GetBalls();
		public abstract void Enable();
		public abstract void Disable();
		public abstract bool IsEnabled();
		private object modifierLock = new();

		public static AbstractLogicApi CreateApi(int width, int height, int ballCount, int ballRadius)
		{
			return new LogicApi(width, height, ballCount, ballRadius);
		}
		internal class LogicApi : AbstractLogicApi
		{
			private AbstractDataApi data;
			private bool enabled = false;
			private List<BallLogic> logicList = new List<BallLogic>();
			public LogicApi(int width, int height, int ballCount, int ballRadius)
			{
				this.data = AbstractDataApi.CreateApi(width, height, ballCount, ballRadius);
				lock (modifierLock)
				{
					foreach (var ball in this.GetBalls())
					{
						BallLogic logic = new BallLogic(ball);
						logic.PropertyChanged += Update;
						logicList.Add(logic);
						logic.Start();
					}
				}
			}
			public override List<Ball> GetBalls()
			{
				return this.data.GetBalls();
			}

			public override void Enable()
			{
				this.enabled = true;
				lock (modifierLock)
				{
					foreach (var logic in this.logicList)
					{
						logic.Start();
					}
				}
			}

			public override void Disable()
			{
				this.enabled = false;
				lock (modifierLock)
				{
					foreach (var logic in logicList)
					{
						logic.Stop();
					}
				}
			}

			public override bool IsEnabled()
			{
				return this.enabled;
			}

			private void Update(object? sender, PropertyChangedEventArgs args)
			{
				// This is always called in events that are called with a locked mutex
				// therefore there is no need to lock

				Ball thisObject = (Ball)sender;
				// check for collision between balls
				lock (modifierLock)
				{
					this.data.GetBalls().Sort((left, right) => left.X.CompareTo(right.X));
				}
				// logicList is sorted by X coord

				if (args.PropertyName == "Position") // this is stupid 
				{
					foreach (var ball in data.GetBalls())
					{
						// Only check until first ball that is further away than X + Radius
						//if (ball.X + ball.Radius > thisObject.X) break;
						if (ReferenceEquals(ball, thisObject)) continue;
						double distanceBetweenCenters =
							Math.Sqrt(Math.Pow(ball.X - thisObject.X, 2) + Math.Pow(ball.Y - thisObject.Y, 2));
						double minimalCollisionDistance =
							thisObject.Radius / 2 +
							ball.Radius / 2; // radius is actually diameter so it needs to be halved
						if (distanceBetweenCenters <= minimalCollisionDistance)
						{
							double dx = ball.X - thisObject.X;
							double dy = ball.Y - thisObject.Y;

							// normal vectors
							double nx = dx / distanceBetweenCenters;
							double ny = dy / distanceBetweenCenters;

							// dot product of the velocity and the tangent vector
							double ballTan = ball.speedX * -ny + ball.speedY * nx;
							double thisObjectTan = thisObject.speedX * -ny + thisObject.speedY * nx;

							// the balls are massless so mass is const = 10 
							double newBallSpeed = (ball.speedX * nx + ball.speedY * ny + 2 * 10 * (thisObject.speedX * nx + thisObject.speedY * ny)) / (2 * 10);
							double newThisObjectSpeed = (thisObject.speedX * nx + thisObject.speedY * ny + 2 * 10 * (ball.speedX * nx + ball.speedY * ny)) / (2 * 10);

							ball.speedX = newBallSpeed * nx + ballTan * -ny;
							ball.speedY = newBallSpeed * ny + ballTan * nx;
							thisObject.speedX = newThisObjectSpeed * nx + thisObjectTan * -ny;
							thisObject.speedY = newThisObjectSpeed * ny + thisObjectTan * nx;

							// move one of the balls to stop being in a state of collision
							ball.X += (minimalCollisionDistance - distanceBetweenCenters) * dx / distanceBetweenCenters;
							ball.Y += (minimalCollisionDistance - distanceBetweenCenters) * dy / distanceBetweenCenters;

							return;
						}
					}
				}


				// check for collision against walls
				if (args.PropertyName == "X")
				{
					if (!(thisObject.X > 0 && thisObject.X < (data.GetScene().Width - thisObject.Radius)))
					{
						thisObject.speedX = -thisObject.speedX;
					}
				}

				if (args.PropertyName == "Y")
				{
					if (!(thisObject.Y > 0 && thisObject.Y < (data.GetScene().Height - thisObject.Radius)))
					{
						thisObject.speedY = -thisObject.speedY;
					}
				}
			}
		}
	}
}