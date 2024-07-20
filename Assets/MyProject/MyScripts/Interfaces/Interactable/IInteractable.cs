using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void DestroyInteractable(GameObject goToDestroy, float timeToDestroy, string animToPlay);
}
