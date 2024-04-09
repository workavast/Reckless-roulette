using System;
using Cards;
using GameCycle;
using UI_System.CardUi;
using UI_System.Elements;

namespace TrainLevel
{
    public class EffectsTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly CardLine _cardLine;
        private readonly TrainWindow _trainWindow; 
        
        public override event Action OnEnd;

        public EffectsTrainBlock(GameCycleController gameCycleController, CardLine cardLine, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _cardLine = cardLine;
            _trainWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenEffectsTrainWindow();
        }
        
        private async void OpenEffectsTrainWindow()//если противник заспавнен спавним яд, ждем пару секунд
        {
            await Wait(6000);
            _trainWindow.Open();
            
            SpawnPoisonCard();
        }
        
        private void SpawnPoisonCard()// говорим как работают эффекты, внизу карта выехала, перетините ее на поле
        {
            _cardLine.OnCardSpawn += SubToPoisonCard;
            _cardLine.SpawnNewCard(CardType.Poison);
        }
        private void SubToPoisonCard(MovableCard movableCard)
        {
            _cardLine.OnCardSpawn -= SubToPoisonCard;
            movableCard.OnReachDestination += WaitUsePoisonCard;
            movableCard.OnUse += OnUsePoisonCard;
        }
        private void WaitUsePoisonCard()//ставим игру на паузу, просим применить карту
        {
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }
        private void OnUsePoisonCard(int holderIndex)//карта применена, снимае паузу и ждем пока противник умрет
        {
            _gameCycleController.SwitchState(GameCycleState.Gameplay);
            _trainWindow.Close();
            
            OnEnd?.Invoke();
        }

    }
}