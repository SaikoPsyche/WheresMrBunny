using UnityEngine;

public class Collectible : ItemSystem
{
    public override void Collect()
    {
        // Add to inventory
        // Add fx

        DestroyInteractable(destroyDelay);
    }
}
