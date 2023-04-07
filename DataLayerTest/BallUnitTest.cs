using DataLayer;

namespace DataLayerTest;

[TestClass]
public class BallUnitTest
{
    [TestMethod]
    public void BallMoveTest()
    {
        Ball ball = new Ball(1, 1, 5);
        ball.move();
        Assert.AreNotEqual(1, ball.X);
        Assert.AreNotEqual(1, ball.Y);
    }
    
}