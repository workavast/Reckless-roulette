using System;
using Cards;
using GameCycle;
using UI_System.CardUi;
using UI_System.Elements;

namespace TrainLevel
{
    public class HealTrainBlock : TrainBlockBase
    {
        private readonly GameCycleController _gameCycleController;
        private readonly CardLine _cardLine;
        private readonly TrainWindow _trainWindow; 
        
        public override event Action OnEnd;

        public HealTrainBlock(GameCycleController gameCycleController, CardLine cardLine, TrainWindow trainWindow)
        {
            _gameCycleController = gameCycleController;
            _cardLine = cardLine;
            _trainWindow = trainWindow;
        }
        
        public override void Enter()
        {
            OpenHealTrainWindow();
        }
        
        private void OpenHealTrainWindow()//спавним хилку
        {
            _trainWindow.Open();
            //говорим как работают отхилы, внизу карта выехала, перетините ее на поле
            // просим ее применить, ессли хилка дойдет до конца полоски, ставим игру на паузу и ждем,
            SpawnHealCard();
        }
        private void SpawnHealCard()
        {
            _cardLine.OnCardSpawn += SubToHealCard;
            _cardLine.SpawnNewCard(CardType.Heal);
        }
        private void SubToHealCard(MovableCard movableCard)
        {
            _cardLine.OnCardSpawn -= SubToHealCard;
            movableCard.OnReachDestination += WaitUseHealCard;
            movableCard.OnUse += OnUseHealCard;
        }
        private void WaitUseHealCard()//ставим игру на паузу, просим применить карту
        {
            _gameCycleController.SwitchState(GameCycleState.Pause);
        }
        private void OnUseHealCard(int holderIndex)//карта применена, снимае паузу и ждем пока противник умрет
        {
            _gameCycleController.SwitchState(GameCycleState.Gameplay);
            _trainWindow.Close();
            
            OnEnd?.Invoke();
        }
    }
}