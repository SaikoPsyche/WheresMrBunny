using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenuAttribute(menuName = "Player/Stats/Active")]
public class PlayerStats_SO : ScriptableObject
{
    // Player
    public PlayerDetails Player;

    public void SetCurrentHealth(int currentHealth)
    {
        Player.CurrentHealth = currentHealth;
    }

    public void SetPlayerName(string playerName) => Player.Name = playerName;

    public void SetGemCount(int count, BuffType gemType)
    {
        switch (gemType)
        {
            case BuffType.Strength:
                Player.GemCount.StrenthGemCount = count;
                break;
            case BuffType.Health:
                Player.GemCount.HealthGemCount = count;
                break;
            case BuffType.MoveSpeed:
                Player.GemCount.MSGemCount = count;
                break;
            case BuffType.Jump:
                Player.GemCount.JumpGemCount = count;
                break;
            case BuffType.AttackSpeed:
                Player.GemCount.ASGemCount = count;
                break;
            case BuffType.Shrink:
                Player.GemCount.ShrinkGemCount = count;
                break;
        }
    }

    public void SetPlayerLevel() => Player.Level = SceneManager.GetActiveScene().buildIndex;

    public void SetPlayerScore(int score) => Player.Score = score;

    public void SetCharacterDetails()
    {
        Player.Character.Index = CharacterManager.Character.Index;
        Player.Character.Name = CharacterManager.Character.Name;
        Player.Character.Sprite = CharacterManager.Character.Sprite;

        if (CharacterManager.Character.AnimatorController != null || CharacterManager.Character.AnimOverrideController != null)
        {
            switch (CharacterManager.Character.IsOverrideController)
            {
                case true:
                    Player.Character.AnimatorController = CharacterManager.Character.AnimatorController;
                    break;
                case false:
                    Player.Character.AnimOverrideController = CharacterManager.Character.AnimOverrideController;
                    break;
            }
        }
        else if (CharacterManager.Character.AnimatorController == null || CharacterManager.Character.AnimOverrideController == null)
            Debug.Log(name + $": Animator Controller and Animator Override Controller do not exist. \nCharacter index is {CharacterManager.Character.Index}.");

        Player.Character.AnimOverrideController = CharacterManager.Character.AnimOverrideController;
        Player.Character.BonusHealth = CharacterManager.Character.Health;
        Player.Character.MSMultiplier = CharacterManager.Character.MSMultiplier;
        Player.Character.JumpHeightMultiplier = CharacterManager.Character.JumpHeightMultiplier;
        Player.Character.BonusStrength = CharacterManager.Character.Strength;
        Player.Character.ASMultiplier = CharacterManager.Character.ASMultiplier;
        Player.Character.SizeMultipler = CharacterManager.Character.SizeMultiplier;
        Debug.Log(name + $": Size Multiplier = {Player.Character.SizeMultipler}");

        SetBaseStats();
        UpdatePlayerDetails();
    }

    private void SetBaseStats()
    {
        Player.TotalBonusHealth = Player.BaseStats.Player.BaseHealth;
        Player.TotalBonusStrength = Player.BaseStats.Player.BaseStrength;
        Player.TotalJumpMultiplier = Player.BaseStats.Player.BaseJumpHeight;
        Player.Agility = CalculateAgility();
        Player.TotalASMultiplier = CalculateAttackSpeed(Player.BaseStats.Player.BaseAttackTime);
        Player.TotalMSMultiplier = Player.BaseStats.Player.BaseMoveSpeed;
        Player.TotalSizeMultipler = Player.BaseStats.Player.BaseSize;
    }

    public void UpdatePlayerDetails()
    {
        Player.TotalBonusHealth += Player.Character.BonusHealth;
        Player.MaxHealth = Player.TotalBonusHealth;
        Player.TotalBonusStrength += Player.Character.BonusStrength;
        Player.TotalJumpMultiplier *= Player.Character.JumpHeightMultiplier;
        Player.TotalASMultiplier *= CalculateAttackSpeed(Player.Character.ASMultiplier);
        Player.TotalMSMultiplier *= Player.Character.MSMultiplier;
        Player.TotalSizeMultipler *= Player.Character.SizeMultipler;
        Debug.Log(name + $": Size = {Player.TotalSizeMultipler}");
    }

    private float CalculateAgility()
    {
        float agility = Player.BaseStats.Player.BaseHealth / Player.BaseStats.Player.BaseMoveSpeed;

        return agility;
    }

    private float CalculateAttackSpeed(float bonus)
    {
        float attackSpeed = Player.TotalASMultiplier;
        float bat = Player.BaseStats.Player.BaseAttackTime * Player.Agility;

        attackSpeed *= bat * bonus;
        return attackSpeed;
    }
}

[Serializable]
public struct PlayerDetails
{
    public string Name;
    public int Score;
    public int Level;
    public int MaxHealth;
    public int CurrentHealth;
    public bool IsFullSize;
    public PlayerBaseStats_SO BaseStats;
    public CharacterDetails Character;
    public float Agility;
    public int TotalBonusHealth;
    public int TotalBonusStrength;
    public float TotalMSMultiplier;
    public float TotalJumpMultiplier;
    public float TotalASMultiplier;
    public float TotalSizeMultipler;
    public GemCount GemCount;
   
}

[Serializable]
public struct GemCount
{
    public int StrenthGemCount;
    public int HealthGemCount;
    public int MSGemCount;
    public int JumpGemCount;
    public int ASGemCount;
    public int ShrinkGemCount;
    public int GrowGemCount;
}

[Serializable]
public struct CharacterDetails
{
    public int Index;
    public string Name;
    public Sprite Sprite;
    public bool IsOverrideController;
    public AnimatorOverrideController AnimOverrideController;
    public AnimatorController AnimatorController;
    public int BonusHealth;
    public int BonusStrength;
    public float MSMultiplier;
    public float JumpHeightMultiplier;
    public float ASMultiplier;
    public float SizeMultipler;
}
