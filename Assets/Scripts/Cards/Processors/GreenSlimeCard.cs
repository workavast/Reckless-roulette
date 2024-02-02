using Enemies;
using Factories;
using Zenject;

namespace Cards
{
    public class GreenSlimeCard : EnemyCreatorCardBase
    {
        protected override EnemyType EnemyType => EnemyType.GreenSlime;
    }
}