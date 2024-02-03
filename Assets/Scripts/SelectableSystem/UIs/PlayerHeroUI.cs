using UnityEngine;

namespace SelectableSystem
{
    public class PlayerHeroUI : SelectableObjectUIBase<PlayerHero>
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private SelectionField selectionField;

        protected override void OnStart()
        {
            base.OnStart();
        
            healthBar.Init(selectable.HealthPoints);
            healthBar.Show();

            selectionField.Init(selectable.IsSelected);

            SelectedEvent += selectionField.Show;
            DeselectedEvent += selectionField.Hide;
        }
    }
}