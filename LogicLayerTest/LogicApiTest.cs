using System.Windows.Documents;
using DataLayer;
using LogicLayer;

namespace LogicLayerTest;

[TestClass]
public class LogicApiTest
{
	[TestMethod]
	public void TestCollision()
	{
		AbstractLogicApi api = AbstractLogicApi.CreateApi(500, 500, 2, 15);
		api.Enable();
		api.Disable();
		List<Ball> balls = api.GetBalls();
		Assert.IsNotNull(balls);
		balls[0].speedX = 0;
		balls[0].speedY = 1;
		balls[0].X = 50;
		balls[0].Y = 50;
		balls[1].speedX = 0;
		balls[1].speedY = 0;
		balls[1].X = 50;
		balls[1].Y = 100;
		api.Enable();
		Thread.Sleep(1000);
		api.Disable();
		Assert.AreNotEqual(balls[1].speedY, 0);
	}
}