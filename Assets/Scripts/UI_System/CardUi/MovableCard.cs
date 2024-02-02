using System;
using Cards;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.CardUi
{
    [RequireComponent(typeof(RectTransform))]
    public class MovableCard : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Image FillImage;

        public int HolderIndex { get; private set; }
        public bool IsReachDestination { get; private set; }

        private RectTransform _rectTransform;
        private Transform _destinationTarget;
        private CardProcessorBase _card;
        private bool _isMove;

        private event Action<float> OnUpdate;
        
        public event Action OnReachDestination;
        public event Action<int> OnUse;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void HandleUpdate(float time) => OnUpdate?.Invoke(time);

        public void SetCardData(CardProcessorBase card, Sprite sprite)
        {
            _card = card;
            FillImage.sprite = sprite;
        }
        
        public void SetDestination(int holderIndex, Transform cardHolder)
        {
            IsReachDestination = false;
            HolderIndex = holderIndex;
            _destinationTarget = cardHolder;
            
            if(!_isMove)
                OnUpdate += Move;
            
            _isMove = true;
        }

        public void UseCard()
        {
            _card.UseCard();
            OnUse?.Invoke(HolderIndex);
        }

        private void Move(float time)
        {
            _rectTransform.Translate(Vector3.left * (moveSpeed * time));
            
            if (_rectTransform.position.x <= _destinationTarget.position.x)
            {
                var pos = _rectTransform.position;
                pos.x = _destinationTarget.position.x;
                
                _rectTransform.position = pos;
                OnUpdate -= Move;
                _isMove = false;
                IsReachDestination = true;
                
                OnReachDestination?.Invoke();
            }
        }
    }
}