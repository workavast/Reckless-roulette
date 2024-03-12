using System;
using Cards;
using Cards.CardsLogics;
using EventBusExtension;
using Events.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI_System.CardUi
{
    [RequireComponent(typeof(RectTransform))]
    public class MovableCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image dragTargetImage;

        [Inject] private EventBus _eventBus;
        
        public int HolderIndex { get; private set; }
        public bool IsReachDestination { get; private set; }
        public bool Interactable { get; private set; } = true;
        public Type CardLogicType => _cardLogicBase.MyType;

        private CardLogicBase _cardLogicBase;
        private Vector3 _currentCardLinePosition;
        private RectTransform _rectTransform;
        private Transform _destinationTarget;
        private float _cardMoveSpeed;
        private float _moveScale;
        private bool _isMove;
        private bool _isDrag;
        
        private event Action<float> OnUpdate;
        
        public event Action OnReachDestination;
        public event Action<int> OnUse;
        
        private void Awake()
        {
            _moveScale = Screen.width / 1080f;
            
            _cardLogicBase = GetComponent<CardLogicBase>();
            _rectTransform = GetComponent<RectTransform>();
            _currentCardLinePosition = _rectTransform.position;
        }
        
        public void HandleUpdate(float time) => OnUpdate?.Invoke(time);
        
        //need cus in the awake dont work cus card take spawn position after instantiate
        public void Init(Transform startTransform, float moveSpeed)
        {
            _currentCardLinePosition = startTransform.position;
            _cardMoveSpeed = moveSpeed;
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
        
        private void Move(float time)
        {
            _currentCardLinePosition += Vector3.left * (_cardMoveSpeed * _moveScale * time);
            
            if (_currentCardLinePosition.x <= _destinationTarget.position.x)
            {
                var pos = _currentCardLinePosition;
                pos.x = _destinationTarget.position.x;
                
                _currentCardLinePosition = pos;
                OnUpdate -= Move;
                _isMove = false;
                IsReachDestination = true;
                
                OnReachDestination?.Invoke();
            }
            
            if(!_isDrag) 
                _rectTransform.position = _currentCardLinePosition;
        }
        
        public void SwitchInteractionState(bool interactable)
        {
            Interactable = interactable;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!Interactable) return;

            _isDrag = true;
            dragTargetImage.raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GameObject target = eventData.pointerCurrentRaycast.gameObject;
            
            if (target is null)
            {
                var cardTarget = CastRaycast();
                if (_cardLogicBase.TryUse(cardTarget))
                {
                    OnUse?.Invoke(HolderIndex);
                    _eventBus.Invoke(new CardUse());
                }
            }

            _rectTransform.position = _currentCardLinePosition;
            
            _isDrag = false;
            dragTargetImage.raycastTarget = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
        }

        private ICardTarget CastRaycast()
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var castRes = Physics2D.Raycast(point, Vector2.zero);

            ICardTarget res = null;
            if(castRes)
                castRes.collider.gameObject.TryGetComponent(out res);

            return res;
        }
    }
}