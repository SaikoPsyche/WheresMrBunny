using System.Collections;
using UnityEngine;

public class EnemyMovement : UnitMovement
{
    [SerializeField] protected EnemyStats_SO enemyStats;
    [SerializeField] protected int defOffTimer = 1;
    [SerializeField] protected int defOnTimer = 1;
    [SerializeField] protected float hitMoveMultiplier;
    readonly string _isDefensiveText = "IsDefensive";


    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        isDead = false;
    }

    private void Start()
    {
        _unitHealth = enemyStats.MaxHP;
        StartCoroutine(IdleTimer());
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Play Animation
            _animator.SetBool(_isDefensiveText, isDefensive = true);
        }
    }*/

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            StartCoroutine(DefenseTimer(DefensiveChance(0, 101, 49)));
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Play Animation
            if (isDefensive == false)
            {
                _animator.SetTrigger("Hit");
                TakeDamage(Player.PlayerSystem.Player.CurrentStr);
            }
            else 
            {
                // knock back
                // player take dmg
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Play Animation
            if (_animator.GetBool(_isDefensiveText) == true)
                _animator.SetBool(_isDefensiveText, isDefensive = false);
        }
    }

    protected override void Move()
    {
        int numberOfSteps = Random.Range(minSteps, maxSteps + 1);
        StartCoroutine(StepTimer(numberOfSteps, enemyStats.CurrentMS));
    }

    private IEnumerator DefenseTimer(bool isDeffensive)
    {
            //yield return new WaitForSeconds(DefensiveType(isDeffensive));

            _animator.SetBool(_isDefensiveText, isDeffensive);

            yield return new WaitForSeconds(DefensiveType(isDeffensive));
    }

    protected int DefensiveType(bool isDefensive)
    {
        int timerType;
        if (isDefensive == true) timerType = defOffTimer;
        else timerType = defOnTimer;
        return timerType;
    }

    protected bool DefensiveChance(int minChance, int maxChance, int maxForTrue)
    {
        int isDefensiveChance = Random.Range(minChance, maxChance);
        isDefensive = isDefensiveChance <= maxForTrue ? true : false;

        return isDefensive;
    }

    protected virtual void TakeDamage(int damage)
    {
        _unitHealth -= damage;
        
        if (_unitHealth <= 0)
        {
            _unitHealth = 0;
            DestroyInteractable(gameObject, 0.5f, "IsDead");
        }
    }
}
