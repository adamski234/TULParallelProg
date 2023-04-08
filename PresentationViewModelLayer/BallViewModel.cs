
using DataLayer;
using PresentationModelLayer;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PresentationViewModelLayer
{
	public class BallViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Ball> BallList { get; set; } = new ObservableCollection<Ball>();
        public int BallNumber { get; set; }
        public BallModel? Model { get; set; }
        public ICommand StartSimulationCommand { get; set; }
        public ICommand StopSimulationCommand { get; set; }

        public BallViewModel()
        {
            StartSimulationCommand = new RelayCommand(Start);
            StopSimulationCommand = new RelayCommand(Stop);
        }

        public void Start()
        {
            Model = new BallModel(BallNumber);
            BallList = new ObservableCollection<Ball>(Model.GetBalls());
            OnPropertyChanged(nameof(BallList));
            Model.StartSimulation();
        }

        public void Stop()
        {
            Model.StopSimulation();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}