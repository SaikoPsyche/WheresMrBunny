using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static PlayerSystem _playerSystem;
    private GameManager _gameManager;
    private SpriteRenderer _playerRenderer;

    public static AnimatorOverrideController PlayerAnimOverrideController
    {
        get { return _playerSystem.Player.AnimOverrideController; }
    }

    public static AnimatorController PlayerAnimatorController
    {
        get { return _playerSystem.Player.AnimatorController; }
    }

    public static PlayerSystem PlayerSystem { get { return _playerSystem; } }

    private void OnEnable()
    {
        EventManager.OnGiveBuff += RecieveBuffs;
        EventManager.OnChangeScore += GetPlayerScore;
        EventManager.OnGameStateChange += UpdatePlayer;
        EventManager.OnPlayerHealth += PlayerDeath;
    }

    private void OnDisable()
    {
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
        _playerRenderer.sprite = _playerSystem.Player.Sprite;

    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1); // make damage variable
        }
    }*/

    public void GetPlayerName(string playerName) => _playerSystem.Player.Name = playerName;

    public void UpdatePlayer(GameState gameState)
    {
        if (_playerSystem == null)
        {
            _playerSystem = new()
            {
                Player = _gameManager.Player
            };

            _playerSystem.UpdatePlayer(gameState);

            Debug.Log(name + ": Player Name = " + _playerSystem.Player.Name);
        }
        else
        {
            _playerSystem.Player = _gameManager.Player;
            int playerLevel = SceneManager.GetActiveScene().buildIndex;
            _playerSystem.UpdatePlayer(gameState);

            Debug.Log(name + ": Player Name = " + _playerSystem.Player.Name);
        }
    }

    public void GetPlayerScore(int score) => _playerSystem.PlayerScore = score;

    

    private void RecieveBuffs(float amount, BuffType buff)
    {
        _playerSystem.RecieveBuffs(amount, buff);
    }

    /*private void TakeDamage(int damage)
    {
        _playerManager.TakeDamage(damage);
    }*/

    private void PlayerDeath(HPPercent playerHealth)
    {
        if (playerHealth == HPPercent.Dead)
        {

            Destroy(this); // make better kill method
        }
    }
}

