using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : Interactable
{
    [SerializeField] protected bool canMove = true;
    [SerializeField][Range(0, 10)] protected float moveSpeed;
    [SerializeField][Range(0, 10)] protected int minSteps = 1;
    [SerializeField][Range(1, 10)] protected int maxSteps = 5;
    [SerializeField] protected float stepDelay = 0.5f;
    [SerializeField] protected float idleTime = 3f;
    protected bool isDefensive;
    protected bool isDead;
    protected SpriteRenderer _sr;
    protected Rigidbody2D _rb;
    protected bool _isFacingRight;

    protected virtual void Move() { }

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
        FlipSprite(newPos);

        for (int i = 0; i < stepCount; i++)
        {
            transform.position += step * newPos;
            yield return new WaitForSeconds(stepDelay);
        }
    }

    protected virtual void BounceCounter(int bounceCount, int maxBounces)
    {
        bounceCount++;
        if (bounceCount > maxBounces) bounceCount = 0;
    }

    protected virtual void Bounce(float bounceHeight, float bounceDuration, int direction)
    {
        float bounceVector = Mathf.Lerp(transform.position.y, bounceHeight, bounceDuration);

        Vector3 moveVector = new(direction, bounceVector, 0);

        transform.position += moveVector;
    }

    private void FlipSprite(Vector3 newPos)
    {
        _isFacingRight = newPos.x <= 0;

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
}
