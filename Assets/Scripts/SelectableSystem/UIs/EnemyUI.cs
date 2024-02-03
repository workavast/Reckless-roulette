using Enemies;
using UnityEngine;

namespace SelectableSystem
{
    public class EnemyUI : SelectableObjectUIBase<Enemy>
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