using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<int> OnChooseCharacter;
    public static event Action<string> OnChooseName;
    public static event Action<int, BuffType> OnCollectGems;
    public static event Action<float, BuffType> OnGiveBuff;
    public static event Action<int> OnChangeScore;
    public static event Action<PlayerStats_SO> OnSaveGame;
    public static event Action OnLoadGame;
    public static event Action<int> OnTakeDamage;
    public static event Action OnPlayerDeath;
    public static event Action<HPPercent> OnPlayerHealth;
    public static event Action<GameState> OnGameStateChange;
    public static event Action<TransitionType,Vector3> OnTeleport;



    public static void ChooseCharacter(int index) => OnChooseCharacter?.Invoke(index);
    public static void ChooseName(string name) => OnChooseName?.Invoke(name);
    public static void CollectGems(int count, BuffType gemType) => OnCollectGems?.Invoke(count, gemType);
    public static void GiveBuff(float amount, BuffType gemType) => OnGiveBuff?.Invoke(amount, gemType);
    public static void ChangeScore(int score) => OnChangeScore?.Invoke(score);
    public static void SaveGame(PlayerStats_SO playerData) => OnSaveGame?.Invoke(playerData);
    public static void PlayerHealth(HPPercent playerHealth) => OnPlayerHealth?.Invoke(playerHealth);
    public static void LoadGame() => OnLoadGame?.Invoke();
    public static void GameStateChange(GameState gameState) => OnGameStateChange?.Invoke(gameState);
    public static void Teleport(TransitionType type, Vector3 nextPos) => OnTeleport?.Invoke(type, nextPos);
}
