using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float swayAmount = 0.1f;
    public float swaySpeed = 1f;
    public Vector3 StartingPosition;

    private void Start() {
        StartingPosition = transform.position;
    }

    private void Update() {
        float swayX = Mathf.Sin(Time.time*swaySpeed) * swayAmount;
        float swayY = Mathf.Sin(Time.time*swaySpeed * 0.5f) * swayAmount;

        if(StudySceneManager.instance.attentionSpanManager.CurrentAttentionSpan < 80) {
            transform.localPosition = new Vector3(swayX,swayY, 0f);
        } else {
            transform.localPosition = StartingPosition;
        }
        
    }
}
