
using DataLayer;
using LogicLayer;

namespace PresentationModelLayer
{
    public class BallModel
    {
        private AbstractLogicApi LogicApi;
        public BallModel(int ballNumber)
        {   
            LogicApi = AbstractLogicApi.CreateApi(500, 500, ballNumber, 15);
        }

        public void StartSimulation()
        {
            LogicApi.Enable();
        }

        public void StopSimulation()
        {
            LogicApi.Disable();
        }

        public List<Ball> GetBalls()
        {
            return LogicApi.GetBalls();
        }


    }
}