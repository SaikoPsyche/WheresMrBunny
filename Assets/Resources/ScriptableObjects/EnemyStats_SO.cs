using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable/Enemy")]
public class EnemyStats_SO : UnitStats_SO
{
    public DifficultyLevel DifficultyLevel;
}

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
