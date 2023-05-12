using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DataLayer;

public class Ball : INotifyPropertyChanged
{
    private double x;
    private double y;
    public double Radius { get; }
    public double speedX;
    public double speedY;
    private bool isEnabled = true;
    private Thread runningThread;
    private object mutex;


	public Ball(double x, double y, double radius, ref object mutexObject)
    {
        this.x = x;
        this.y = y;
        this.Radius = radius;
        this.mutex = mutexObject;
		Random source = new Random();
        do
        {
            this.speedX = source.NextDouble() * 2;
        } while (speedX == 0);
        do
        {
            this.speedY = source.NextDouble() * 2;
        } while (speedY == 0);
    }

    public double X
    {
        get { return this.x; }
        set
        {
            this.x = value;
            OnPropertyChanged();
        }

    }

    public double Y
    {
        get { return this.y;  }
        set
        {
            this.y = value;
            OnPropertyChanged();
        }
    }

    public void Enable()
    {
		this.runningThread = new Thread(() =>
		{
			while (isEnabled)
			{
				lock (mutex)
				{
					this.X += this.speedX;
					this.Y += this.speedY;
                    OnPropertyChanged("Position"); // Stupid workaround to get a specific single event for movement, rather than two for each coord
				}
				Thread.Sleep(50); // 20 tps tickrate
			}
		});
		this.isEnabled = true;
        this.runningThread.Start();
	}

    public void Disable()
    {
	    this.isEnabled = false;
    }

    #region INotifyPropertyChanged
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}