using DataLayer;
using LogicLayer;

namespace LogicLayerTest;

[TestClass]
public class BallLogicTest
{
	[TestMethod]
	public void TestMoving()
	{
		Ball ball = new Ball(5, 5, 5);
		object mutex = new object();
		BallLogic logic = new BallLogic(ball, ref mutex, 500, 500);
		Thread.Sleep(300);
		double currentX, currentY;
		lock (mutex)
		{
			currentY = ball.Y;
			currentX = ball.X;
			Assert.AreNotEqual(ball.X, 5); // check if move happened
			Assert.AreNotEqual(ball.Y, 5);
			Thread.Sleep(300);
			Assert.AreEqual(currentX, ball.X); // check that balls aren't moving while mutex is locked
			Assert.AreEqual(currentY, ball.Y);
		}
		logic.Stop();
		Thread.Sleep(300); // allow all threads to finish
		// check that threads respect disabling
		currentY = ball.Y;
		currentX = ball.X;
		Thread.Sleep(300);
		Assert.AreEqual(currentX, ball.X); // check that balls haven't moved after disabling
        Assert.AreEqual(currentY, ball.Y);
	}
}