using UnityEditor.Playables;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerManager
{
    public PlayerStats_SO Player;

    public string PlayerName
    {
        get { return Player.Player.Name; }

        set { Player.SetPlayerName(value); }
    }

    public string CharacterName
    {
        get { return Player.Player.Character.Name; }
    }

    public int CurrentHealth
    {
        get { return Player.Player.CurrentHealth; }
        set { Player.SetCurrentHealth(value); }
    }

    public int PlayerScore
    {
        get { return Player.Player.Score; }
        set { Player.SetPlayerScore(value); }
    }

    public int StrengthGemCount
    {
        get { return Player.Player.GemCount.StrenthGemCount; }
        set { Player.SetGemCount(value, BuffType.Strength); }
    }

    public int HealthGemCount
    {
        get { return Player.Player.GemCount.HealthGemCount; }
        set { Player.SetGemCount(value, BuffType.Health); }
    }

    public int JumpGemCount
    {
        get { return Player.Player.GemCount.JumpGemCount; }
        set { Player.SetGemCount(value, BuffType.Jump); }
    }

    public int ASGemCount
    {
        get { return Player.Player.GemCount.ASGemCount; }
        set { Player.SetGemCount(value, BuffType.AttackSpeed); }
    }

    public int MSGemCount
    {
        get { return Player.Player.GemCount.MSGemCount; }
        set { Player.SetGemCount(value, BuffType.MoveSpeed); }
    }

    public int ShrinkGemCount
    {
        get { return Player.Player.GemCount.ShrinkGemCount; }
        set { Player.SetGemCount(value, BuffType.Shrink); }
    }

    public int GrowGemCount
    {
        get { return Player.Player.GemCount.GrowGemCount; }
        set { Player.SetGemCount(value, BuffType.Grow); }
    }

    public void RecieveBuffs(float amount, BuffType buff)
    {
        switch (buff)
        {
            case BuffType.Strength:
                Player.Player.TotalBonusStrength += (int)amount;
                break;
            case BuffType.Health:
                Player.Player.TotalBonusHealth += (int)amount;
                break;
            case BuffType.Jump:
                Player.Player.TotalJumpMultiplier *= amount;
                break;
            case BuffType.MoveSpeed:
                Player.Player.TotalMSMultiplier *= amount;
                break;
            case BuffType.AttackSpeed:
                Player.Player.TotalASMultiplier *= amount;
                break;
            case BuffType.Shrink:
                Player.Player.TotalSizeMultipler *= amount; // shrink to half size
                break;
            case BuffType.Grow:
                Player.Player.TotalSizeMultipler *= amount;
                break;
        }
    }

    public float GetStat(BuffType buff)
    {
        float amount = -1;

        switch (buff)
        {
            case BuffType.Strength:
                amount = Player.Player.TotalBonusStrength;
                break;
            case BuffType.Health:
                amount = Player.Player.MaxHealth;
                break;
            case BuffType.Jump:
                amount = Player.Player.TotalJumpMultiplier;
                break;
            case BuffType.MoveSpeed:
                amount = Player.Player.TotalMSMultiplier;
                break;
            case BuffType.AttackSpeed:
                amount = Player.Player.TotalASMultiplier;
                break;
            case BuffType.Shrink:
                amount = Player.Player.TotalSizeMultipler;
                break;
            case BuffType.Grow:
                amount = Player.Player.TotalSizeMultipler;
                break;
        }

        return amount;
    }

    public void UpdatePlayer(GameState gameState, string GOName)
    {
        if (gameState == GameState.SaveGame)
        {
            Player.SetPlayerLevel();
            Player.SetCharacterDetails();
            EventManager.PlayerHealth(CalculateHPPercent(CurrentHealth));

            Debug.Log(GOName + $": Player Name = {PlayerName}, Character Name = {Player.Player.Character.Name}");
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < 0) CurrentHealth = 0;

        EventManager.PlayerHealth(CalculateHPPercent(CurrentHealth));
    }

    private HPPercent CalculateHPPercent(int currentHP)
    {
        HPPercent hp = HPPercent.FullHealth;

        return hp;
    }
}
