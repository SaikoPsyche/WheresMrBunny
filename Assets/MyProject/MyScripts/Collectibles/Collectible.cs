using UnityEngine;

public class Collectible : UnitMovement
{
    protected int bounceCount = 0;

    protected virtual void Collect(GameObject go, ConsumableStats_SO consumable)
    {
        // Add to inventory
        // Add fx
        if (consumable == null) consumable.PowerUpType = BuffType.None;

        GiveBuff(consumable);
        DestroyInteractable(go, 0, null);
    }

    protected virtual void GiveBuff(ConsumableStats_SO consumable)
    {
        float amount = 0;

        BuffType buffType = consumable.PowerUpType;

        switch (buffType)
        {
            case BuffType.None: break;
            case BuffType.AttackSpeed:
                amount = consumable.ASMultiplier;
                break;
            case BuffType.Health:
                amount = consumable.BonusHealth;
                break;
            case BuffType.Jump:
                amount = consumable.JumpHeightMultiplier;
                break;
            case BuffType.MoveSpeed:
                amount = consumable.MSMultiplier;
                break;
            case BuffType.Strength:
                amount = consumable.BonusStrength;
                break;
            case BuffType.Shrink:
                amount = consumable.SizeMultiplier;
                break;
        }

        EventManager.GiveBuff(amount, buffType);
    }
}
