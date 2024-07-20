using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Image dissolveMat;
    [SerializeField] private float lerpTime = 10f;
    private static Animator _animator;

    private void OnEnable()
    {
        EventManager.OnTeleport += TriggerTransition;
    }

    private void OnDisable()
    {
        EventManager.OnTeleport -= TriggerTransition;

    }

    private void Awake() => _animator = GetComponent<Animator>();

    /*private void Start()
    {
        float fadeAmount = 0;

        for (float i = 0; i < 1.2; i += 0.1f)
            fadeAmount += i * 10 * Time.deltaTime;
            dissolveMat.material.SetFloat("_FadeAmount", fadeAmount);
        Debug.Log(dissolveMat);
    }*/

    public static void TriggerTransition(TransitionType type, Vector3 pos)
    {
        if (type != TransitionType.None) _animator.SetTrigger("Start");
    }
}
