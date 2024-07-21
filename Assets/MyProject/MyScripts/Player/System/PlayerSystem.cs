using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSystem : IPlayerSystem
{
    public PlayerStats_SO Player;

    #region Properties

    public string PlayerName
    {
        get { return Player.Name; }

        set { Player.Name = value; }
    }

    public string CharacterName
    {
        get { return Player.CharacterName; }
    }

    public int CurrentHP
    {
        get { return Player.CurrentHP; }
        set { Player.CurrentHP = value; }
    }

    public int PlayerScore
    {
        get { return Player.Score; }
        set { SetPlayerScore(value); }
    }

    public int StrengthGemCount
    {
        get { return Player.PowerUpCount.StrenthGemCount; }
        set { Player.PowerUpCount.StrenthGemCount = value; }
    }

    public int HealthGemCount
    {
        get { return Player.PowerUpCount.HealthGemCount; }
        set { Player.PowerUpCount.HealthGemCount = value; }
    }

    public int JumpGemCount
    {
        get { return Player.PowerUpCount.JumpGemCount; }
        set { Player.PowerUpCount.JumpGemCount = value; }
    }

    public int ASGemCount
    {
        get { return Player.PowerUpCount.ASGemCount; }
        set { Player.PowerUpCount.ASGemCount = value; }
    }

    public int MSGemCount
    {
        get { return Player.PowerUpCount.MSGemCount; }
        set { Player.PowerUpCount.MSGemCount = value; }
    }

    public int ShrinkGemCount
    {
        get { return Player.PowerUpCount.ShrinkGemCount; }
        set { Player.PowerUpCount.ShrinkGemCount = value; }
    }

    public int GrowGemCount
    {
        get { return Player.PowerUpCount.GrowGemCount; }
        set { Player.PowerUpCount.GrowGemCount = value; }
    }

    #endregion

    public void UnitDeath()
    {
        throw new System.NotImplementedException();
    }

    #region Get

    public void RecieveBuffs(float amount, BuffType buff)
    {
        switch (buff)
        {
            case BuffType.Strength:
                Player.TotalBonusStrength += (int)amount;
                break;
            case BuffType.Health:
                Player.TotalBonusHealth += (int)amount;
                break;
            case BuffType.Jump:
                Player.TotalJumpMultiplier += amount;
                break;
            case BuffType.MoveSpeed:
                Player.TotalMSMultiplier += amount;
                break;
            case BuffType.AttackSpeed:
                Player.TotalASMultiplier += amount;
                break;
            case BuffType.Shrink:
                Player.TotalSizeMultipler /= amount;
                break;
            case BuffType.Grow:
                Player.TotalSizeMultipler *= amount;
                break;
        }
    }

    public float GetStat(BuffType stat)
    {
        float amount = -1;

        switch (stat)
        {
            case BuffType.Strength:
                amount = Player.CurrentStr;
                break;
            case BuffType.Health:
                amount = Player.CurrentHP / Player.MaxHP * 100;
                break;
            case BuffType.Jump:
                amount = Player.CurrentJumpHeight;
                break;
            case BuffType.MoveSpeed:
                amount = Player.CurrentMS;
                break;
            case BuffType.AttackSpeed:
                amount = Player.CurrentAS;
                break;
            case BuffType.Shrink:
                amount = Player.CurrentSize;
                break;
            case BuffType.Grow:
                amount = Player.CurrentSize;
                break;
        }

        return amount;
    }

    #endregion

    #region Calculate

    public float CalculateAttackSpeed(float baseAttackTime, float agility, float bonusAS) => baseAttackTime * agility * bonusAS;

    public float CalculateAgility(int baseHP, float baseMS) => baseHP / baseMS;

    private HPPercent CalculateHPPercent(int currentHP, int maxHP) =>
        currentHP switch
        {
            int i when i == maxHP => HPPercent.FullHealth,
            int i when i <= maxHP * 0.75 => HPPercent.Health75,
            int i when i <= maxHP * 0.5 => HPPercent.HalfHealth,
            int i when i <= maxHP * 0.25 => HPPercent.Health25,
            int i when i == 0 => HPPercent.Dead,
            _ => throw new System.NotImplementedException(),
        };

    public void TakeDamage(int damage)
    {
        Player.CurrentHP -= damage;
        EventManager.PlayerHealth(CalculateHPPercent(CurrentHP, Player.MaxHP));
        if (CurrentHP < 0) CurrentHP = 0;
        if (CurrentHP == 0) UnitDeath();
    }

    public void CountGems(int count, BuffType gemType)
    {
        switch (gemType)
        {
            case BuffType.Strength:
                Player.PowerUpCount.StrenthGemCount += count;
                break;
            case BuffType.Health:
                Player.PowerUpCount.HealthGemCount += count;
                break;
            case BuffType.MoveSpeed:
                Player.PowerUpCount.MSGemCount += count;
                break;
            case BuffType.Jump:
                Player.PowerUpCount.JumpGemCount += count;
                break;
            case BuffType.AttackSpeed:
                Player.PowerUpCount.ASGemCount += count;
                break;
            case BuffType.Shrink:
                Player.PowerUpCount.ShrinkGemCount += count;
                break;
        }
    }

    #endregion

    #region Set & Update

    public void SetPlayerLevel() => Player.Level = SceneManager.GetActiveScene().buildIndex;

    public void SetPlayerScore(int score) => Player.Score = score; // create scoring system

    public void SetCharacterDetails()
    {
        Player.ID = CharacterManager.Character.Index;
        Player.CharacterName = CharacterManager.Character.Name;
        Player.Sprite = CharacterManager.Character.Sprite;
        Player.IsOverrideController = CharacterManager.Character.IsOverrideController;

        if (CharacterManager.Character.AnimatorController != null || CharacterManager.Character.AnimOverrideController != null)
        {
            switch (Player.IsOverrideController)
            {
                case true:
                    Player.AnimatorController = CharacterManager.Character.AnimatorController;
                    break;
                case false:
                    Player.AnimOverrideController = CharacterManager.Character.AnimOverrideController;
                    break;
            }
        }
        else if (CharacterManager.Character.AnimatorController == null && CharacterManager.Character.AnimOverrideController == null)
            Debug.Log($": Animator Controller and Animator Override Controller do not exist. \nCharacter index is {Player.ID}.");

        RecieveBuffs(CharacterManager.Character.Health, BuffType.Health);
        RecieveBuffs(CharacterManager.Character.MSMultiplier, BuffType.MoveSpeed);
        RecieveBuffs(CharacterManager.Character.JumpHeightMultiplier, BuffType.Jump);
        RecieveBuffs(CharacterManager.Character.Strength, BuffType.Strength);
        RecieveBuffs(CharacterManager.Character.ASMultiplier, BuffType.AttackSpeed);
        UpdatePlayerDetails();
    }

    public void UpdatePlayerDetails()
    {
        Player.MaxHP = Player.TotalBonusHealth;
        Player.CurrentStr += Player.TotalBonusStrength;
        Player.CurrentJumpHeight *= Player.TotalJumpMultiplier;
        Player.Agility = CalculateAgility(
                Player.Player.BaseHealth, Player.Player.BaseMS
                );
        Player.CurrentAS = CalculateAttackSpeed(
            Player.Player.BaseAttackTime, Player.Agility, Player.TotalASMultiplier
            );
        Player.CurrentMS *= Player.TotalMSMultiplier;
        Player.CurrentSize *= Player.TotalSizeMultipler;
        Debug.Log($": Size = {Player.TotalSizeMultipler}");
    }

    public void UpdatePlayer(GameState gameState)
    {
        if (gameState == GameState.SaveGame)
        {
            SetPlayerLevel();
            UpdatePlayerDetails();
            EventManager.PlayerHealth(CalculateHPPercent(CurrentHP, Player.MaxHP));

            //Debug.Log($": Player Name = {PlayerName}, Character Name = {Player.Player.Character.Name}");
        }
    }

    #endregion
}
