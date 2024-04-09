using System;
using Cards;
using GameCycle;
using UI_System.CardUi;
using UI_System.Elements;

namespace TrainLevel
{
    public class EnemyTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly CardLine _cardLine;
        private readonly TrainWindow _enemyCardWindow; 
        
        public override event Action OnEnd;

        public EnemyTrainBlock(GameCycleController gameCycleController, CardLine cardLine, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _cardLine = cardLine;
            _enemyCardWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenEnemyCardTrainWindow();
        }
        
        private void OpenEnemyCardTrainWindow()//говорим внизу карта выехала, перетините ее на поле
        {
            _gameCycleController.SwitchState(GameCycleState.Gameplay);
            _enemyCardWindow.Open();
            SpawnEnemyCard();
        }
        
        private void SpawnEnemyCard()//спавним карту
        {
            _cardLine.OnCardSpawn += SubToEnemyCard;
            _cardLine.SpawnNewCard(CardType.SlimeGreen);
        }
        
        private void SubToEnemyCard(MovableCard movableCard)// если она достигла конца конвеера, ставим игру на паузу и ждем пока карта будет использована
        {
            _cardLine.OnCardSpawn -= SubToEnemyCard;
            movableCard.OnReachDestination += WaitUseEnemyCard;
            movableCard.OnUse += OnUseEnemyCard;
        }
        
        private void WaitUseEnemyCard()
        {
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }
        
        private void OnUseEnemyCard(int holderIndex)
        {
            _gameCycleController.SwitchState(GameCycleState.Gameplay);
            _enemyCardWindow.Close();
            OnEnd?.Invoke();
        }
    }
}