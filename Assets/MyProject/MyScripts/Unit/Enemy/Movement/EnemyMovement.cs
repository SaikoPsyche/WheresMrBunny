using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement : UnitMovement
{
    [SerializeField] private int maxIdleTime;
    [SerializeField] private int maxWalkTime;
    [SerializeField] private int targetStayDelay = 50;
    [SerializeField] private int followDelay = 4;
    [SerializeField] protected float knockBackMultiplier;

    private bool _isWandering = false;

    private Transform _target = null;
    private bool _facingTarget = false;
    private bool _followingTarget = false;
    private bool _targetFound;
    private int _targetLotto;

    public bool NoticeTarget
    {
        get
        {
            if (_targetFound)
            {
                _targetLotto = Random.Range(0, 101);
            }

            if (_targetLotto > 20)
                return true;
            else
                return false;
        }
    }

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        isDead = false;
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D col) => _target = col.transform;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            StartCoroutine(TargetStayTimer());
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _targetFound = false;
            _followingTarget = false;
            _target = null;
        }
    }

    public override void Move()
    {
        if (_isWandering == false && _targetFound == false)
            StartCoroutine(IdleTimer());
    }

    protected override IEnumerator IdleTimer()
    {
        int direction = Random.Range(-1, 2);
        int stepCount = Random.Range(minSteps, maxSteps + 1);
        int walkWait = Random.Range(1, maxWalkTime);
        int walkTime = Random.Range(1, maxIdleTime);

        _isWandering = true;

        yield return new WaitForSeconds(walkWait);

        StartCoroutine(StepTimer(stepCount, moveSpeed));

        yield return new WaitForSeconds(walkTime);

        FlipSprite(direction);

        _isWandering = false;

        if (_targetFound == false)
        {
            yield return new WaitForSeconds(walkWait);
            _isWandering = true;
        }
    }

    private IEnumerator TargetStayTimer()
    {
        bool m_foundTarget = _targetFound;
        int m_targetStayDelay = Random.Range(2, targetStayDelay);

        while (m_foundTarget)
        {
            yield return new WaitForSeconds(m_targetStayDelay);

            StartCoroutine(FollowTargetTimer());
        }
    }

    private IEnumerator FollowTargetTimer()
    {
        int m_followDelay = Random.Range(0, followDelay);
        Vector3 direction = Vector3.zero;

        if (NoticeTarget == true && _target != null)
        {
            if (!_facingTarget)
            {
                _isWandering = false;

                // face target
                direction = transform.position - _target.position;
                if (direction.x < 0) FlipSprite(-1);
                else FlipSprite(1);

                _facingTarget = true;

                Debug.Log($"Facing target: {_target.name}");

                yield return new WaitForSeconds(m_followDelay);
            }
            else
                if (!_followingTarget)
                {
                    FollowTarget(direction.x);
                    _followingTarget = true;

                    Debug.Log($"Following target: {_target.name}");
                }
        }
    }

    public void FollowTarget(float direction)
    {
        transform.position += moveSpeed * Time.deltaTime * new Vector3(direction, 0);

        Debug.Log("Following player.");
    }
}
