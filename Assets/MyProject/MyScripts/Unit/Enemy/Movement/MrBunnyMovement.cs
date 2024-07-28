using UnityEngine;

public class MrBunnyMovement : UnitMovement
{
    [SerializeField] private float bounceHeight = 2f;
    [SerializeField] private float bounceDuration = 1f;
    [SerializeField] private int maxBounces = 2;
    [SerializeField] private Transform exitPoint;
    readonly int bounceCount = 0;
    private bool isFleeing = false;

    private void Awake()
    {
        isDead = false;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Play Animation
            // Mr Bunny Leaves
            isFleeing = true;
            if (_sr.flipX == false) _sr.flipX = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Passage"))
        {
            //float lerpMove = Mathf.Lerp(0, 1, idleTime);
            _animator.SetFloat("IsRunning", 0);
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override int ChooseDirection()
    {
        int direction = 0;

        if (bounceCount == maxBounces)
        {
            direction = Random.Range(-1, 2);

            if (direction == -1) _sr.flipX = false;
            else if (direction == 1) _sr.flipX = true;
        }

        return direction;
    }

    public override void Move()
    {
        if (isFleeing == true)
        {
            float lerpMove = Mathf.Lerp(0, 1, 100);
            _animator.SetFloat("IsRunning", lerpMove);

            /*transform.Translate(new Vector3(
                    Mathf.Lerp(transform.position.x, exitPoint.position.x, 100),
                    transform.position.y) *
                    Time.deltaTime);*/

            _rb.position += moveSpeed * Time.fixedDeltaTime * new Vector2(Mathf.Lerp(transform.position.x, exitPoint.position.x, 100), _rb.position.y);
        }
    }
}
