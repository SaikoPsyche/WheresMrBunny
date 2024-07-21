using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    public PlayerStats_SO Player;
    public static int GemCount;
    private string saveName;

    private GameObject _startScreenCanvas;
    private GameObject _newPlayerCanvas;

    private int _scene;
    private GameState _gameState;

    

    private void OnEnable()
    {
        //EventManager.OnSaveGame += SaveGame;
        //EventManager.OnLoadGame += LoadGame;
        EventManager.OnChooseName += GetPlayerName;
        EventManager.OnGameStateChange += GameStateChanged;
        SceneManager.activeSceneChanged += LevelChanged;
    }

    private void OnDisable()
    {
        //EventManager.OnSaveGame -= SaveGame;
        //EventManager.OnLoadGame -= LoadGame;
        EventManager.OnGameStateChange += GameStateChanged;
        EventManager.OnChooseName -= GetPlayerName;
        SceneManager.activeSceneChanged -= LevelChanged;
    }

    private void Awake()
    {
        // Start Game Scene
        _scene = SceneManager.GetActiveScene().buildIndex;
        _startScreenCanvas = GameObject.FindWithTag("StartScreen");
        _newPlayerCanvas = GameObject.FindWithTag("NewPlayer");
        _newPlayerCanvas.SetActive(false);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(ShowStartScreen());
    }

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
                EventManager.LoadGame();
                // Walk through
                break;
            case GameState.NewScene:
                Debug.Log("New Scene...");
                EventManager.LoadGame();
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
            EventManager.GameStateChange(GameState.GameStart);
        else if (newScene.buildIndex > 1)
            EventManager.GameStateChange(GameState.NewScene);

        Debug.Log(name + $": Current Scene = Scene {_scene} New Scene = Scene {newScene.buildIndex}");
    }

    public void GameSetUp()
    {
        EventManager.GameStateChange(GameState.Setup);
    }

    private IEnumerator ShowStartScreen()
    {
        _startScreenCanvas.SetActive(false);

        yield return new WaitForSeconds(0.55f); // use variable

        _startScreenCanvas.SetActive(true);
    }

    private IEnumerator SceneTransitionTimer()
    {
        //SaveGame(Player);
        // Start Aniimation

        yield return new WaitForSeconds(transitionTime);

        if (_scene <= SceneManager.sceneCount)
            SceneManager.LoadScene(_scene + 1);
        if (_scene >= SceneManager.sceneCount)
            EventManager.GameStateChange(GameState.GameOver);
    }

    public void StartSceneTransition()
    {
        GameStateChanged(GameState.Transition);
    }

    public void GotoPlayerCreation()
    {
        _startScreenCanvas.SetActive(false);
        _newPlayerCanvas.SetActive(true);
    }

    /*private void CreatePlayer()
    {
        try
        {
            Player.SetCharacterDetails();
        }
        catch (NullReferenceException nullRE)
        {
            Debug.Log(name + $": Player does not exist. " +
                $"\nError Type = {nullRE.GetType()}" +
                $"\nException = {nullRE.Message}");
        }
    }*/

    private void GetPlayerName(string playerName) => Player.Name = playerName;

    public void CloseUI(GameObject go)
    {
        go.SetActive(false);
    }

    /*public void SaveGame(PlayerStats_SO playerData)
    {
        GameStateChanged(GameState.SaveGame);
        saveName = Player.Player.Name + "PlayerData";
        ES3.Save(saveName, playerData);
        Debug.Log(name + ": Game Saved Successfully");
        Debug.Log(name + $": Saved Player Name = {Player.Player.Name}, Saved Character = {Player.Player.Character.Name}");
    }*/

    /*public void LoadGame()
    {
        Player = (PlayerStats_SO)ES3.Load(saveName);
        Player.SetCharacterDetails();
        Player.SetPlayerLevel();
        Debug.Log(name + ": Game Loaded Successfully");
        Debug.Log(name + $": Loaded Player Name = {Player.Player.Name}, Loaded Character = {Player.Player.Character.Name}");
    }*/
}

public enum GameState
{
    Setup,
    GameStart,
    Paused,
    SaveGame,
    Transition,
    NewScene,
    GameOver,
}

