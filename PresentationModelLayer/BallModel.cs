
using DataLayer;
using LogicLayer;

namespace PresentationModelLayer
{
    public class BallModel
    {
        private readonly AbstractLogicApi _logicApi;
        public BallModel(int ballNumber)
        {   
            _logicApi = AbstractLogicApi.CreateApi(900, 600, ballNumber, 25);
        }

        public void StartSimulation()
        {
            _logicApi.Enable();
        }

        public void StopSimulation()
        {
            _logicApi.Disable();
        }
        public bool IsRunning()
        {
            return _logicApi.IsEnabled();
        }

        public List<Ball> GetBalls()
        {
            return _logicApi.GetBalls();
        }


    }
}