using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Interactable/Consumable")]
public class ConsumableStats_SO : ScriptableObject
{
    public int Index;
    public BuffType PowerUpType;
    public Sprite Sprite;
    public int BonusHealth;
    public int BonusStrength;
    public float JumpHeightMultiplier;
    public float MSMultiplier;
    public float ASMultiplier;
    public float SizeMultiplier;
    public Bounce Bounce;
}

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
    Grow
}

[Serializable]
public struct Bounce
{
    public float BounceHeight;
    public float BounceDuration;
    public int MinBounces;
    public int MaxBounces;
}
