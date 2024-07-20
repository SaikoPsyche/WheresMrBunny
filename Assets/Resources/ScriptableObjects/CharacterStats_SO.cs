using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Enemy")]
public class CharacterStats_SO : ScriptableObject
{
    public int Index;
    public string Name;
    public Sprite Sprite;
    public bool IsOverrideController;
    public AnimatorController AnimatorController;
    public AnimatorOverrideController AnimOverrideController;
    public int BonusHealth;
    public int BonusStrength;
    public float JumpHeightMultiplier;
    public float MSMultiplier;
    public float ASMultiplier;
    public float SizeMultiplier;
}
