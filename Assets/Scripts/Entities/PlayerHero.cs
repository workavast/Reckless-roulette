using System;
using Cards;
using CustomTimer;
using Enemies;
using Entities;
using Entities.Enemies;
using Entities.EnemiesGroups;
using EventBusExtension;
using Events;
using GameCycle;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class PlayerHero : EntityBase, IEventReceiver<EnemyGroupReachFightPoint>, IGameCycleUpdate, ICardTarget
{
    [Inject] private EventBus _eventBus;
    [Inject] private IGameCycleController _gameCycleController;
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    public float moveSpeed;
    public event Action OnDie;
    public event Action<float> OnMove;

    private EnemyGroup _enemyForFight;
    
    protected override void OnAwake()
    {
        _gameCycleController.AddListener(GameCycleState.Gameplay, this);
        
        _eventBus.Subscribe(this);
        
        OnUpdate += Move;
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
        AttackCooldown.Reset();
        _enemyForFight.TakeDamage(attackDamage);
    }
    
    private void Die()
    {
        OnDie?.Invoke();
    }

    public void OnEvent(EnemyGroupReachFightPoint @event)
    {
        _enemyForFight = @event.EnemyGroup;
        _enemyForFight.OnGroupDie += OnEnemyDieOnFightPoint;
        OnUpdate -= Move;
        AttackCooldown.OnTimerEnd += Attack;
        
        if(AttackCooldown.TimerIsEnd)
            Attack();
    }

    private void OnEnemyDieOnFightPoint(EnemyGroup enemy)
    {
        _enemyForFight.OnGroupDie -= OnEnemyDieOnFightPoint;
        OnUpdate += Move;
        AttackCooldown.OnTimerEnd -= Attack;
    }
    
    private void OnDestroy()
    {
        _gameCycleController.RemoveListener(GameCycleState.Gameplay, this);
    }
}
