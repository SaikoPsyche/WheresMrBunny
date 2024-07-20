using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSystem : MonoBehaviour
{
    private static PlayerManager _playerManager;
    private GameManager _gameManager;
    private SpriteRenderer _playerRenderer;

    public static AnimatorOverrideController PlayerAnimOverrideController
    {
        get { return _playerManager.Player.Player.Character.AnimOverrideController; }
    }

    public static AnimatorController PlayerAnimatorController
    {
        get { return _playerManager.Player.Player.Character.AnimatorController; }
    }

    public static PlayerManager PlayerManager { get { return _playerManager; } }

    private void OnEnable()
    {
        EventManager.OnCollectGems += CountGems;
        EventManager.OnGiveBuff += RecieveBuffs;
        EventManager.OnChangeScore += GetPlayerScore;
        EventManager.OnGameStateChange += UpdatePlayer;
        EventManager.OnPlayerHealth += PlayerDeath;
    }

    private void OnDisable()
    {
        EventManager.OnCollectGems -= CountGems;
        EventManager.OnGiveBuff -= RecieveBuffs;
        EventManager.OnChangeScore -= GetPlayerScore;
        EventManager.OnGameStateChange -= UpdatePlayer;
        EventManager.OnPlayerHealth -= PlayerDeath;
    }

    private void Awake()
    {
        _gameManager = GameObject.FindWithTag("Managers").GetComponent<GameManager>();
        _playerRenderer = GetComponentInChildren<SpriteRenderer>();
        DontDestroyOnLoad(this);
        UpdatePlayer(GameState.SaveGame);
    }

    private void Start()
    {
        _playerRenderer.sprite = _playerManager.Player.Player.Character.Sprite;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); // make damage variable
        }
    }

    public void GetPlayerName(string playerName) => _playerManager.PlayerName = playerName;

    public void UpdatePlayer(GameState gameState)
    {
        if (_playerManager == null)
        {
            _playerManager = new()
            {
                Player = _gameManager.Player
            };

            int playerLevel = SceneManager.GetActiveScene().buildIndex;
            _playerManager.UpdatePlayer(gameState, name);

            Debug.Log(name + ": Player Name = " + _playerManager.Player.Player.Name);
        }
        else
        {
            _playerManager.Player = _gameManager.Player;
            int playerLevel = SceneManager.GetActiveScene().buildIndex;
            _playerManager.UpdatePlayer(gameState, name);

            Debug.Log(name + ": Player Name = " + _playerManager.Player.Player.Name);
        }
    }

    public void GetPlayerScore(int score) => _playerManager.PlayerScore = score;

    public void CountGems(int count, BuffType gemType)
    {
        switch (gemType)
        {
            case BuffType.Strength:
                _playerManager.StrengthGemCount = count;
                break;
            case BuffType.Health:
                _playerManager.HealthGemCount = count;
                break;
            case BuffType.Jump:
                _playerManager.JumpGemCount = count;
                break;
            case BuffType.MoveSpeed:
                _playerManager.MSGemCount = count;
                break;
            case BuffType.AttackSpeed:
                _playerManager.ASGemCount = count;
                break;
            case BuffType.Grow:
                _playerManager.GrowGemCount = count;
                break;
            case BuffType.Shrink:
                _playerManager.ShrinkGemCount = count;
                break;
        }
        Debug.Log(gemType + $"Gem: Gem count = {count}");
    }

    private void RecieveBuffs(float amount, BuffType buff)
    {
        _playerManager.RecieveBuffs(amount, buff);
    }

    private void TakeDamage(int damage)
    {
        _playerManager.TakeDamage(damage);
    }

    private void PlayerDeath(HPPercent playerHealth)
    {
        if (playerHealth == HPPercent.Dead)
        {
            Destroy(this); // make better kill method
        }
    }
}

