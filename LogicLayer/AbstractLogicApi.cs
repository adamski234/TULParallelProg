
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
							// we have a collision
							// Placeholder inversion of direction
							// TODO actually implement algorithm
							double nx = (ball.X - thisObject.X) / distanceBetweenCenters;
							double ny = (ball.Y - thisObject.Y) / distanceBetweenCenters;
							double p = 2 * (thisObject.speedX * nx + thisObject.speedY * ny - ball.speedX * nx + ball.speedY * ny) / 20;

							thisObject.speedX = thisObject.speedX - p * 10 * nx;
							thisObject.speedY = thisObject.speedY - p * 10 * ny;
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