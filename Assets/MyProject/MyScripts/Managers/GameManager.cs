using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    public PlayerStats_SO Player;
    private string saveName;

    private GameObject _startScreenCanvas;
    private GameObject _newPlayerCanvas;

    private int _scene;
    private GameState _gameState;

    private bool _isNewAccount = true;
    private int _numberOfAccounts = 0;

    private void OnEnable()
    {
        EventManager.Instance.OnSaveGame += SaveGame;
        EventManager.Instance.OnLoadGame += LoadGame;
        EventManager.Instance.OnChooseName += GetPlayerName;
        EventManager.Instance.OnGameStateChange += GameStateChanged;
        SceneManager.activeSceneChanged += LevelChanged;
        EventManager.Instance.OnChooseCharacter += GetCharacter;

    }

    private void OnDisable()
    {
        EventManager.Instance.OnSaveGame -= SaveGame;
        EventManager.Instance.OnLoadGame -= LoadGame;
        EventManager.Instance.OnGameStateChange += GameStateChanged;
        EventManager.Instance.OnChooseName -= GetPlayerName;
        SceneManager.activeSceneChanged -= LevelChanged;
        EventManager.Instance.OnChooseCharacter -= GetCharacter;

    }

    private void Awake()
    {
        // UI
        _startScreenCanvas = GameObject.FindWithTag("StartScreen");
        _newPlayerCanvas = GameObject.FindWithTag("NewPlayer");
        _newPlayerCanvas.SetActive(false);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(ShowStartScreen());
    }

    #region Scene Manager

    private void GameStateChanged(GameState state)
    {
        _gameState = state;

        switch (_gameState)
        {
            case GameState.Setup:
                Debug.Log("Setup...");
                //CreatePlayer();
                StartSceneTransition();
                break;
            case GameState.Transition:
                Debug.Log("Transitioning...");
                StartCoroutine(SceneTransitionTimer());
                break;
            case GameState.GameStart:
                Debug.Log("Game Starting...");
                EventManager.Instance.LoadGame();
                // Walk through
                break;
            case GameState.NewScene:
                Debug.Log("New Scene...");
                EventManager.Instance.LoadGame();
                break;
            case GameState.Paused:
                Debug.Log("Paused...");
                break;
            case GameState.GameOver:
                Debug.Log("GameOver...");
                break;
        }
    }

    private void LevelChanged(Scene currentScene, Scene newScene)
    {
        _scene = newScene.buildIndex;

        if (newScene.buildIndex == 1)
            EventManager.Instance.GameStateChange(GameState.GameStart);
        else if (newScene.buildIndex > 1)
            EventManager.Instance.GameStateChange(GameState.NewScene);

        Debug.Log(name + $": Current Scene = Scene {_scene} New Scene = Scene {newScene.buildIndex}");
    }

    public void GameSetUp()
    {
        EventManager.Instance.GameStateChange(GameState.Setup);
    }

    private IEnumerator SceneTransitionTimer()
    {
        //SaveGame(Player);
        // Start Aniimation

        yield return new WaitForSeconds(transitionTime);

        if (_scene <= SceneManager.sceneCount)
            SceneManager.LoadScene(_scene + 1);
        if (_scene >= SceneManager.sceneCount)
            EventManager.Instance.GameStateChange(GameState.GameOver);
    }

    public void StartSceneTransition()
    {
        GameStateChanged(GameState.Transition);
    }

    #endregion

    #region Character Manager

    private void GetCharacter(int id)
    {

    }

    #endregion

    #region UI Manager

    private IEnumerator ShowStartScreen()
    {
        _startScreenCanvas.SetActive(false);

        yield return new WaitForSeconds(0.55f); // use variable

        _startScreenCanvas.SetActive(true);
    }

    public void GotoPlayerCreation()
    {
        _startScreenCanvas.SetActive(false);
        _newPlayerCanvas.SetActive(true);
    }

    #endregion

    #region Inventory Manager
    #endregion

    #region FX Manager

    #region Particles
    #endregion

    #region Visual
    #endregion

    #endregion

    #region Audio Manager

    #region Music
    #endregion

    #region Sound FX
    #endregion

    #endregion

    private void CreatePlayer()
    {
        try
        {
            PlayerStats_SO character = (PlayerStats_SO)CharacterManager.Instance.Character;
            Player = character;
        }
        catch (NullReferenceException nullRE)
        {
            Debug.Log(name + $": Cannot set player data. " +
                $"\nError Type = {nullRE.GetType()}" +
                $"\nException = {nullRE.Message}");
        }
    }

    private void GetPlayerName(string playerName) => Player.Name = playerName;

    public void CloseUI(GameObject go)
    {
        go.SetActive(false);
    }

    public void SaveGame(PlayerStats_SO playerData)
    {
        GameStateChanged(GameState.SaveGame);
        saveName = Player.Name + "PlayerData";
        ES3.Save(saveName, playerData);
        Debug.Log(name + ": Game Saved Successfully");
        Debug.Log(name + $": Saved Player Name = {Player.Name}, Saved Character = {Player.CharacterName}");
    }

    public void LoadGame()
    {
        Player = (PlayerStats_SO)ES3.Load(saveName);
        Debug.Log(name + ": Game Loaded Successfully");
        Debug.Log(name + $": Loaded Player Name = {Player.Name}, Loaded Character = {Player.CharacterName}");
    }
}
