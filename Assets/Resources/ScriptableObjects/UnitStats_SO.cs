using UnityEditor.Animations;
using UnityEngine;

public class UnitStats_SO : ScriptableObject
{
    [Header("Identifiers")]
    public int ID;
    public string Name;
    public Sprite Sprite;

    [Header("Animation")]
    public bool IsOverrideController;
    public AnimatorController AnimatorController;
    public AnimatorOverrideController AnimOverrideController;

    [Header("Current Stats")]
    public int MaxHP;
    public int CurrentHP;
    public int CurrentStr;
    public float CurrentJumpHeight;
    public float CurrentMS;
    public float CurrentAS;
    public float CurrentDex;
}
