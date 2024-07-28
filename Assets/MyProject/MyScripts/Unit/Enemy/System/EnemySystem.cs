using System.Collections;
using UnityEngine;

public abstract class EnemySystem : MonoBehaviour, IEnemySystem
{
    [SerializeField] protected float deathDelay = 1;
    public int MaxHP { get { return Stats.BaseStats.MaxHP; } }
    public int CurrentStrength { get { return Stats.CurrentStats.CurrentStr; } }
    public float CurrentMS { get { return Stats.CurrentStats.CurrentMS; } }
    public float CurrentJumpHeight { get { return Stats.CurrentStats.CurrentJumpHeight; } }

    [Tooltip("Scriptable Object containing enemy stats.")]
    protected EnemyStats_SO Stats; // set by EnemySpawner

    public float CalculateAgility(int hp, float ms) => hp / ms;

    public float CalculateAttackSpeed(float baseAttackTime, float agility, float bonusAS) => baseAttackTime * (agility + bonusAS);

    public virtual void DropLoot()
    {
        /*Loot[] loot = lootSystem.GiveLoot();

        foreach (Loot item in loot) Debug.Log(name + ": Congrats! You recieved a reward! \n Reward item = " + item);
        Debug.LogWarning(name + ": No inventory system found.");*/
    }

    public virtual void TakeDamage(ObjectType type, int damage)
    {
        if (type == ObjectType.Enemy)
        {
            int cHP = Stats.CurrentStats.CurrentHP;
            cHP -= damage;
            if (cHP  < 0) cHP = 0;
            Stats.CurrentStats.CurrentHP = cHP;
        }
    }

    public virtual void DestroyInteractable(float time)
    {
        StartCoroutine(DestructionDelayTimer(time));
    }

    protected virtual IEnumerator DestructionDelayTimer(float time)
    {
        //DropLoot();
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void DestroyInteractable()
    {
        throw new System.NotImplementedException();
    }
}
