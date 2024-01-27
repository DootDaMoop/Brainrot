using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class AttentionSpanManager : MonoBehaviour
{
    public float MaxAttentionSpan = 100f;
    public float CurrentAttentionSpan;
    public float AttentionSpanDecreaseRate = 2f;
    public float AttentionSpanIncreaseRate = 1f;
    public bool isOnPhone = false;
    public GameObject Phone;
    public GameObject Blur;

    private void Start() {
        CurrentAttentionSpan = MaxAttentionSpan;
        StartCoroutine(ChangeAttentionSpan());
    }

    private void Update() {
        // Phone Functionality
        if(Input.GetKey(KeyCode.LeftShift)) {
            isOnPhone = true;
            Phone.SetActive(true);
            Blur.SetActive(true);
        } else {
            isOnPhone = false;
            Phone.SetActive(false);
            Blur.SetActive(false);
        }
    }

    IEnumerator ChangeAttentionSpan() {
        while(CurrentAttentionSpan > 0) {
            if(!isOnPhone) {
                yield return new WaitForSecondsRealtime(1f);
                CurrentAttentionSpan = Mathf.Clamp(CurrentAttentionSpan - AttentionSpanDecreaseRate, 0f, MaxAttentionSpan);
                Debug.Log($"Attention Span: {CurrentAttentionSpan}");
            } else {
                yield return new WaitForSecondsRealtime(1f);
                CurrentAttentionSpan = Mathf.Clamp(CurrentAttentionSpan + AttentionSpanIncreaseRate, 0f, MaxAttentionSpan);
                Debug.Log($"Attention Span: {CurrentAttentionSpan}");
            }
        }
    }
}
