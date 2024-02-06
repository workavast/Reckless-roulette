using CastExtension;
using Configs.Cards;
using Entities.Bosses;
using Entities.Enemies;
using Zenject;

namespace Cards.CardsLogics
{
    public class KillEnemyAndTakeDamageCardLogic : CardLogicBase
    {
        [Inject] private KillEnemyAndTakeDamageCardConfig _config;
        [Inject] private PlayerHero _playerHero;
        
        public override bool TryUse(ICardTarget target)
        {
            if (target.TryCast(out Enemy enemy))
            {
                if (target.CastPossible<BossBase>())
                    enemy.TakeDamage(7);
                else
                    enemy.TakeDamage(float.MaxValue);

                _playerHero.TakeDamage(_config.PlayerDamage);

                return true;
            }

            return false;
        }
    }
}