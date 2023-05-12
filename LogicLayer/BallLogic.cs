using System.ComponentModel;
using DataLayer;
using System.Diagnostics;

namespace LogicLayer;

public class BallLogic : INotifyPropertyChanged
{
	private Ball ball;
	public BallLogic(Ball input)
	{
		this.ball = input;
	}

	public event PropertyChangedEventHandler? PropertyChanged
	{
		add
		{
			ball.PropertyChanged += value;
		}

		remove
		{
			ball.PropertyChanged -= value;
		}
	}

	public void Stop()
	{
		this.ball.Disable();
	}

	public void Start()
	{
		this.ball.Enable();
	}

	public Ball GetBall()
	{
		return this.ball;
	}
}