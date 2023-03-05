namespace ProgramUnitTests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Sum_AddTwoNumbers_ReturnsSum()
        {
            int result = Program.Sum(10, 20);

            Assert.AreEqual(30, result);
        }

        [TestMethod]
        public void IsEven_EvenNumber_ReturnsTrue()
        {
            bool result = Program.IsEven(10);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEven_OddNumber_ReturnsFalse()
        {
            bool result = Program.IsEven(9);

            Assert.IsFalse(result);
        }
    }
}