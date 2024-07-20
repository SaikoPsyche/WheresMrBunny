using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountGems : MonoBehaviour
{
    [SerializeField] private BuffType gemType;
    private TextMeshProUGUI gemText;
    private int gemCount;

    private void Awake() => gemText = GetComponent<TextMeshProUGUI>();

    private void Start() => gemText.text = gemCount.ToString();

    private void OnEnable() => EventManager.OnCollectGems += CollectedGems;

    private void OnDisable() => EventManager.OnCollectGems -= CollectedGems;

    private void CollectedGems(int gemCount, BuffType gemType)
    {
        if (gemType == this.gemType) 
        { 
            gemText.text = $"{gemCount}"; 
        }
    }
}
