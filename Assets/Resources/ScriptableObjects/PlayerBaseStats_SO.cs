using System;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Player/Stats/Base")]
public class PlayerBaseStats_SO : ScriptableObject
{
    public BaseStats Player;
}

[Serializable]
public struct BaseStats
{
    public int BaseHealth;
    public float BaseMoveSpeed;
    public float BaseJumpHeight;
    public int BaseStrength;
    public float BaseAttackTime; // Base attacks per second
    public float BaseSize;
}
