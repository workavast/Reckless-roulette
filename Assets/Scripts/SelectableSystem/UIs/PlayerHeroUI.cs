using UnityEngine;

namespace SelectableSystem
{
    public class PlayerHeroUI : SelectableObjectUIBase<PlayerHero>
    {
        [SerializeField] private HealthBar healthBar;
    
        protected override void OnStart()
        {
            base.OnStart();
        
            healthBar.Init(selectable.HealthPoints);
            healthBar.Show();
            // SelectedEvent += healthBar.Show;
            // DeselectedEvent += healthBar.Hide;
        }
    }
}