
using DataLayer;

namespace LogicLayer
{
	public abstract class AbstractLogicApi
	{
		public abstract List<Ball> GetBalls();
		public abstract void Enable();
		public abstract void Disable();
		public abstract bool IsEnabled();

		public static AbstractLogicApi CreateApi(int width, int height, int ballCount, int ballRadius)
		{
			return new LogicApi(width, height, ballCount, ballRadius);
		}
		internal class LogicApi : AbstractLogicApi
		{
			private AbstractDataApi data;
			private object mutex = new object();
			private bool enabled = false;
			private List<BallLogic> logicList = new List<BallLogic>();
			public LogicApi(int width, int height, int ballCount, int ballRadius)
			{
				this.data = AbstractDataApi.CreateApi(width, height, ballCount, ballRadius);
			}
			public override List<Ball> GetBalls()
			{
				return this.data.GetBalls();
			}

			public override void Enable()
			{
				this.enabled = true;
				foreach (var ball in this.GetBalls())
				{
					logicList.Add(new BallLogic(ball, ref mutex));
				}
			}

			public override void Disable()
			{
				this.enabled = false;
                foreach (var logic in logicList)
                {
					logic.Stop();
                }
                this.logicList.Clear();
			}

			public override bool IsEnabled()
			{
				return this.enabled;
			}
		}
	}
}