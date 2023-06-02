using System.IO;

namespace DataLayer
{
	public class Logger
	{	
		private static string _path;
		public static void UpdateLog(Ball ball) 
		{
			string info = "\n{\"ID\":\"" + ball.Id + "\", \"X\":\"" + ball.X.ToString("0.00") + "\", \"Y\":\"" + ball.Y.ToString("0.00") 
				+ "\", \"SpeedX\":\"" + ball.speedX.ToString("0.00") + "\", \"SpeedY\":\"" + ball.speedY.ToString("0.00") + "\"}]";
			if(File.Exists(_path))
			{
				string jsonData = File.ReadAllText(_path);
				if (jsonData.Length != 2)
				{
					info = info.Insert(0, ",");
				}
				jsonData = jsonData.Substring(0, jsonData.Length - 1);
				jsonData += info;
				File.WriteAllText(_path, jsonData);
			}
		}

		public static void Init()
		{	
			_path = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent}\\Log\\ball_log.json";
			if (File.Exists(_path))
			{
				File.Delete(_path);
			}
			File.WriteAllText(_path, "[]");
		}
	}
}
