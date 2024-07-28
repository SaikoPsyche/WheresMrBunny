using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSystem : IUnitSystem
{
    float GetStat(BuffType stat);
    void RecieveBuffs(float amount, BuffType buff);
    void CountGems(int count, BuffType gemType);
}
