using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CharacterController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float groundedRadius = 0.1f;
    [SerializeField] private Transform groundedAnchor;
    [SerializeField] private GameObject projectile;

    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;
    public static bool isFacingRight;
    private Vector3 newPos;
    private Vector2 groundedDirection = Vector3.down;

    private Animator _animator;
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
        EventManager.OnGiveBuff += UpdateStats;
        EventManager.OnTeleport += Teleport;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        EventManager.OnGiveBuff -= UpdateStats;
        EventManager.OnTeleport -= Teleport;
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.SetCallbacks(this);
    }

    private void Start()
    {
        SetAnimatorController();

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
                    jumpHeight = PlayerSystem.PlayerManager.GetStat(BuffType.Jump);
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

    private void Move(Vector3 newPos)
    {
        float _speed = 0;

        if (isRunning)
        {
            moveSpeed = PlayerSystem.PlayerManager.GetStat(BuffType.MoveSpeed);
            _speed = moveSpeed * Time.deltaTime;
            FlipSprite(newPos);
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
    private bool Grounded()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(groundedAnchor.position, Vector3.down, groundedRadius);

        if (hit2D && hit2D.collider.IsTouchingLayers(3)) isGrounded = true; // Layer 3 = Ground Layer
        else isGrounded = false;

        //isGroundedCheck(isGrounded, hit2D);

        return isGrounded;
    }

    private void isGroundedCheck(bool isGrounded, RaycastHit2D hit2D)
    {
        Color groundedColor = Color.green;
        Color unGroundedColor = Color.red;

        Color displayColor = isGrounded ? groundedColor : unGroundedColor;

        Debug.DrawRay(groundedAnchor.position, groundedDirection, displayColor);
        Debug.Log(name + $"= Grounding ray collided with {hit2D.collider}, tag is {hit2D.collider.tag}");
    }

    // Make sprite face left if sprite is moving left.
    private void FlipSprite(Vector3 newPos)
    {
        isFacingRight = newPos.x >= 0;

        switch (isFacingRight)
        {
            case true:
                sr.flipX = false;
                break;
            case false:
                sr.flipX = true;
                break;
        }
    }

    private void SetAnimatorController()
    {
        if (PlayerSystem.PlayerAnimatorController != null || PlayerSystem.PlayerAnimOverrideController != null)
        {
            switch (PlayerSystem.PlayerManager.Player.Player.Character.IsOverrideController)
            {
                case true:
                    _animator.runtimeAnimatorController = PlayerSystem.PlayerAnimatorController;
                    break;
                case false:
                    _animator.runtimeAnimatorController = PlayerSystem.PlayerAnimOverrideController;
                    break;
            }
        }
        else if (PlayerSystem.PlayerAnimatorController == null || PlayerSystem.PlayerAnimOverrideController == null)
            Debug.Log(name + $": Animator Controller and Animator Override Controller do not exist. " +
                $"\nCharacter index is {PlayerSystem.PlayerManager.Player.Player.Character.Index}.");
    }

    private void UpdateStats(float amount, BuffType buff)
    {
        float total = PlayerSystem.PlayerManager.GetStat(buff);

        switch (buff)
        {
            case BuffType.None: break;
            case BuffType.Health: break;
            case BuffType.Strength: break;
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
                transform.localScale = new Vector3(total, total);
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
