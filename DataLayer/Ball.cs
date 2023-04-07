﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataLayer;

public class Ball : INotifyPropertyChanged
{
    private double x;
    private double y;
    public readonly double radius;
    public double speedX;
    public double speedY;

    public Ball(double x, double y, double radius)
    {
        this.x = x;
        this.y = y;
        this.radius = radius;
        Random source = new Random();
        do
        {
            this.speedX = source.NextDouble();
        } while (speedX == 0);
        do
        {
            this.speedY = source.NextDouble();
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

    public void move()
    {
        this.X += this.speedX;
        this.Y += this.speedY;
    }


    #region INotifyPropertyChanged
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}