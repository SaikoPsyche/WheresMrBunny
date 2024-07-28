using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : PlayerSystem
{
    private GameManager _gameManager;
    private SpriteRenderer _sr;
    private Animator _animator;
    public bool isFacingRight;

    public AnimatorOverrideController PlayerAnimOverrideController
    {
        get { return Player.AnimOverrideController; }
    }

    public AnimatorController PlayerAnimatorController
    {
        get { return Player.AnimatorController; }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnGiveBuff += RecieveBuffs;
        EventManager.Instance.OnChangeScore += SetPlayerScore;
        //EventManager.OnGameStateChange += UpdatePlayer;
        EventManager.Instance.OnTakeDamage += TakeDamage;
        EventManager.Instance.OnChangeScore += SetPlayerScore;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGiveBuff -= RecieveBuffs;
        EventManager.Instance.OnChangeScore -= SetPlayerScore;
        //EventManager.OnGameStateChange -= UpdatePlayer;
        EventManager.Instance.OnTakeDamage -= TakeDamage;
        EventManager.Instance.OnChangeScore -= SetPlayerScore;
    }

    private void Awake()
    {
        _gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _sr.sprite = Player.Sprite;

    }

    public Player () { SetCharacterDetails(); }

    public Player(string name) { PlayerName = name; SetCharacterDetails(); }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Enemy>())
        {
            TakeDamage(ObjectType.Enemy, Player.CurrentStats.CurrentStr); // make damage variable
        }
    }

    public void GetPlayerName(string playerName) => PlayerName = playerName;

    protected override void SetCharacterDetails() { base.SetCharacterDetails(); }

    public override void UpdatePlayer() { base.UpdatePlayer(); }

    public override void SetPlayerScore(int score) { base.SetPlayerScore(score); }

    public override void RecieveBuffs(float amount, BuffType buff) { base.RecieveBuffs(amount, buff); UpdatePlayer(); }// update player movement stats

    public override void TakeDamage(ObjectType type, int damage) 
    {
        if (type == ObjectType.Player)
        {
            base.TakeDamage(type, damage);
            _animator.SetTrigger("Hit");
            if (CurrentHP == 0)
            {
                _animator.SetTrigger("Dead");
                DestroyInteractable(deathDelay);
            }
        }
    }

    public override void DestroyInteractable()
    {
        // Death animation
        _animator.SetTrigger("Dead");
        // Ask if player wishes to restart

        string message = "Would you like to restart?";
        WarningManager.Instance.DisplayWarning(null, message);
        // restart or go back to start streen based on decision
    }

    private void FlipSprite(Vector3 newPos)
    {
        isFacingRight = newPos.x >= 0;

        switch (isFacingRight)
        {
            case true:
                _sr.flipX = false;
                break;
            case false:
                _sr.flipX = true;
                break;
        }
    }

    private void SetAnimatorController()
    {
        if (PlayerAnimatorController != null || PlayerAnimOverrideController != null)
        {
            switch (Player.IsOverrideController)
            {
                case true:
                    _animator.runtimeAnimatorController = PlayerAnimatorController;
                    break;
                case false:
                    _animator.runtimeAnimatorController = PlayerAnimOverrideController;
                    break;
            }
        }
        else if (PlayerAnimatorController == null || PlayerAnimOverrideController == null)
            Debug.Log(name + $": Animator Controller and Animator Override Controller do not exist. " +
                $"\nCharacter index is {Player.ID}.");
    }
}

