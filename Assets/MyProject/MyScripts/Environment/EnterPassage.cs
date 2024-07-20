using System;
using UnityEngine;

public class EnterPassage : MonoBehaviour
{
    [SerializeField] private TransitionType transitionType;
    [SerializeField] private bool isSameSceneTransition = true;
    [SerializeField] private Transform nextPassage;
    private Animator _animator;

    private void Awake() => _animator = GetComponentInParent<Animator>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (transitionType != TransitionType.None)
                _animator.SetTrigger("Open");

            switch (transitionType)
            {
                case TransitionType.None: break;
                case TransitionType.InScene:
                    EventManager.Teleport(transitionType, nextPassage.position);
                    break;
                case TransitionType.ExitScene:
                    EventManager.GameStateChange(GameState.Transition); 
                    break;
            }
        }
    }
}

[Serializable]
public enum TransitionType
{
    None,
    InScene,
    ExitScene,
}
