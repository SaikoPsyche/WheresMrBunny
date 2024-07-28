using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [Tooltip("Possible items to be droppped by loot box.")]
    [SerializeField] protected Loot[] possibleDrops;

    [Tooltip("Total number of items to be dropped by loot box.")]
    [SerializeField] protected int amountToDrop = 3;

    [Tooltip("Possible number of repeats among drops.")]
    [SerializeField] protected int maxAmountPerDrop = 2;

    protected LootRarity GetRarity()
    {
        int chance = Random.Range(1, 101);
        LootRarity rarity = LootRarity.None;

        switch (chance)
        {
            case 1: rarity = LootRarity.Diamond; break;
            case int i when i <= 5: rarity = LootRarity.Emerald; break;
            case int i when i <= 10: rarity = LootRarity.Ruby; break;
            case int i when i <= 25: rarity = LootRarity.Rare; break;
            case int i when i <= 50: rarity = LootRarity.Uncommon; break;
            case int i when i <= 100: rarity = LootRarity.Common; break;
        }
        return rarity;
    }

    protected Loot? ChooseLoot(Loot[] possibleDrops, LootRarity rarity)
    {
        foreach (Loot loot in possibleDrops)
        {
            if (loot.Rarity == rarity) return loot;
        }
        return null;
    }

    public Loot[] GiveLoot() // call through collision data
    {
        Loot[] chosenLoot = new Loot[amountToDrop];
        for (int i = 0; i < amountToDrop; i++)
        {
            int amount = Random.Range(0, maxAmountPerDrop + 1);

            if (amount > 0)
            {
                if (chosenLoot.Length < amountToDrop - amount && chosenLoot.Length < amountToDrop)
                    for (int j = 0; j < amount; j++) 
                        chosenLoot[i] = (Loot)ChooseLoot(possibleDrops, GetRarity());
            }
        }
        return chosenLoot;
    }
}

[System.Serializable]
public struct Loot
{
    public Collectible Reward;
    public LootRarity Rarity;
    [HideInInspector] public int MaxDropChance;
    [HideInInspector] public int MinDropChance;
}
