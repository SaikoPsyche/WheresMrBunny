using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class WarningManager : MonoBehaviour
{
    public static WarningManager Instance;

    [SerializeField] private GameObject warningUI;
    private TextMeshProUGUI warningText;
    private Button okButton;
    private Button cancelButton;
    private GameManager gameManager;

    readonly ButtonType button;

    private void Awake()
    {
        Instance = this;
        gameManager = GetComponent<GameManager>();
    }

    private void Start()
    {
        warningUI = Instantiate(warningUI);
        SetButtonRefernces(warningUI);
        warningUI.SetActive(false);
    }

    public void DisplayWarning(Vector3? pos, string message)
    {
        if (pos == null) pos = new(Screen.width / 2, Screen.height / 2);

        warningUI.SetActive(true);
        warningUI.transform.position = (Vector3)pos;
        warningUI.transform.rotation = Quaternion.identity;
        warningText = warningUI.GetComponentInChildren<TextMeshProUGUI>();
        warningText.text = message;
    }

    public void DisplayWarning(Vector3? pos, string message, bool showOk, bool showCancel)
    {
        DisplayWarning(pos, message);
        warningUI.transform.Find("OKButton").gameObject.SetActive(showOk);
        warningUI.transform.Find("CancelButton").gameObject.SetActive(showCancel);
    }


    private void SetButtonRefernces(GameObject warningUI) // Make more reusable
    {
        Debug.Log(name + $": Current button is {button}");

        if (GameObject.Find("CancelButton")) cancelButton = GameObject.Find("CancelButton").GetComponent<Button>();
        if (GameObject.Find("OKButton")) okButton = GameObject.Find("OKButton").GetComponent<Button>();

        Debug.Log(name + $": Ok Button = {okButton}");
        Debug.Log(name + $": Cancel button = {cancelButton}");
        
        
        UnityAction cancelAction = () => gameManager.CloseUI(warningUI);
        cancelButton.onClick.AddListener(cancelAction);
        Debug.Log(name + ": Destroying warning UI.");

        UnityAction okAction = () => EventManager.Instance.GameStateChange(GameState.Setup);
        okButton.onClick.AddListener(okAction);
        Debug.Log(name + ": Using default name.");
    }
}

