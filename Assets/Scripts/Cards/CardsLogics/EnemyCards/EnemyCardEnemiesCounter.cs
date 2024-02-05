using UnityEngine;

namespace Cards.CardsLogics.EnemyCards
{
    public class EnemyCardEnemiesCounter : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemiesCountMarks;
        
        private void Awake()
        {
            var enemyCreatorCardLogicBase = GetComponent<EnemyCreatorCardLogicBase>();

            for (int i = 0; i < enemyCreatorCardLogicBase.EnemiesCount && i < enemiesCountMarks.Length; i++)
                enemiesCountMarks[i].SetActive(true);
        }
    }
}