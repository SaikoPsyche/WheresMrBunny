using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUp : Collectible
{
    [SerializeField] private int powerUpCount;
    [SerializeField] private ConsumableStats_SO[] powerUp;
    [SerializeField] private ConsumableStats_SO chosenPowerUp;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (chosenPowerUp == null)
            ChoosePowerUpType();
        else SetPowerUpType(chosenPowerUp);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { 
            CountPowerUps();
            Collect(gameObject, chosenPowerUp);
        }
    }

    private void CountPowerUps() => EventManager.CollectGems(1, chosenPowerUp.PowerUpType);

    private void SetPowerUpType(ConsumableStats_SO chosenPowerUp)
    {
        this.chosenPowerUp = chosenPowerUp;
        _sr.sprite = this.chosenPowerUp.Sprite;
    }

    private void ChoosePowerUpType()
    {
        int index = Random.Range(0, powerUp.Length + 1);

        for (int i = 0; i < powerUp.Length; i++)
        {
            Debug.Log(name + $": Index = {index}, iteration number = {i}");
            if (powerUp[i].Index == index)
            {
                if (powerUp[i] != null)
                {
                    chosenPowerUp = powerUp[i];
                    _sr.sprite = chosenPowerUp.Sprite;
                }
                else Debug.Log(name + $": Gem {powerUp[i]} does not exist.");
            }
            else if (i > index) continue;
        }
    }
}
