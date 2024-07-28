using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSystem : MonoBehaviour, IItemSystem
{
    [SerializeField] protected float destroyDelay = 1;
    protected SpriteRenderer _sr;
    protected Animator _animator;

    public virtual void Collect()
    {
        DestroyInteractable(destroyDelay);
    }

    public void DestroyInteractable()
    {
        gameObject.SetActive(false);
    }

    public void DestroyInteractable(float time)
    {
        Destroy(gameObject, time);
    }
}
