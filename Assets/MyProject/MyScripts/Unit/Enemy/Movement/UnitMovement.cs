using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour, IMovement
{
    [SerializeField] protected bool canMove = true;
    [SerializeField][Range(0, 10)] protected float moveSpeed;
    [SerializeField][Range(0, 10)] protected int minSteps = 1;
    [SerializeField][Range(1, 10)] protected int maxSteps = 5;
    [SerializeField] protected float stepDelay = 0.5f;
    [SerializeField] protected float jumpHeight;
    [SerializeField] protected float idleTime = 3f;
    protected bool isDefensive;
    protected bool isDead;
    protected SpriteRenderer _sr;
    protected Rigidbody2D _rb;
    protected Animator _animator;
    protected bool _isFacingRight;

    protected virtual int ChooseDirection()
    {
        int direction = Random.Range(-1, 2);

        return direction;
    }

    protected virtual IEnumerator IdleTimer()
    {
        while (isDead == false)
        {
            Move();

            yield return new WaitForSeconds(idleTime);
        }
    }

    protected IEnumerator StepTimer(int stepCount, float moveSpeed)
    {
        float step = moveSpeed * Time.deltaTime;
        Vector3 newPos = new(ChooseDirection(), 0);
        FlipSprite(newPos.x);

        for (int i = 0; i < stepCount; i++)
        {
            transform.position += step * newPos;
            yield return new WaitForSeconds(stepDelay);
        }
    }

    public virtual void FlipSprite(float direction)
    {
        _isFacingRight = direction <= 0;

        switch (_isFacingRight)
        {
            case true:
                _sr.flipX = false;
                break;
            case false:
                _sr.flipX = true;
                break;
        }
    }

    public virtual bool Grounded()
    {
        throw new System.NotImplementedException();
    }

    public virtual void IsGroundedCheck(bool isGrounded, RaycastHit2D hit2D)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Move()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Move(Vector3 newPos)
    {
        throw new System.NotImplementedException();
    }
}
