using Configs.Cards;
using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace Cards.CardsLogics
{
    public class ReshuffleCardLogic : CardLogicBase
    {
        [Inject] private ReshuffleCardConfig _reshuffleCardConfig;
        [Inject] private CardLine _cardLine;
        [Inject] private CardCreatorWithPause _cardCreatorWithPause;
        
        public override bool TryUse(ICardTarget target)
        {
            var enemyIndex = Random.Range(0, _reshuffleCardConfig.PossibleEnemy.Count);
            _cardLine.SpawnNewCard(_reshuffleCardConfig.PossibleEnemy[enemyIndex]);

            for (int i = 0; i < _reshuffleCardConfig.HealthCardsCount; i++)
            {            
                var healIndex = Random.Range(0, _reshuffleCardConfig.PossibleHeals.Count);
                _cardCreatorWithPause.AddCard(_reshuffleCardConfig.PossibleHeals[healIndex]);
            }

            return true;
        }
    }
}