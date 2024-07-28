using UnityEngine;

public class Consumable : ItemSystem
{
    [SerializeField] protected int consumableCount;
    [SerializeField] protected ConsumableStats_SO[] consumables;
    [SerializeField] protected ConsumableStats_SO consumable;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();

        _ = TryGetComponent(out _animator);
    }

    private void Start()
    {
        if (consumables == null)
            ChooseConsumableType();
        else SetConsumableType(consumable);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        { 
            CountConsumables();
            Collect();
        }
    }

    private void CountConsumables() => EventManager.Instance.CollectConsumable(1, consumable.ConsumableType);

    private void SetConsumableType(ConsumableStats_SO chosenConsumable)
    {
        consumable = chosenConsumable;
        _sr.sprite = consumable.Sprite;
    }

    private void ChooseConsumableType()
    {
        int index = Random.Range(0, consumables.Length + 1);

        for (int i = 0; i < consumables.Length; i++)
        {
            Debug.Log(name + $": Index = {index}, iteration number = {i}");
            if (consumables[i].Index == index)
            {
                try
                {
                    SetConsumableType(consumables[i]);
                }
                catch(System.Exception ex)
                {
                    Debug.LogError(name + $": Gem {consumables[i]} does not exist." +
                        $"\nException: {ex.Message}\n Error from: {ex.Source}");
                }
            }
            else if (i > index) continue;
        }
    }

    public override void Collect()
    {
        base.Collect();
    }

    public virtual void GiveBuff()
    {
        float amount = consumable.Amount;
        BuffType type = consumable.ConsumableType;

        EventManager.Instance.GiveBuff(amount, type);
    }
}
