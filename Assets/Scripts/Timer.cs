using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int TimeSec;
    public GameObject TimerText;

    private void Start() {
        TimeSec = 120;
        StartCoroutine(TimerStart());
    }

    IEnumerator TimerStart() {
        while(true) {
            TimerText.GetComponent<TMP_Text>().SetText($"{TimeSec} Sec");
            yield return new WaitForSecondsRealtime(1f);
            TimeSec--;
            if(TimeSec == 0) {
                break;
            }
        }
        StudySceneManager.instance.questionManager.SubmitAnswers();
    }

}
