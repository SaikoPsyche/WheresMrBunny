using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : UnitMovement, PlayerInputActions.IPlayerActions
{
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundedRadius = 0.1f;
    [SerializeField] private Transform groundedAnchor;
    [SerializeField] private GameObject projectile;

    private PlayerInputActions playerInputActions;
    private Vector3 newPos;
    private Vector2 groundedDirection = Vector3.down;

    private bool isRunning = false;
    private bool isJumping = false;

   /*private bool isDoubleJumping = false;
    private bool isWallJumping = false; 
    private bool isDead = true;*/

    /*private int isRunning_animatorHashCode;
    private int isJumping_animatorHashCode;
    private int isDoubleJumping_animatorHashCode;
    private int isWallJumping_animatorHashCode;
    private int hit_animatorHashCode;
    private int isDead_animatorHashCode;*/

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.SetCallbacks(this);
    }

    private void Start()
    {
        

        //AnimatorHashCodes();
    }

    private void Update()
    {
        Grounded();
    }

    private void FixedUpdate()
    {
        // Player Movement
        newPos = playerInputActions.Player.Run.ReadValue<Vector2>();
        isRunning = newPos != Vector3.zero;

        Move(newPos);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameObject projectile = PoolSpawner.Instance.SpawnPooledObjects();
            projectile.transform.position = transform.position;
            projectile.SetActive(true);

            Debug.Log("Fire!");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (isGrounded)
                {
                    //jumpHeight = _player.GetStat(BuffType.Jump);
                    transform.position += new Vector3(0, jumpHeight, 0);
                    isJumping = true;
                }
                break;
            case InputActionPhase.Canceled:
                if (!isGrounded)
                {
                    isJumping = false;
                }
                break;
        }

        _animator.SetBool("IsJumping", isJumping);
    }

    public override void Move(Vector3 newPos)
    {
        float _speed = 0;

        if (isRunning)
        {
            //moveSpeed = _player.GetStat(BuffType.MoveSpeed);
            _speed = moveSpeed * Time.deltaTime;
        }

        transform.position += newPos * _speed;

        _animator.SetBool("IsRunning", isRunning);
    }

    public void Teleport(TransitionType type, Vector3 nextPos)
    {
        if (type == TransitionType.InScene) transform.position = nextPos;

        Debug.Log(name + $": Teleporting to {nextPos}!");
    }

    #region Helper Methods
    public override bool Grounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(groundedAnchor.position, Vector3.down, groundedRadius);

        if (hit2D && hit2D.collider.IsTouchingLayers(3)) isGrounded = true; // Layer 3 = Ground Layer
        else isGrounded = false;

        //isGroundedCheck(isGrounded, hit2D);

        return isGrounded;
    }

    public override void IsGroundedCheck(bool isGrounded, RaycastHit2D hit2D)
    {
        Color groundedColor = Color.green;
        Color unGroundedColor = Color.red;

        Color displayColor = isGrounded ? groundedColor : unGroundedColor;

        Debug.DrawRay(groundedAnchor.position, groundedDirection, displayColor);
        Debug.Log(name + $"= Grounding ray collided with {hit2D.collider}, tag is {hit2D.collider.tag}");
    }

    

    private void UpdateStats(float amount, BuffType buff, float total)
    {
        switch (buff)
        {
            case BuffType.AttackSpeed:
                attackSpeed = total;
                break;
            case BuffType.MoveSpeed:
                moveSpeed = total;
                break;
            case BuffType.Jump:
                jumpHeight = total;
                break;
            case BuffType.Shrink:
                transform.localScale /= 2;
                break;
            case BuffType.Grow:
                transform.localScale *= 2;
                break;
        }
    }

    /*private void AnimatorHashCodes()
    {
        isRunning_animatorHashCode = Animator.StringToHash("IsRunning");
        isJumping_animatorHashCode = Animator.StringToHash("IsJumping");
        isDoubleJumping_animatorHashCode = Animator.StringToHash("IsDoubleJumping");
        isWallJumping_animatorHashCode = Animator.StringToHash("IsWallJumping");
        hit_animatorHashCode = Animator.StringToHash("Hit");
        isDead_animatorHashCode = Animator.StringToHash("IsDead");
    }*/

    #endregion

    public void OnRun(InputAction.CallbackContext context) {}
}
