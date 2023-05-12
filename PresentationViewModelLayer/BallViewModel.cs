
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
        private BallModel? _model;
		public ObservableCollection<Ball> BallList { get; set; } = new ObservableCollection<Ball>();
        public int BallNumber { get; set; }
        public ICommand StartSimulationCommand { get; set; }
        public ICommand StopSimulationCommand { get; set; }

        public BallViewModel()
        {
            StartSimulationCommand = new RelayCommand(Start);
            StopSimulationCommand = new RelayCommand(Stop);
        }

        public void Start()
        {   
            if (BallNumber == BallList.Count)
            {
                if (!_model.IsRunning())
                {
                    _model?.StartSimulation();
                }
            } else
            {
                _model?.StopSimulation();
                _model = new BallModel(BallNumber);
                BallList = new ObservableCollection<Ball>(_model.GetBalls());
                OnPropertyChanged(nameof(BallList));
                _model.StartSimulation();
            }
        }

        public void Stop()
        {
            _model?.StopSimulation();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}