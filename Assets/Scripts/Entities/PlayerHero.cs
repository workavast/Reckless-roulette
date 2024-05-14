using System;
using Cards;
using Entities;
using Entities.EnemiesGroups;
using EventBusExtension;
using Events;
using Events.Audio;
using GameCycle;
using PlayerLevelSystem;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class PlayerHero : EntityBase, IEventReceiver<EnemyGroupReachFightPoint>, IGameCycleUpdate, ICardTarget
{
    [SerializeField] private DamageLevelConfig damageLevelConfig;
    [SerializeField] private HealthPointsLevelConfig healthPointsLevelConfig;
    [SerializeField] private ArmorLevelConfig armorLevelConfig;
    
    [Inject] private EventBus _eventBus;
    [Inject] private IGameCycleController _gameCycleController;
    
    public ReceiverIdentifier ReceiverIdentifier { get; } = new();

    public float moveSpeed;
    public event Action OnDie;
    public event Action<float> OnMove;

    private float _armor;
    private EnemyGroup _enemyForFight;

    public HealthPointsLevelSystem HealthPointsLevelSystem;
    public DamageLevelSystem DamageLevelSystem;
    public ArmorLevelSystem ArmorLevelSystem;

    public override event Action OnTakeDamage;
    public override event Action OnAttack;
    public event Action OnParamsLoad;

    protected override void OnAwake()
    {
        DamageLevelSystem = new DamageLevelSystem(damageLevelConfig, _eventBus, this);
        HealthPointsLevelSystem = new HealthPointsLevelSystem(healthPointsLevelConfig, _eventBus, healthPoints);
        ArmorLevelSystem = new ArmorLevelSystem(armorLevelConfig, _eventBus, this);
            
        _gameCycleController.AddListener(GameCycleState.Gameplay, this as IGameCycleUpdate);
        
        _eventBus.Subscribe(this);
        
        OnUpdate += Move;
    }

    private void Start()
    {
        PlayerHeroSaver.Instance.LoadParams(this);
    }

    public void LoadParams(PlayerHeroParamsSaves saves)
    {
        DamageLevelSystem?.Dispose();
        HealthPointsLevelSystem?.Dispose();
        ArmorLevelSystem?.Dispose();

        DamageLevelSystem =
            new DamageLevelSystem(damageLevelConfig, _eventBus, this, saves.DamageLevel, saves.DamageExp);
        HealthPointsLevelSystem = 
            new HealthPointsLevelSystem(healthPointsLevelConfig, _eventBus, healthPoints, saves.HealthLevel, saves.HealthExp);
        ArmorLevelSystem = 
            new ArmorLevelSystem(armorLevelConfig, _eventBus, this, saves.ArmorLevel, saves.ArmorExp);
        
        healthPoints.SetCurrentValue(saves.CurrentHealth);
        
        OnParamsLoad?.Invoke();
    }
    
    public void GameCycleUpdate() => HandleUpdate(Time.deltaTime);
    
    public override void TakeDamage(float damage)
    {
        if(IsDead) return;

        var dam = Mathf.Clamp(damage + FullTakeDamage - _armor, 0, float.MaxValue);
        healthPoints.ChangeCurrentValue(-dam);
        if (healthPoints.IsEmpty)
            OnDie?.Invoke();
        
        OnTakeDamage?.Invoke();
    }

    public void ChangeDamage(float newDamage)
    {
        attackDamage = newDamage;
    }
    
    public void ChangeArmor(float value)
    {
        _armor = value;
    }
    
    private void Move(float time)
    {
        OnMove?.Invoke(moveSpeed * time);
        _eventBus.Invoke(new PlayerHeroMove(moveSpeed * time));
    }

    private void Attack()
    {
        AttackCooldown.Reset();
        _enemyForFight.TakeDamage(FullAttackDamage);
        OnAttack?.Invoke();
        _eventBus.Invoke(new SwordUse());
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
        _eventBus?.UnSubscribe(this);
        _gameCycleController?.RemoveListener(GameCycleState.Gameplay, this);
    }
}
