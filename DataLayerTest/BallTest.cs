using DataLayer;

namespace DataLayerTest;

[TestClass]
public class BallTest
{
	[TestMethod]
	public void TestMoving()
	{
		object mutex = new object();
		Ball ball = new Ball(5, 5, 5, ref mutex, 1);
		ball.Enable();
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
		Thread.Sleep(300); // allow all threads to finish
		// check that threads respect disabling
		ball.Disable();
		currentY = ball.Y;
		currentX = ball.X;
		Thread.Sleep(300);
		Assert.AreEqual(currentX, ball.X); // check that balls haven't moved after disabling
		Assert.AreEqual(currentY, ball.Y);
	}
}