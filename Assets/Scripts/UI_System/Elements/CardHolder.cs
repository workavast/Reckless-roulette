using UnityEngine;

namespace UI_System.CardUi
{
    public class CardHolder : MonoBehaviour
    {
        public bool HaveMovableCard { get; private set; }
        
        public void SetCard()
        {
            HaveMovableCard = true;
        }

        public void ResetCard()
        {
            HaveMovableCard = false;
        }
    }
}