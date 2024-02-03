using UnityEngine;

namespace SelectableSystem
{
    public class SelectionField : MonoBehaviour, ShowableUI
    {
        public void Init(bool isSelected) => gameObject.SetActive(isSelected);

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}