using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PlayerSystem : MonoBehaviour, IPlayerSystem
{
    public PlayerStats_SO Player;
    [SerializeField] protected float deathDelay = 1;

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
        get { return Player.CurrentStats.CurrentHP; }
        set { Player.CurrentStats.CurrentHP = value; }
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

    #region Get

    public virtual void RecieveBuffs(float amount, BuffType buff)
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
                Player.TotalSizeMultipler /= 2;
                break;
            case BuffType.Grow:
                Player.TotalSizeMultipler *= 2;
                break;
        }
    }

    public float GetStat(BuffType stat)
    {
        float amount = -1;

        switch (stat)
        {
            case BuffType.Strength:
                amount = Player.CurrentStats.CurrentStr;
                break;
            case BuffType.Health:
                amount = Player.CurrentStats.CurrentHP / Player.BaseStats.MaxHP * 100;
                break;
            case BuffType.Jump:
                amount = Player.CurrentStats.CurrentJumpHeight;
                break;
            case BuffType.MoveSpeed:
                amount = Player.CurrentStats.CurrentMS;
                break;
            case BuffType.AttackSpeed:
                amount = Player.CurrentStats.CurrentAS;
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

    public virtual float CalculateAttackSpeed(float baseAttackTime, float agility, float bonusAS) => baseAttackTime * (agility + bonusAS);

    public virtual float CalculateAgility(int baseHP, float baseMS) => baseHP / baseMS;

    protected HPPercent ChangeHPIcon(int currentHP, int maxHP) =>
        currentHP switch
        {
            int i when i == 0 => HPPercent.Dead,
            int i when i <= maxHP * 0.25 => HPPercent.Health25,
            int i when i <= maxHP * 0.5 => HPPercent.HalfHealth,
            int i when i <= maxHP * 0.75 => HPPercent.Health75,
            int i when i == maxHP => HPPercent.FullHealth,
            _ => throw new System.NotImplementedException(),
        };

    protected float CalculateHPPercent(int currentHP, int maxHP) =>
        currentHP switch
        {
            int i when i == 0 => 0.00f,
            int i when i <= maxHP * 0.25 => 0.25f,
            int i when i <= maxHP * 0.5 => 0.50f,
            int i when i <= maxHP * 0.75 => 0.75f,
            int i when i == maxHP => 1.00f,
            _ => throw new System.NotImplementedException(),
        };

    public virtual void TakeDamage(ObjectType type, int damage)
    {
        if (type == ObjectType.Player)
        {
            Player.CurrentStats.CurrentHP -= damage;
            EventManager.Instance.UnitHealth(type, ChangeHPIcon(CurrentHP, Player.BaseStats.MaxHP));
            if (CurrentHP < 0) CurrentHP = 0;
        }
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

    public virtual void SetPlayerLevel() => Player.Level = SceneManager.GetActiveScene().buildIndex;

    public virtual void SetPlayerScore(int score) => Player.Score = score; // create scoring system

    protected virtual void SetCharacterDetails()
    {
        Player.ID = CharacterManager.Instance.Character.ID;
        Player.CharacterName = CharacterManager.Instance.Character.Name;
        Player.Sprite = CharacterManager.Instance.Character.Sprite;
        Player.IsOverrideController = CharacterManager.Instance.Character.IsOverrideController;

        if (CharacterManager.Instance.Character.AnimatorController != null || CharacterManager.Instance.Character.AnimOverrideController != null)
        {
            switch (Player.IsOverrideController)
            {
                case true:
                    Player.AnimatorController = CharacterManager.Instance.Character.AnimatorController;
                    break;
                case false:
                    Player.AnimOverrideController = CharacterManager.Instance.Character.AnimOverrideController;
                    break;
            }
        }
        else if (CharacterManager.Instance.Character.AnimatorController == null && CharacterManager.Instance.Character.AnimOverrideController == null)
            Debug.Log($": Animator Controller and Animator Override Controller do not exist. \nCharacter index is {Player.ID}.");

        UpdatePlayerDetails();
    }

    public virtual void UpdatePlayerDetails()
    {
        Player.BaseStats.MaxHP = Player.TotalBonusHealth + Player.Player.BaseHealth + CharacterManager.Instance.Character.CurrentStats.CurrentHP;
        Player.CurrentStats.CurrentHP = Player.BaseStats.MaxHP;

        Player.CurrentStats.CurrentStr = Player.TotalBonusStrength + Player.Player.BaseStrength + CharacterManager.Instance.Character.CurrentStats.CurrentStr;
        Player.CurrentStats.CurrentJumpHeight = Player.Player.BaseJumpHeight * (Player.TotalJumpMultiplier + CharacterManager.Instance.Character.CurrentStats.CurrentJumpHeight);
        Player.Agility = CalculateAgility(
                Player.Player.BaseHealth, Player.Player.BaseMS
                );
        Player.CurrentStats.CurrentAS = CalculateAttackSpeed(
            Player.Player.BaseAttackTime, Player.Agility, Player.TotalASMultiplier + CharacterManager.Instance.Character.CurrentStats.CurrentAS
            );
        Player.CurrentStats.CurrentMS = Player.Player.BaseMS * (Player.TotalMSMultiplier + CharacterManager.Instance.Character.CurrentStats.CurrentMS);
        Player.CurrentSize = Player.Player.BaseSize * Player.TotalSizeMultipler;
        Debug.Log($": Size = {Player.CurrentSize}");
    }

    public virtual void UpdatePlayer()
    {
        SetPlayerLevel();
        UpdatePlayerDetails();
        EventManager.Instance.UnitHealth(ObjectType.Player, ChangeHPIcon(CurrentHP, Player.BaseStats.MaxHP));

        //Debug.Log($": Player Name = {PlayerName}, Character Name = {Player.Player.Character.Name}");
    }

    #endregion

    public virtual void DestroyInteractable(float time)
    {
        Destroy(this, time);
    }

    public virtual void DestroyInteractable()
    {
        gameObject.SetActive(false);
    }
}
