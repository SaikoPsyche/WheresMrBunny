using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsAndStructs { }

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

public enum ObjectType
{
    None,
    Player,
    Enemy,
    Consumable,
    Collectible
}

// Items //

// Consumables
[Serializable]
public enum BuffType
{
    None,
    AttackSpeed,
    Health,
    Jump,
    MoveSpeed,
    Strength,
    Shrink,
    Grow,
}

public enum DOT
{
    None,
    Poison,
    Burn,
    Freeze,
    Horrify, // Fear DOT
    Zap, // Electric DOT
    Drown,
}

// Loot
[System.Serializable]
public enum LootRarity // think of better names
{
    None,
    Common,
    Uncommon,
    Rare,
    Ruby,
    Emerald,
    Diamond
}


// Units //
[System.Serializable]
public struct BaseStats
{
    public int MaxHP;
    public int BaseHP;
    public int BaseStr;
    public float BaseJumpHeight;
    public float BaseMS;
    public float BaseAS;
    public float BaseDex;
}

[System.Serializable]
public struct CurrentStats
{
    public int CurrentHP;
    public int CurrentStr;
    public float CurrentJumpHeight;
    public float CurrentMS;
    public float CurrentAS;
    public float CurrentDex;
}

//Enemy
[Serializable]
public enum DifficultyLevel
{
    None,
    Easy,
    Medium,
    Hard,
    MiniBoss,
    Boss,
}

// Player...
[Serializable]
public struct PlayerDetails
{
    public int BaseHealth;
    public int BaseStrength;
    public float BaseMS;
    public float BaseJumpHeight;
    public float BaseAttackTime;
    public float BaseSize;
}

// ...Stats 
[Serializable]
public enum HPPercent
{
    FullHealth,
    Health75,
    HalfHealth,
    Health25,
    Dead,
}

[Serializable]
public struct HPImageState
{
    public Sprite FullHealthSprite;
    public Sprite Health75Sprite;
    public Sprite HalfHealthSprite;
    public Sprite Health25Sprite;
    public Sprite DeadSprite;
}

// ...Gameplay
[Serializable]
public struct PowerUpCount
{
    public int StrenthGemCount;
    public int HealthGemCount;
    public int MSGemCount;
    public int JumpGemCount;
    public int ASGemCount;
    public int ShrinkGemCount;
    public int GrowGemCount;
}


// Environment //

// Passages
[Serializable]
public enum TransitionType
{
    None,
    InScene,
    ExitScene,
}


// UI //

// Warning
public enum ButtonType
{
    oK,
    cancel
}