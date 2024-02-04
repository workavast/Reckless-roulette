namespace SelectableSystem
{
    public interface ISelectable
    {
        public bool IsSelected { get; }
        public event System.Action OnSelect;
        public event System.Action OnDeselect;
        
        public void Select();
        public void Deselect();
    }
}