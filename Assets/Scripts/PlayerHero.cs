using System;
using CustomTimer;
using Enemies;
using EventBusExtension;
using Events;
using GameCycle;
using SelectableSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

public class PlayerHero : MonoBehaviour, IEventReceiver<EnemyReachFightPoint>, IHaveUI, IGameCycleUpdate
{
    [Inject] private EventBus _eventBus;
    [Inject] private IGameCycleController _gameCycleController;
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    public float moveSpeed;
    public float attackDamage;
    
    [Tooltip("attack speed per minute")]
    public float attackSpeed;
    
    public event Action OnDie;
    public event Action<float> OnMove;
    public event Action<float> OnUpdate;

    private Enemy _enemyForFight;
    private Timer _attackCooldown;
    [SerializeField] private SomeStorageFloat healthPoints;
    public IReadOnlySomeStorage<float> HealthPoints => healthPoints;
    
    private void Awake()
    {
        _gameCycleController.AddListener(GameCycleState.Gameplay, this);
        
        _eventBus.Subscribe(this);
        
        _attackCooldown = new Timer(60 / attackSpeed);
        
        OnUpdate += Move;
        OnUpdate += _attackCooldown.Tick;
    }

    public void GameCycleUpdate()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        healthPoints.ChangeCurrentValue(-damage);
        if (healthPoints.IsEmpty)
        {
            OnDie?.Invoke();
        }
    }
    
    private void Move(float time)
    {
        OnMove?.Invoke(moveSpeed * time);
        _eventBus.Invoke(new PlayerHeroMove(moveSpeed * time));
    }

    private void Attack()
    {
        _attackCooldown.Reset();
        _enemyForFight.TakeDamage(attackDamage);
    }
    
    private void Die()
    {
        OnDie?.Invoke();
    }

    public void OnEvent(EnemyReachFightPoint @event)
    {
        _enemyForFight = @event.Enemy;
        _enemyForFight.OnDie += OnEnemyDieOnFightPoint;
        OnUpdate -= Move;
        _attackCooldown.OnTimerEnd += Attack;
        
        if(_attackCooldown.TimerIsEnd)
            Attack();
    }

    private void OnEnemyDieOnFightPoint(Enemy enemy)
    {
        _enemyForFight.OnDie -= OnEnemyDieOnFightPoint;
        OnUpdate += Move;
        _attackCooldown.OnTimerEnd -= Attack;
    }

    public bool IsSelected { get; }
    public event Action OnSelect;
    public event Action OnDeselect;
    public void Select()
    {
        throw new NotImplementedException();
    }

    public void Deselect()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        _gameCycleController.RemoveListener(GameCycleState.Gameplay, this);
    }
}
