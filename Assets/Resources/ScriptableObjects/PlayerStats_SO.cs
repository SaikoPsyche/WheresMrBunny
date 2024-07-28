using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenuAttribute(menuName = "Units/Player")]
public class PlayerStats_SO : UnitStats_SO
{
    [Header("Base Stats")]
    public PlayerDetails Player;

    [Header("Chosen Character")]
    public string CharacterName;

    [Header("Additional Details")]
    public int Score;
    public int Level;
    public float Agility;

    [Header("Bonus Stats")]
    public int TotalBonusHealth;
    public int TotalBonusStrength;
    public float TotalMSMultiplier;
    public float TotalJumpMultiplier;
    public float TotalASMultiplier;
    public float TotalSizeMultipler;

    [Header("Size")]
    public bool IsFullSize = true;
    public float CurrentSize = 1;

    [Header("Power Up Count")]
    public PowerUpCount PowerUpCount;
}

