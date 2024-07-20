using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{
    [SerializeField] private int index;

    private TextMeshProUGUI _playerNameInput;
    private TextMeshProUGUI _playerNamePlaceholder;

    private void Awake()
    {
        _playerNameInput = GameObject.FindWithTag("PlayerNameInput").GetComponent<TextMeshProUGUI>();
        _playerNamePlaceholder = GameObject.FindWithTag("PlayerTextPlaceholder").GetComponent<TextMeshProUGUI>();
    }

    public void SetCharacterIndex()
    {
        EventManager.ChooseCharacter(index);
    }

    public void VerifyPlayerName()
    {
        string warningText = "Invalid name. \nDefault name will be used.";
        Vector3 position = new(Screen.width / 2, Screen.height / 2);
        Regex regex = new("([a-zA-Z_][a-zA-Z0-9_]*){5,12}");
        string _name = _playerNamePlaceholder.text;

        if (regex.IsMatch(_playerNameInput.text)) _name = _playerNameInput.text;
        else WarningManager.Instance.DisplayWarning(position, warningText);

        SetPlayerName(_name);
    }

    public void SetPlayerName(string name)
    {
        EventManager.ChooseName(name);
    }
}
