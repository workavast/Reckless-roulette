using GameCycle;
using UI_System.Elements;
using UnityEngine.SceneManagement;

namespace TrainLevel
{
    public class TrainProcessor
    {
        private readonly StartTrainBlock _startTrainBlock;
        private readonly EnemyTrainBlock _enemyTrainBlock;
        private readonly EffectsTrainBlock _effectsTrainBlock;
        private readonly LevelUpTrainBlock _levelUpTrainBlock;
        private readonly HealTrainBlock _healTrainBlock;
        private readonly FinalTrainBlock _finalTrainBlock;
        
        public TrainProcessor(GameCycleController gameCycleController, CardLine cardLine, TrainWindow startWindow,
            TrainWindow enemyCardWindow, TrainWindow effectCardWindow, TrainWindow levelUpWindow, 
            TrainWindow healCardWindow, TrainWindow finalTrainWindow)
        {
            _startTrainBlock = new StartTrainBlock(gameCycleController, startWindow);
            _enemyTrainBlock = new EnemyTrainBlock(gameCycleController, cardLine, enemyCardWindow);
            _effectsTrainBlock = new EffectsTrainBlock(gameCycleController, cardLine, effectCardWindow);
            _levelUpTrainBlock = new LevelUpTrainBlock(gameCycleController, levelUpWindow);
            _healTrainBlock = new HealTrainBlock(gameCycleController, cardLine, healCardWindow);
            _finalTrainBlock = new FinalTrainBlock(gameCycleController, finalTrainWindow);
            
            _startTrainBlock.OnEnd += _enemyTrainBlock.Enter;
            _enemyTrainBlock.OnEnd += _effectsTrainBlock.Enter;
            _effectsTrainBlock.OnEnd += _levelUpTrainBlock.Enter;
            _levelUpTrainBlock.OnEnd += _healTrainBlock.Enter;
            _healTrainBlock.OnEnd += _finalTrainBlock.Enter;
            _finalTrainBlock.OnEnd += LoadMainMenu;
        }
        
        public void Invoke()
        {
            _startTrainBlock.Enter();
        }
        
        private void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}