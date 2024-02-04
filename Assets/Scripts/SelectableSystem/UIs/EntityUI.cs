using Entities;
using UnityEngine;

namespace SelectableSystem
{
    public class EntityUI: SelectableObjectUIBase<EntityBase>
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private EffectsBar effectsBar;
        [SerializeField] private SelectionField selectionField;

        protected override void OnStart()
        {
            base.OnStart();
            
            effectsBar.Init(selectable.EffectsProcessor);
            effectsBar.Show();
            
            healthBar.Init(selectable.HealthPoints);
            healthBar.Show();
            
            selectionField.Init(selectable.IsSelected);

            SelectedEvent += selectionField.Show;
            DeselectedEvent += selectionField.Hide;
        }
    }
}