using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float SwayAmount = 0.1f;
    public float SwaySpeed = 1f;
    public float SwayMultiplier = 5f;
    public Vector3 StartingPosition;

    private void Start() {
        StartingPosition = transform.position;
    }

    private void Update() {
        float swayX = Mathf.Sin(Time.time*SwaySpeed * SwayMultiplier) * SwayAmount;
        float swayY = Mathf.Sin(Time.time*SwaySpeed * 0.5f * SwayMultiplier) * SwayAmount;

        if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan < 80) {
            transform.position = new Vector3(swayX,swayY, 0f);
        } else {
            transform.position = StartingPosition;
        }
        
    }
}
