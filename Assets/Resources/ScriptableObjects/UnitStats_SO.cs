using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "Units/Base")]
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

    [Header("Base Stats")]
    public BaseStats BaseStats;
    
    [Header("Current Stats")]
    public CurrentStats CurrentStats;
}
