using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private HPImageState hpState;
    private PlayerStats_SO _player;
    private TextMeshProUGUI _playerNameText;
    private TextMeshProUGUI _characterNameText;
    private HPPercent hpPercent;
    private Image _hpGauge;
    readonly int _scene;

    private void Awake()
    {
        _playerNameText = GameObject.FindWithTag("PlayerNameText").GetComponent<TextMeshProUGUI>();
        _characterNameText = GameObject.FindWithTag("CharacterNameText").GetComponent<TextMeshProUGUI>();
        _hpGauge = GameObject.FindWithTag("HPBar").GetComponent<Image>();
    }

    public void UpdatePlayerName(string name) { Debug.Log(this.name + ": Player's name is " + name); _playerNameText.text = name; }

    public void UpdateCharacterName(string name) { Debug.Log(this.name + ": Character is " + name); _characterNameText.text = name; }

    public void UpdateHP(HPPercent hpPercent)
    {
        Sprite sprite = hpState.FullHealthSprite;

        switch(hpPercent)
        {
            case HPPercent.FullHealth:
                sprite = hpState.FullHealthSprite;
                break;
            case HPPercent.Health75:
                sprite = hpState.Health75Sprite;
                break;
            case HPPercent.HalfHealth:
                sprite = hpState.HalfHealthSprite;
                break;
            case HPPercent.Health25:
                sprite = hpState.Health25Sprite;
                break;
            case HPPercent.Dead:
                sprite = hpState.DeadSprite;
                break;
        }
        _hpGauge.sprite = sprite;
    }
}
