using System;
using Cards;
using CustomTimer;
using Enemies;
using Entities;
using Entities.Enemies;
using EventBusExtension;
using Events;
using GameCycle;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class PlayerHero : EntityBase, IEventReceiver<EnemyReachFightPoint>, IGameCycleUpdate, ICardTarget
{
    [Inject] private EventBus _eventBus;
    [Inject] private IGameCycleController _gameCycleController;
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    public float moveSpeed;
    public event Action OnDie;
    public event Action<float> OnMove;

    private Enemy _enemyForFight;
    private Timer _attackCooldown;
    
    protected override void OnAwake()
    {
        _gameCycleController.AddListener(GameCycleState.Gameplay, this);
        
        _eventBus.Subscribe(this);
        
        _attackCooldown = new Timer(60 / attackSpeed);
        
        OnUpdate += Move;
        OnUpdate += _attackCooldown.Tick;
    }

    public void GameCycleUpdate() => HandleUpdate(Time.deltaTime);
    
    public override void TakeDamage(float damage)
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
    
    private void OnDestroy()
    {
        _gameCycleController.RemoveListener(GameCycleState.Gameplay, this);
    }
}
