using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitSystem : IInteractable
{
    float CalculateAttackSpeed(float baseAttackTime, float agility, float bonusAS);
    float CalculateAgility(int hp, float ms);
    void TakeDamage(ObjectType type, int damage);
}
