using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Item/Consumable")]
public class ConsumableStats_SO : ItemStats_SO
{
    public BuffType ConsumableType;
    public DOT StatusEffect;
}

