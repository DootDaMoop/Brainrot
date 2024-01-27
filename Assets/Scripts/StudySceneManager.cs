using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudySceneManager : MonoBehaviour
{
    public static StudySceneManager instance;
    public AttentionSpanManager attentionSpanManager;
    public CameraManager cameraManager;
    

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
