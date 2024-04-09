using System;
using UnityEngine;

namespace TrainLevel
{
    public class TrainWindow : MonoBehaviour
    {
        public event Action OnContinueButtonPressed;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
        
        public void _Continue()
        {
            OnContinueButtonPressed?.Invoke();
        }
    }
}