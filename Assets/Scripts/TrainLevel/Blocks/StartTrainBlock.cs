using System;
using GameCycle;

namespace TrainLevel
{
    public class StartTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly TrainWindow _startWindow; 

        public override event Action OnEnd;

        public StartTrainBlock(GameCycleController gameCycleController, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _startWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenStartTrainWindow();
        }
        
        private void OpenStartTrainWindow()
        {
            _startWindow.Open();
            _startWindow.OnContinueButtonPressed += OnContinue;
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }

        private void OnContinue()
        {
            _startWindow.Close();
            OnEnd?.Invoke();
        }
    }
}