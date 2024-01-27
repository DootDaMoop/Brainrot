using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionSpanManager : MonoBehaviour
{
    public float MaxAttentionSpan = 100f;
    public float CurrentAttentionSpan;
    public float AttentionSpanDecreaseRate = 2f;
    public float AttentionSpanIncreaseRate = 1f;
    public bool isOnPhone = false;

    private void Start() {
        CurrentAttentionSpan = MaxAttentionSpan;
        StartCoroutine(DecreaseAttentionSpan());
    }

    private void Update() {
        
    }

    IEnumerator DecreaseAttentionSpan() {
        while(CurrentAttentionSpan > 0 && !isOnPhone) {
            yield return new WaitForSecondsRealtime(1f);
            CurrentAttentionSpan = Mathf.Clamp(CurrentAttentionSpan - AttentionSpanDecreaseRate, 0f, MaxAttentionSpan);
            Debug.Log($"Attention Span: {CurrentAttentionSpan}");
        }
    }

    IEnumerator IncreaseAttentionSpan() {
        while(CurrentAttentionSpan > 0 && isOnPhone) {
            yield return new WaitForSecondsRealtime(1f);
            CurrentAttentionSpan = Mathf.Clamp(CurrentAttentionSpan + AttentionSpanIncreaseRate, 0f, MaxAttentionSpan);
            Debug.Log($"Attention Span: {CurrentAttentionSpan}");
        }
    }
}
