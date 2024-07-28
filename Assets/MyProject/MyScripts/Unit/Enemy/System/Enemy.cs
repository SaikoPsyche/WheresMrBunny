using UnityEngine;

public class Enemy : EnemySystem
{
    private Animator _animator;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        Stats.CurrentStats.CurrentHP = Stats.BaseStats.MaxHP;
    }

    public Enemy(EnemyStats_SO stats)
    {
        this.Stats = stats;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TakeDamage(ObjectType.Player, CurrentStrength);
        }
    }

    public override void TakeDamage(ObjectType type, int damage)
    {
        base.TakeDamage(type, damage);
        _animator.SetTrigger("Hit");
        if (Stats.CurrentStats.CurrentHP == 0)
        {
            _animator.SetTrigger("Dead");
            DestroyInteractable(deathDelay);
        }
    }

    public override void DestroyInteractable(float time)
    {
        base.DestroyInteractable(time);
    }
    public override void DropLoot()
    {
        base.DropLoot();
    }
}
