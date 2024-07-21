using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    float CalculateAttackSpeed(float baseAttackTime, float agility, float bonusAS);
    float CalculateAgility(int hp, float ms);
    void TakeDamage(int damage);
    void UnitDeath();
}
