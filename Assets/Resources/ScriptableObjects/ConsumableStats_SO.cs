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
    public float JumpHeightMultiplier = 1;
    public float MSMultiplier = 1;
    public float ASMultiplier = 1;
    public float SizeMultiplier = 1;
    public int Points;
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
