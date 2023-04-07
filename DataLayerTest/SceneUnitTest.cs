using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTest;

[TestClass]
public class SceneUnitTest
{
	[TestMethod]
	public void SceneCreationTest()
	{
		Scene scene = new Scene(500, 500, 5, 5);
		Assert.AreEqual(scene.Balls.Count, 5);
		for (int i = 0; i < scene.Balls.Count; i++)
		{
			for (int j = i; j  < scene.Balls.Count; j++)
			{
				if (j == i)
				{
					continue;
				}
                    double distanceBetweenCentres = Math.Sqrt(Math.Pow(scene.Balls[i].X - scene.Balls[j].X, 2) + Math.Pow(scene.Balls[i].Y - scene.Balls[j].Y, 2));
				if (distanceBetweenCentres < scene.Balls[i].radius + scene.Balls[j].radius)
				{
					Assert.Fail("Balls overlap");
				}
                }
		}
	}
}
