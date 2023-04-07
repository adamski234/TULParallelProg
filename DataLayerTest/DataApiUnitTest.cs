using DataLayer;

namespace DataLayerTest
{
	[TestClass]
	public class DataApiUnitTest
	{
		[TestMethod]
		public void DataApiCreationTest()
		{
			AbstractDataApi api = AbstractDataApi.CreateApi(500, 500, 5, 5);
			Assert.IsNotNull(api);
			Assert.AreEqual(api.GetBalls().Count, 5);
		}
	}
}