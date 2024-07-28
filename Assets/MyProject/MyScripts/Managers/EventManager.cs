using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action<int> OnChooseCharacter;
    public event Action<string> OnChooseName;
    public event Action<int, BuffType> OnCollectConsumable;
    public event Action<float, BuffType> OnGiveBuff;
    public event Action<int> OnChangeScore;
    public event Action<PlayerStats_SO> OnSaveGame;
    public event Action OnLoadGame;
    public event Action<ObjectType, int> OnTakeDamage;
    public event Action OnUnitDeath;
    public event Action<ObjectType, HPPercent> OnUnitHealth;
    public event Action<GameState> OnGameStateChange;
    public event Action<TransitionType,Vector3> OnTeleport;

    public void ChooseCharacter(int index) => OnChooseCharacter?.Invoke(index);
    public void ChooseName(string name) => OnChooseName?.Invoke(name);
    public void CollectConsumable(int count, BuffType gemType) => OnCollectConsumable?.Invoke(count, gemType);
    public void GiveBuff(float amount, BuffType gemType) => OnGiveBuff?.Invoke(amount, gemType);
    public void ChangeScore(int score) => OnChangeScore?.Invoke(score);
    public void SaveGame(PlayerStats_SO playerData) => OnSaveGame?.Invoke(playerData);
    public void LoadGame() => OnLoadGame?.Invoke();
    public void UnitHealth(ObjectType type, HPPercent playerHealth) => OnUnitHealth?.Invoke(type, playerHealth);
    public void TakeDamage(ObjectType type, int damage) => OnTakeDamage?.Invoke(type, damage);
    public void UnitDeath() => OnUnitDeath?.Invoke();
    public void GameStateChange(GameState gameState) => OnGameStateChange?.Invoke(gameState);
    public void Teleport(TransitionType type, Vector3 nextPos) => OnTeleport?.Invoke(type, nextPos);
}
