using System;
using UnityEngine;

namespace SelectableSystem
{
    public abstract class SelectableObjectUIBase<TSelectable> : MonoBehaviour
        where TSelectable : ISelectable
    {
        [SerializeField] protected TSelectable selectable;
        [SerializeField] private GameObject canvas;
    
        public event Action SelectedEvent;
        public event Action DeselectedEvent;
    
        private void Start() => OnStart();

        protected virtual void OnStart()
        {
            canvas.SetActive(true);
            // selectable.OnSelect += () => canvas.SetActive(true);
            // selectable.OnDeselect += () => canvas.SetActive(false);
        
            selectable.OnSelect += () => SelectedEvent?.Invoke();
            selectable.OnDeselect += () => DeselectedEvent?.Invoke();
        }

        private void OnDestroy() => DeselectedEvent?.Invoke();
    }
}