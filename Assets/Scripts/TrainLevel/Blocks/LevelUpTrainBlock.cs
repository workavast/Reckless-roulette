using System;
using GameCycle;

namespace TrainLevel
{
    public class LevelUpTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly TrainWindow _trainWindow; 
        
        public override event Action OnEnd;

        public LevelUpTrainBlock(GameCycleController gameCycleController, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _trainWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenLevelUpTrainWindow();
        }
        
        private async void OpenLevelUpTrainWindow()//когда противник умер показываем на полоску прокачки, мол она выросла
        {
            await Wait(5000);
            //говорим как работают уровни
            _trainWindow.Open();
            _trainWindow.OnContinueButtonPressed += CloseLevelUpTrainWindow;
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }
        private void CloseLevelUpTrainWindow()
        {
            _gameCycleController.SwitchState(GameCycleState.Gameplay);
            _trainWindow.Close();
            
            OnEnd?.Invoke();
        }
    }
}