using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectible
{
    private void Update()
    {
        //CollectibleMovement();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) Collect(gameObject, null);
    }
}
