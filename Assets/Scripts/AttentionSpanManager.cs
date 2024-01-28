using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionSpanManager : MonoBehaviour
{
    [Header("Attention Span Settings")]
    public float MaxAttentionSpan = 100f;
    public float CurrentAttentionSpan;
    public float AttentionSpanDecreaseRate = 2f;
    public float AttentionSpanIncreaseRate = 1f;

    [Header("BrainRot Settings")]
    public int BrainRotLevel;
    public float BrainRotScale;
    public float BrainRotSpanIncreaseRate = 2f;

    [Header("BrainRot Effects")]
    public GameObject StaticFilter;

    [Header("Phone Stuff")]
    public GameObject Phone;
    public GameObject Blur;
    public bool isOnPhone = false;

    private void Start() {
        CurrentAttentionSpan = MaxAttentionSpan;
        StartCoroutine(ChangeAttentionSpan());
        BrainRotLevel = 0;
    }

    private void Update() {
        // Phone Functionality
        if(Input.GetKey(KeyCode.LeftShift)) {
            isOnPhone = true;
            Phone.SetActive(true);
            Blur.SetActive(true);
            // pos X: 1250, pos y: 0 to pox: 0, poy:0
            Phone.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(Phone.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, Time.deltaTime*3.0f);
        } else {
            isOnPhone = false;
            Phone.SetActive(false);
            Blur.SetActive(false);
            Phone.GetComponent<RectTransform>().anchoredPosition = new Vector2(1250.0f,0.0f);
        }

        // Enables Brain Rot Effects for each level
        switch(BrainRotLevel) {
            case 1:
                StaticFilter.gameObject.SetActive(true);
                if(GameObject.FindGameObjectWithTag("Distraction") != null) {
                    StudySceneManager.instance.distractionManager.SpawnInterval = 10;
                    StudySceneManager.instance.distractionManager.PrefabHealth = 3;
                    if(GameObject.FindGameObjectWithTag("Distraction").GetComponent<Distraction>().Health == 0) {
                        Destroy(GameObject.FindGameObjectWithTag("Distraction"));
                    }
                }

                if(!StudySceneManager.instance.distractionManager.isRunning) {
                    StartCoroutine(StudySceneManager.instance.distractionManager.SpawnDistractions());
                }
                
                break;
            case 2:
                if(GameObject.FindGameObjectWithTag("Distraction") != null) {
                    StudySceneManager.instance.distractionManager.SpawnInterval = 7;
                    StudySceneManager.instance.distractionManager.PrefabHealth = 5;
                }
                Color newColor = StaticFilter.GetComponent<Image>().color;
                newColor.a = 0.2f;
                StaticFilter.GetComponent<Image>().color = newColor;
                break;
            case 3:
                if(GameObject.FindGameObjectWithTag("Distraction") != null) {
                    StudySceneManager.instance.distractionManager.SpawnInterval = 5;
                    StudySceneManager.instance.distractionManager.PrefabHealth = 7;
                }
                Color newColor2 = StaticFilter.GetComponent<Image>().color;
                newColor2.a = 0.4f;
                StaticFilter.GetComponent<Image>().color = newColor2;
                break;
            case 4:
                if(GameObject.FindGameObjectWithTag("Distraction") != null) {
                    StudySceneManager.instance.distractionManager.SpawnInterval = 3;
                    StudySceneManager.instance.distractionManager.PrefabHealth = 10;
                }
                Color newColor3 = StaticFilter.GetComponent<Image>().color;
                newColor2.a = 0.4f;
                StaticFilter.GetComponent<Image>().color = newColor3;
                break;
            case 5:
                StudySceneManager.instance.sceneSwitcher.GoScene("End");
                break;
            default:
                break;
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
                BrainRotScale = Mathf.Clamp(BrainRotScale + BrainRotSpanIncreaseRate, 0f, 100f);
                if(BrainRotScale >= 20 && BrainRotScale <= 39) {
                    BrainRotLevel = 1;
                } else if(BrainRotScale >= 40 && BrainRotScale <= 59) {
                    BrainRotLevel = 2;
                } else if(BrainRotScale >= 60 && BrainRotScale <= 89) {
                    BrainRotLevel = 3;
                } else if(BrainRotScale >= 90 && BrainRotLevel != 100) {
                    BrainRotLevel = 4;
                } else if(BrainRotLevel == 100) {
                    BrainRotLevel = 5;
                }
                Debug.Log($"Attention Span: {CurrentAttentionSpan} | {BrainRotScale}:{BrainRotLevel}");
            }
        }
    }
}
