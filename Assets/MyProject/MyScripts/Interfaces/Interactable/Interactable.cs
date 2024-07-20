using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    protected Animator _animator;
    protected int _unitHealth;

    [Tooltip("Assumes parameter for anim is a bool. Automatically sets parameter to true.")]
    public void DestroyInteractable(GameObject goToDestroy, float timeToDestroy, string animToPlay)
    {
        if (animToPlay != null) _animator.SetBool(animToPlay, true);

        //goToDestroy.SetActive(false);
        Destroy(goToDestroy, timeToDestroy);
    }
}
