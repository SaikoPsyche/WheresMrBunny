using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private HPImageState hpState;
    private HPPercent hpPercent;
    private Image _hpGauge;
    private TextMeshProUGUI _playerNameText;
    private TextMeshProUGUI _characterNameText;
    private PlayerStats_SO _player;
    readonly int _scene;


    private void OnEnable()
    {
        EventManager.OnPlayerHealth += UpdateHP;
        EventManager.OnGameStateChange += UpdatePlayer;
        EventManager.OnSaveGame += SetPlayer;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerHealth -= UpdateHP;
        EventManager.OnGameStateChange -= UpdatePlayer;
        EventManager.OnSaveGame -= SetPlayer;
    }

    private void Awake()
    {
        _playerNameText = GameObject.FindWithTag("PlayerNameText").GetComponent<TextMeshProUGUI>();
        _characterNameText = GameObject.FindWithTag("CharacterNameText").GetComponent<TextMeshProUGUI>();
        _hpGauge = GameObject.FindWithTag("HPBar").GetComponent<Image>();
    }

    private void Start()
    {
        _player = PlayerSystem.PlayerManager.Player;
        UpdatePlayerName();
        UpdateCharacterName();
        Debug.Log(name + $": Player name = {_player.Player.Name}");
    }

    private void SetPlayer(PlayerStats_SO player) => _player = player;

    public void UpdatePlayerName() { Debug.Log(_player.Player.Name); _playerNameText.text = _player.Player.Name; }

    public void UpdateCharacterName() => _characterNameText.text = _player.Player.Character.Name;

    public void UpdateHP(HPPercent hpPercent)
    {
        Image image = hpState.FullHealthSprite;

        switch(hpPercent)
        {
            case HPPercent.FullHealth:
                image = hpState.FullHealthSprite;
                break;
            case HPPercent.Health75:
                image = hpState.Health75Sprite;
                break;
            case HPPercent.HalfHealth:
                image = hpState.HalfHealthSprite;
                break;
            case HPPercent.Health25:
                image = hpState.Health25Sprite;
                break;
            case HPPercent.Dead:
                image = hpState.DeadSprite;
                break;
        }
        _hpGauge = image;
    }

    public void UpdatePlayer(GameState gameState)
    {
        if (gameState == GameState.NewScene)
        {
            UpdatePlayerName();
            UpdateCharacterName();
            Debug.Log(name + $": Player name = {_player.Player.Name}");
        }
    }
}


[Serializable]
public enum HPPercent
{
    FullHealth,
    Health75,
    HalfHealth,
    Health25,
    Dead,
}

[Serializable]
public struct HPImageState
{
    public Image FullHealthSprite;
    public Image Health75Sprite;
    public Image HalfHealthSprite;
    public Image Health25Sprite;
    public Image DeadSprite;
}
