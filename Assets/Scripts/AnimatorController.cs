using Entities;
using UnityEngine;

[RequireComponent(typeof(EntityBase))]
public class AnimatorController : MonoBehaviour
{
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    
    private EntityBase _entityBase;
    private Animator _animator;

    private void Start()
    {
        _entityBase = GetComponent<EntityBase>();
        _animator = GetComponentInChildren<Animator>();

        _entityBase.OnAttack += () => _animator.SetTrigger(Attack);
        _entityBase.OnTakeDamage += () => _animator.SetTrigger(TakeDamage);
    }
}