using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float SwayAmount = 0.5f;
    public float SwaySpeed = 1f;
    public float SwayMultiplier = 1f;
    public Vector3 StartingPosition;

    private void Start() {
        StartingPosition = transform.position;
    }

    private void Update() {
        float swayX = Mathf.Sin(Time.time*SwaySpeed * SwayMultiplier) * SwayAmount;
        float swayY = Mathf.Sin(Time.time*SwaySpeed * 0.5f * SwayMultiplier) * SwayAmount;
        if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan == 0) {
            // Game Over
            StudySceneManager.instance.sceneSwitcher.GoScene("End");

        } else if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan >= 1 && StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan <= 20) {
            SwayAmount = 3.5f;
            SwaySpeed = 5f;
            SwayMultiplier = 5f;
            transform.position = new Vector3(swayX,swayY, 0f);
        } else if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan >= 21 && StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan <= 40) {
            SwayAmount = 2.5f;
            SwaySpeed = 3f;
            SwayMultiplier = 3f;
            transform.position = new Vector3(swayX,swayY, 0f);
        } else if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan >= 41 && StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan <= 60) {
            SwayAmount = 1.5f;
            SwaySpeed = 2f;
            SwayMultiplier = 2f;
            transform.position = new Vector3(swayX,swayY, 0f);
        } else if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan >= 61 && StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan <= 80) {
            SwayAmount = 1f;
            SwaySpeed = 1f;
            SwayMultiplier = 1f;
            transform.position = new Vector3(swayX,swayY, 0f);
        } else if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan > 80) {
            transform.position = StartingPosition;
        }
        
    }
}
