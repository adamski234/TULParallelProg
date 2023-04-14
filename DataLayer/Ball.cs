using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLayer;

public class Ball : INotifyPropertyChanged
{
    private double x;
    private double y;
    public double Radius { get; }
    public double speedX;
    public double speedY;

    public Ball(double x, double y, double radius)
    {
        this.x = x;
        this.y = y;
        this.Radius = radius;
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
            OnPropertyChanged("X");
        }

    }

    public double Y
    {
        get { return this.y;  }
        set
        {
            this.y = value;
            OnPropertyChanged("Y");
        }
    }


    #region INotifyPropertyChanged
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}