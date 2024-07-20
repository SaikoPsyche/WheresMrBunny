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

    private void OnCollisionEnter2D(Collision2D col)
    {
        int noDirection = 0;

        if (col.gameObject.CompareTag("Ground"))
        {
            //Debug.Log(name + $": Collision with {col}!");
            if (chosenPowerUp == null) chosenPowerUp = powerUp[1]; // HealthGem

            BounceCounter(bounceCount, chosenPowerUp.Bounce.MaxBounces);

            if (canMove == true)
                Bounce(chosenPowerUp.Bounce.BounceHeight, chosenPowerUp.Bounce.BounceDuration, ChooseDirection());
            else Bounce(chosenPowerUp.Bounce.BounceHeight, chosenPowerUp.Bounce.BounceDuration, noDirection);
        }
    }

    private void CountPowerUps()
    {
        UpdatePowerUpCount(chosenPowerUp.PowerUpType);
        powerUpCount++;
        EventManager.CollectGems(powerUpCount, chosenPowerUp.PowerUpType);
    }

    private void SetPowerUpType(ConsumableStats_SO chosenPowerUp)
    {
        this.chosenPowerUp = chosenPowerUp;
        _sr.sprite = this.chosenPowerUp.Sprite;
        UpdatePowerUpCount(this.chosenPowerUp.PowerUpType);
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
                    UpdatePowerUpCount(chosenPowerUp.PowerUpType);
                }
                else Debug.Log(name + $": Gem {powerUp[i]} does not exist.");
            }
            else if (i > index) continue;
        }
    }

    private void UpdatePowerUpCount(BuffType powerUpType)
    {
        switch (powerUpType)
        {
            case BuffType.None: break;
            case BuffType.AttackSpeed:
                powerUpCount = PlayerSystem.PlayerManager.ASGemCount;
                break;
            case BuffType.Health:
                powerUpCount = PlayerSystem.PlayerManager.HealthGemCount;
                break;
            case BuffType.Jump:
                powerUpCount = PlayerSystem.PlayerManager.JumpGemCount;
                break;
            case BuffType.MoveSpeed:
                powerUpCount = PlayerSystem.PlayerManager.MSGemCount;
                break;
            case BuffType.Strength:
                powerUpCount = PlayerSystem.PlayerManager.StrengthGemCount;
                break;
            case BuffType.Shrink:
                powerUpCount = PlayerSystem.PlayerManager.GrowGemCount;
                break;
        }
    }
}
