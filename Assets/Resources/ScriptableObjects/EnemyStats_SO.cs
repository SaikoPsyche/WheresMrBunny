using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Units/Enemy")]
public class EnemyStats_SO : UnitStats_SO
{
    public DifficultyLevel DifficultyLevel;
    public Loot[] PossibleDrops;
}

