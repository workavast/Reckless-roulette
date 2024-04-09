using System;
using GameCycle;

namespace TrainLevel
{
    public class FinalTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly TrainWindow _trainWindow; 
        
        public override event Action OnEnd;

        public FinalTrainBlock(GameCycleController gameCycleController, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _trainWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenFinalTrainWindow();
        }
        
        private void OpenFinalTrainWindow()// показываем прогресс прохождения и говорим че это
        {
            _trainWindow.Open();
            _trainWindow.OnContinueButtonPressed += CloseFinalTrainWindow;
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }
        
        private void CloseFinalTrainWindow()
        {
            _trainWindow.Close();
            
            OnEnd?.Invoke();
        }
    }
}