using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GetPlayerData : MonoBehaviour
{
    [SerializeField] private int index = -1;

    private TextMeshProUGUI _playerNameInput;
    private TextMeshProUGUI _playerNamePlaceholder;

    private void Awake()
    {
        _playerNameInput = GameObject.FindWithTag("PlayerNameInput").GetComponent<TextMeshProUGUI>();
        _playerNamePlaceholder = GameObject.FindWithTag("PlayerTextPlaceholder").GetComponent<TextMeshProUGUI>();
    }

    public void SetCharacterIndex()
    {
        string warningText = "Please Choose a Character";
        if (index > -1) EventManager.Instance.ChooseCharacter(index);
        // else WarningManager.Instance.DisplayWarning(null, warningText, false, true);
    }

    public void VerifyPlayerName()
    {
        string warningText = "Invalid name. \nDefault name will be used.";
        Regex regex = new("([a-zA-Z0-9_][a-zA-Z0-9_]*){1,12}");
        string _name = _playerNamePlaceholder.text;

        if (regex.IsMatch(_playerNameInput.text)) _name = _playerNameInput.text;
        else WarningManager.Instance.DisplayWarning(null, warningText);

        EventManager.Instance.ChooseName(_name);
    }

}
